using System;

namespace EBook.Core.Entites
{
    public class UserXBook : BaseEntity
    {
        
        public int UserID { get; set; }
        public int BookID { get; set; }
        public State State { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime? RealReturnDate { get; set; }
    }

    public enum State
    {
        Borrow,

        Return
    }
}
