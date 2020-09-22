using EBook.Core.Infrastructure;

namespace EBook.Core.Entites.Mapping
{
    class BookMap : EntityTypeConfigurationBase<Book>
    {
        public BookMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            //Properties
            this.Property(t => t.Name)
               .IsRequired()
               .HasMaxLength(150);

            //Properties
            this.Property(t => t.Author)
               .IsRequired()
               .HasMaxLength(150);

            //Properties
            this.Property(t => t.Name)
               .IsRequired()
               .HasMaxLength(150);



            // Table & Column Mappings
            ToTable("Book");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Author).HasColumnName("Author");
            Property(t => t.Price).HasColumnName("Price");

        }
    }
}
