using Core.DTO.Xml.DTO;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IDBShoesRepository
    {
        Task<IEnumerable<Shoes>> GetAllShoesAsync();
        Task UploadShoesAsync(IEnumerable<XMLShoesDTO> categories);
    }
}
