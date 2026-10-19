using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTO.Xml.DTO
{
    public class XMLCategoryDTO
    {
        public int XmlId { get; set; }
        public int? ParentXmlId { get; set; }
        public string NameCategory { get; set; } = null!;
    }
}
