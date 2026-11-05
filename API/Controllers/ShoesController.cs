using Core.DTO.CardDTO;
using Core.Interfaces;
using Core.Models;
using Core.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ShoesController : Controller
    {
        private readonly IShoesRepository _shoesRepository;

        public ShoesController(IShoesRepository shoesRepository)
        {
            _shoesRepository = shoesRepository;
        }

       

        [HttpGet("/api/shoes/allShoes")]
        public async Task<ActionResult<PagedResult<ShoesCardDTO>>> GetShoes(
            [FromQuery] PaginationParams pagination,
            CancellationToken ct)
        {
            var result = await _shoesRepository.GetShoesAsync(pagination, ct);

            return Ok(result);
        }


        [HttpGet("api/shoes/getOneShoes{id:int}")]
        public async Task<IActionResult> GetOneShoes(int id, CancellationToken ct)
        {
            var shoes = await _shoesRepository.GetOneShoes(id, ct);

            if (shoes is null)
                return NotFound();

            return Ok(shoes);
        }

        [HttpPost("api/shoes/sortShoes")]
        public async Task<ActionResult<PagedResult<ShoesCardDTO>>> GetSortShoes([FromQuery] ShoesFilterQuery filterQuery)
        { 
            var result = await _shoesRepository.SortAsync(filterQuery);
            return Ok(result);
        }

    }
}
