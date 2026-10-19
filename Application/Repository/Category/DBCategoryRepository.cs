using Core.DTO.Xml.DTO;
using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository.Category
{
    public class DBCategoryRepository : IDBCategoryRepository
    {
        private readonly DataContext _dataContext;

        public DBCategoryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        public Task<IEnumerable<Categories>> GetAllCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UploadCategoriesAsync(IEnumerable<XMLCategoryDTO> categories)
        {
            var knowIds = categories.Select(c => c.XmlId).ToHashSet();


            var existingCategories = await _dataContext.Categories.ToDictionaryAsync(c => c.Id);


            foreach (var categorydto in categories)
            {
                int? parentId = categorydto.ParentXmlId is int p && knowIds.Contains(p) ? p : null;


                if (existingCategories.TryGetValue(categorydto.XmlId, out var category))
                {
                    category.NameCategory = categorydto.NameCategory;
                    category.ParentId = parentId;
                }
                else
                {
                    _dataContext.Categories.Add(new Categories
                    {
                        Id = categorydto.XmlId,
                        NameCategory = categorydto.NameCategory,
                        ParentId = parentId
                    });
                }
            }

            await _dataContext.SaveChangesAsync();

        }
    }
}
