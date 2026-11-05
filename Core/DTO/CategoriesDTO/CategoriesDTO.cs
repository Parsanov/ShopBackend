using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTO.CategoriesDTO
{
    public record CategoriesDTO(int Id, string Name, int? ParentId, List<CategoriesDTO> Children);
}
