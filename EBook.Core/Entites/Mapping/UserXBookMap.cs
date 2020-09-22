using EBook.Core.Infrastructure;

namespace EBook.Core.Entites.Mapping
{
    class UserXBookMap : EntityTypeConfigurationBase<UserXBook>
    {
        public UserXBookMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Table & Column Mappings
            ToTable("UserXBook");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.UserID).HasColumnName("UserID");
            Property(t => t.BookID).HasColumnName("BookID");
            Property(t => t.State).HasColumnName("State");
            Property(t => t.BorrowDate).HasColumnName("BorrowDate");
            Property(t => t.ReturnDate).HasColumnName("ReturnDate");
            Property(t => t.RealReturnDate).HasColumnName("RealReturnDate");
        }
    }
}
