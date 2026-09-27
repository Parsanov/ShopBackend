using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class XMLParserController : Controller
    {
        private readonly IXMLCategoriesParser _categoriesParser;
        private readonly IXMLShoesParser _shoesParser;

        public XMLParserController(IXMLCategoriesParser categoriesParser, IXMLShoesParser shoesParser)
        {
            _categoriesParser = categoriesParser;
            _shoesParser = shoesParser;
        }


       [HttpPost("ParseCategoriesXML")]
        public async Task<IActionResult> ParseCategoriesXML(IFormFile xmlFile)
        {
            if (xmlFile == null || xmlFile.Length == 0 ) return BadRequest("File is empty or null");

            var tempFilePath = Path.Combine(Path.GetTempFileName() + Path.GetExtension(xmlFile.FileName));

            using (var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await xmlFile.CopyToAsync(fileStream);
            }

            var categories = await _categoriesParser.ParseCategoriesAsync(tempFilePath);

           return Ok(categories);
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

            var shoes = await _shoesParser.ParseShoesAsync(tempFilePath);
            return Ok(shoes);
        }
    }
}
