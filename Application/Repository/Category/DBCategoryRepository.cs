using Core.DTO.CategoriesDTO;
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


        public async Task<List<CategoriesDTO>> GetAllCategoriesTreeAsync()
        {
            var allCategories = await _dataContext.Categories
                .AsNoTracking()
                .Select(c => new { c.Id, c.NameCategory, c.ParentId })
                .ToListAsync();

            var childrenByParent = allCategories
                .GroupBy(c => c.ParentId ?? -1)
                .ToDictionary(g => g.Key, g => g.ToList());

            List<CategoriesDTO> BuildBunch(int parentKey) =>
                childrenByParent.TryGetValue(parentKey, out var children)
                ? children.Select(c => new CategoriesDTO(c.Id, c.NameCategory, c.ParentId, BuildBunch(c.Id))).ToList()
                : new List<CategoriesDTO>();

            return BuildBunch(-1);
                
        }

        public async Task<HashSet<int>> GetCategoryWithDescendantsAsync(int categoryId)
        {
            var categorisLink = await _dataContext.Categories
                .AsNoTracking()
                .Select(c => new { c.Id, c.ParentId })
                .ToListAsync();

            if (!categorisLink.Any(c => c.Id == categoryId))
                return new HashSet<int>();

            ILookup<int?, int> childrenByParent = categorisLink.ToLookup(c => c.ParentId, c => c.Id);

            var result = new HashSet<int> { categoryId };
            var toVisit = new Queue<int>();

            while (toVisit.Count > 0)
            {
                int currentId = toVisit.Dequeue();

                foreach (int childrenId in childrenByParent[currentId])
                {
                    if (result.Add(childrenId))
                        toVisit.Enqueue(childrenId);
                    
                }
            }

            return result;
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
