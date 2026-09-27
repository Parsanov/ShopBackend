using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class ShoesSize
    {
        public int Id { get; set; }
        public byte Size { get; set; }
        public short QuantityInStock { get; set; }

        public int ShoesId { get; set; }
        public Shoes Shoes { get; set; }

    }
}