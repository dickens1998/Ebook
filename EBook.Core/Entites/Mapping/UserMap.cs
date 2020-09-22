using EBook.Core.Infrastructure;

namespace EBook.Core.Entites.Mapping
{
    public class UserMap : EntityTypeConfigurationBase<User>
    {

        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            //Properties
            this.Property(t => t.Name)
               .IsRequired()
               .HasMaxLength(150);

            //Properties
            this.Property(t => t.Phone)
               .IsRequired()
               .HasMaxLength(150);
            //Properties
            this.Property(t => t.Address)
               .IsRequired()
               .HasMaxLength(150);

            // Table & Column Mappings
            ToTable("User");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Phone).HasColumnName("Phone");
            Property(t => t.Address).HasColumnName("Address");

        }
    }
}
