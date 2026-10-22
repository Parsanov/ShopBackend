using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTO.CardDTO
{
    public record ShoesCardDTO(
          int Id,
          string BrandName,
          decimal Price,
          string? MainImageUrl,
          bool Available);
}
