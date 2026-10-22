using Core.DTO.CardDTO;
using Core.Interfaces;
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

       

        [HttpGet("/api/shoes")]
        public async Task<ActionResult<PagedResult<ShoesCardDTO>>> GetShoes(
            [FromQuery] PaginationParams pagination,
            CancellationToken ct)
        {
            var result = await _shoesRepository.GetShoesAsync(pagination, ct);

            return Ok(result);
        }
    }
}
