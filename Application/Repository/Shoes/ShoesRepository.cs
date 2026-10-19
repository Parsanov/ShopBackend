using Core.DTO.Xml.DTO;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Repository.Shoes
{
    public class ShoesRepository : IShoesRepository
    {
        private readonly IDBShoesRepository _dBShoes;
        private readonly IXMLShoesParser _xmlShoes;

        public ShoesRepository(IDBShoesRepository dBShoes, IXMLShoesParser xmlShoes)
        {
            _dBShoes = dBShoes;
            _xmlShoes = xmlShoes;
        }


        public Task<IEnumerable<XMLShoesDTO>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UploadAsync(string xmlFile)
        {
            var xmlShoes = await _xmlShoes.ParseShoesAsync(xmlFile);
            await _dBShoes.UploadShoesAsync(xmlShoes);
        }


    }
}
 