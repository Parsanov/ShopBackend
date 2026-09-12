
namespace Core.Models
{
    public class Categories
    {
        public Guid Id { get; set; }
        public string NameCategory { get; set; }
        public List<Shoes> Shoes { get; set; }
    }
}
