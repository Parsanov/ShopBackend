using Core.DTO.Xml.DTO;
using Core.Interfaces;
using Core.Models;
using Persistence;
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


        public Task<IEnumerable<Core.Models.Shoes>> GetAllShoesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UploadShoesAsync(IEnumerable<XMLShoesDTO> shoesDTOs)
        {
            ArgumentNullException.ThrowIfNull(shoesDTOs);

            var shoesList = shoesDTOs.ToList();

            if (shoesList.Count == 0)
                throw new ArgumentNullException("Список товарів для імпорту порожній", nameof(shoesDTOs));


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
                    Available = modelGroup.Any(a => a.Available),
                    VendorCode = firstItem.VendorCode,
                    Weight = firstItem.Weight,

                    ShoesSizes = modelGroup
                        .Select(sizeItem => new
                        {
                            Size = ParseSize(sizeItem.ShoesSizes),               
                            QuantityInStock = (short)sizeItem.QuantityInStock    
                        })
                        .Where(x => x.Size.HasValue)                             
                        .Select(x => new ShoesSize                               
                        {
                            Size = x.Size!.Value,                                
                            QuantityInStock = x.QuantityInStock
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
