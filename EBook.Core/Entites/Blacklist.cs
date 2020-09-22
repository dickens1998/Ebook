namespace EBook.Core.Entites
{
    public class Blacklist:BaseEntity
    {
        public int UserId { get; set; }
        public int Count { get; set; }

    }
}
