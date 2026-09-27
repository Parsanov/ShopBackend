
namespace Core.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string NameCategory { get; set; }
        public int XmlCategoryId { get; set; }
        public int ParentId { get; set; }
        public Categories Parent { get; set; }

        public List<Categories> Children { get; set; }
        public List<Shoes> Shoes { get; set; }
    }
}
