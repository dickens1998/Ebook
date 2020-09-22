namespace EBook.Models
{
    public class UpdateBookBindingModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
    }

    public class AddBookBindingModel
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
    }

    public class DeleteBookBindingModel
    {
        public int ID { get; set; }
    }

    public class GetByIdBindingModel
    {
        public int ID { get; set; }
    }
}