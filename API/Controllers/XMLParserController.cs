using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class XMLParserController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IShoesRepository _shoesRepository;

        public XMLParserController(ICategoryRepository categoryRepository, IShoesRepository shoesRepository)
        {
            _categoryRepository = categoryRepository;
            _shoesRepository = shoesRepository;
        }


        [HttpPost("ParseCategoriesXML")]
        public async Task<IActionResult> ParseCategoriesXML(IFormFile xmlFile)
        {
            if (xmlFile == null || xmlFile.Length == 0) return BadRequest("File is empty or null");

            var tempFilePath = Path.Combine(Path.GetTempFileName() + Path.GetExtension(xmlFile.FileName));

            using (var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await xmlFile.CopyToAsync(fileStream);
            }

            await _categoryRepository.UploadAsync(tempFilePath);

            return Ok();
        }


        [HttpPost("ParseShoesXML")]
        public async Task<IActionResult> ParseShoesXML(IFormFile xmlFile)
        {
            if (xmlFile == null || xmlFile.Length == 0) return BadRequest("File is empty or null");

            var tempFilePath = Path.Combine(Path.GetTempFileName() + Path.GetExtension(xmlFile.FileName));

            using (var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await xmlFile.CopyToAsync(fileStream);
            }

            await _shoesRepository.UploadAsync(tempFilePath);

            return Ok();
        }


      
    }
}
