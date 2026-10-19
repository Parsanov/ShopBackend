using Core.DTO.Xml.DTO;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Repository.Category
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IXMLCategoriesParser _xmlCategoriesParser;
        private readonly IDBCategoryRepository _dBCategoryRepository;

        public CategoryRepository(IXMLCategoriesParser xmlCategoriesParser, IDBCategoryRepository dBCategoryRepository)
        {
            _xmlCategoriesParser = xmlCategoriesParser;
            _dBCategoryRepository = dBCategoryRepository;
        }



        public Task<IEnumerable<XMLCategoryDTO>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UploadAsync(string xmlFile)
        {
            var xmlCategories = await _xmlCategoriesParser.ParseCategoriesAsync(xmlFile);
            await _dBCategoryRepository.UploadCategoriesAsync(xmlCategories);
        }
    }
}
