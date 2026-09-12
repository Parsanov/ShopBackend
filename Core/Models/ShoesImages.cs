using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class ShoesImages
    {
        [Key]
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }

        [ForeignKey("ShoesId")]
        public Guid ShoesId { get; set; }
        public Shoes Shoes { get; set; }
    }
}