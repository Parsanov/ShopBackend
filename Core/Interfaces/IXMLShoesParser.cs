using Core.DTO.Xml.DTO;

namespace Core.Interfaces
{
    public interface IXMLShoesParser
    {
        Task<IEnumerable<XMLShoesDTO>> ParseShoesAsync(string xmlFile);
    }
}
