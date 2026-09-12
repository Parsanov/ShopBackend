using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class ShoesSize
    {
        [Key]
        public Guid Id { get; set; }
        public byte Size { get; set; }
        public short QuantityInStock { get; set; }

        [ForeignKey("ShoesId")]
        public Guid ShoesId { get; set; }
        public Shoes Shoes { get; set; }

    }
}