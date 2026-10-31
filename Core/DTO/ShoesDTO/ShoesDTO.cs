

namespace Core.DTO.ShoesDTO
{
    public record ShoesDTO(

        int id,
        string BrandName,
        string Descriptions,
        decimal Price,
        string VendorCode,
        decimal Weight,
        List<string> ImagesUrls,
        List<ShoesSizeDTO> ShoesSizes

        );
   
}
