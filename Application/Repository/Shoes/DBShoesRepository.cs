using Core.DTO.CardDTO;
using Core.DTO.ShoesDTO;
using Core.DTO.Xml.DTO;
using Core.Interfaces;
using Core.Models;
using Core.Pagination;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Net.ServerSentEvents;
using System.Text.RegularExpressions;


namespace Application.Repository.Shoes
{
    public class DBShoesRepository : IDBShoesRepository
    {
        private readonly DataContext _dataContext;

        public DBShoesRepository(DataContext dataContext)
        {
            _dataContext = dataContext; 
        }


        public async Task<PagedResult<ShoesCardDTO>> GetShoesAsync(PaginationParams pagination, CancellationToken ct)
        {
            return await _dataContext.Shoes
              .AsNoTracking()
              .OrderBy(i => i.Id)
              .Select(s => new ShoesCardDTO(
                    s.Id,
                    s.BrandName,
                    s.Price,
                    s.Images.Select(i => i.ImageUrl).FirstOrDefault()
                    ))
            .ToPagedResultAsync(pagination, ct);
        }

        public async Task<ShoesDTO?> GetOneShoes(int id, CancellationToken ct)
        {
            try
            {
                return await _dataContext.Shoes
                     .AsNoTracking()
                     .Where(s => s.Id == id)
                     .Select(s => new ShoesDTO(
                         s.Id,
                         s.BrandName,
                         s.Description,
                         s.Price,
                         s.VendorCode,
                         s.Weight ?? 0m,


                         s.Images
                         .OrderBy(i => i.Id)
                         .Select(i => i.ImageUrl)
                         .ToList(),


                         s.ShoesSizes
                         .OrderBy(size => size.Size)
                         .Select(size => new Core.DTO.ShoesDTO.ShoesSizeDTO (
                             size.Size, size.QuantityInStock, size.Available))
                         .ToList()

                         )).FirstOrDefaultAsync(ct);

            }
            catch (Exception e)
            {
                throw new ArgumentNullException("Id is null or doesn't exist", e.Message);
            }
        }



        public async Task AddShoesAsync(IEnumerable<XMLShoesDTO> shoesDTOs)
        {
            ArgumentNullException.ThrowIfNull(shoesDTOs);

            var shoesList = shoesDTOs.ToList();

            if (shoesList.Count == 0)
                throw new ArgumentNullException("The list of products for import is empty", nameof(shoesDTOs));


            var modelGroups = shoesList.GroupBy(v => new { 
            
                VendorCode = NormalizeKey(v.VendorCode),
                BrandName = NormalizeKey(v.BrandName)
            });

            foreach (var modelGroup in modelGroups)
            {

                var firstItem = modelGroup.First();

                _dataContext.Shoes.Add(new Core.Models.Shoes
                {
                    BrandName = firstItem.BrandName.Trim(),
                    Description = String.IsNullOrEmpty(firstItem.Description) ?
                        String.Empty
                       : Regex.Replace(firstItem.Description, @"<br\s*/?>", "\n", RegexOptions.IgnoreCase),
                    Price = firstItem.Price,
                    VendorCode = firstItem.VendorCode,
                    Weight = firstItem.Weight,
                    ExternalGroupId = firstItem.ExternalGroupId,

                    ShoesSizes = modelGroup
                        .Select(sizeItem => new
                        {
                            Size = ParseSize(sizeItem.ShoesSizes),               
                            QuantityInStock = (short)sizeItem.QuantityInStock,
                            ExternalOfferId = sizeItem.ExternalOfferId,
                            Available = sizeItem.Available

                        })
                        .Where(x => x.Size.HasValue)                             
                        .Select(x => new ShoesSize                               
                        {
                            Size = x.Size!.Value,                                
                            QuantityInStock = x.QuantityInStock,
                            ExternalOfferId = x.ExternalOfferId,
                            Available = x.Available
                            
                        })
                        .ToList(),                                               

                    Images = firstItem.Images.Select(i => new ShoesImages
                    {
                        ImageUrl = i
                    }).ToList(),

                    CategoriesId = firstItem.CategoriesId
                });
            }

            await _dataContext.SaveChangesAsync();
        }



        public async Task UpdateShoesAsync(IEnumerable<XMLShoesDTO> shoesDTOs)
        {

            var shoesList = shoesDTOs.ToList();

            Dictionary<string, Core.Models.Shoes> existingShoes = await _dataContext.Shoes
                .Include(s => s.ShoesSizes)
                .ToDictionaryAsync(s => s.ExternalGroupId);

            var groupedOffers = shoesList.GroupBy(dto => dto.ExternalGroupId);

            foreach (var group in groupedOffers)
            {
                string groupId = group.Key;
                XMLShoesDTO firstOffer = group.First();

                if (!existingShoes.TryGetValue(groupId,  out var shoe))
                {
                    shoe = MapToShoes(firstOffer);
                    _dataContext.Shoes.Add(shoe);
                }
                else
                {
                    shoe.Price = firstOffer.Price;
                }

                foreach (var offer in group)
                {
                    var size = shoe.ShoesSizes.FirstOrDefault(s => s.ExternalOfferId == offer.ExternalOfferId);

                    if (size is null)
                    {
                        shoe.ShoesSizes.Add(MapToShoesSize(offer));
                    }
                    else
                    {
                        size.QuantityInStock = (short)offer.QuantityInStock;
                        size.Available = offer.Available;
                    }
                }
            }

            await _dataContext.SaveChangesAsync();
        }


        private static ShoesSize MapToShoesSize(XMLShoesDTO shoesSizeDTO)
        {
            return new ShoesSize
            {
                ExternalOfferId = shoesSizeDTO.ExternalOfferId,
                Size = ParseSize(shoesSizeDTO.ShoesSizes) ?? 0,
                QuantityInStock = (short)shoesSizeDTO.QuantityInStock, 
                Available = shoesSizeDTO.Available
            };
        }


        private static Core.Models.Shoes MapToShoes(XMLShoesDTO xMLShoesDTO)
        {
            return new Core.Models.Shoes
            {
                ExternalGroupId = xMLShoesDTO.ExternalGroupId,
                BrandName = xMLShoesDTO.BrandName,
                Description = xMLShoesDTO.Description,
                Price = xMLShoesDTO.Price,
                VendorCode = xMLShoesDTO.VendorCode,
                Weight = xMLShoesDTO.Weight,
                Images = xMLShoesDTO.Images.Select(i => new ShoesImages
                {
                    ImageUrl = i
                }).ToList(),

            };
        }


        private static byte? ParseSize(string rawSize)
       {
            if (string.IsNullOrWhiteSpace(rawSize))
                return null;

            var sizePart = rawSize.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0];

            return byte.TryParse(sizePart, out var parsedSize) ? parsedSize : null;
       }


        private static string NormalizeKey(string? value) =>
            string.IsNullOrWhiteSpace(value) ?
            string.Empty :
            Regex.Replace(value.Trim(), @"\s+", " ").ToLowerInvariant();



       
        

      
    }
}
