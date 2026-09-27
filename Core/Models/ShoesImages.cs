using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class ShoesImages
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        public int ShoesId { get; set; }
        public Shoes Shoes { get; set; }
    }
}