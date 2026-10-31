using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IShoesRepository _shoesRepository;

        public AdminController(ICategoryRepository categoryRepository, IShoesRepository shoesRepository)
        {
            _categoryRepository = categoryRepository;
            _shoesRepository = shoesRepository;
        }


        [HttpPost("/api/admin/parseCategoriesXML")]
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


        [HttpPost("/api/admin/addShoesXML")]
        public async Task<IActionResult> AddShoesXML(IFormFile xmlFile)
        {
            if (xmlFile == null || xmlFile.Length == 0) return BadRequest("File is empty or null");

            var tempFilePath = Path.Combine(Path.GetTempFileName() + Path.GetExtension(xmlFile.FileName));

            using (var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await xmlFile.CopyToAsync(fileStream);
            }

            await _shoesRepository.AddAsync(tempFilePath);

            return Ok();
        }


        [HttpPost("/api/admin/updateShoesXML")]
        public async Task<IActionResult> UpdateShoesXML(IFormFile xmlShoesFile)
        {
            if (xmlShoesFile == null || xmlShoesFile.Length == 0) return BadRequest("File is empty or null");
            var tempFilePath = Path.Combine(Path.GetTempFileName() + Path.GetExtension(xmlShoesFile.FileName));

            using (var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await xmlShoesFile.CopyToAsync(fileStream);
            }

            await _shoesRepository.UploadAsync(tempFilePath);

            return Ok();
        }


      
    }
}
