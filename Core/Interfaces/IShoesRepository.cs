using Core.DTO.Xml.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IShoesRepository
    {
        Task<IEnumerable<XMLShoesDTO>> GetAsync();
        Task UploadAsync(string xmlFile);
    }
}
