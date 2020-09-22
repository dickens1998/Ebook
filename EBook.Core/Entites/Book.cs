namespace EBook.Core.Entites
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }

    }
}
