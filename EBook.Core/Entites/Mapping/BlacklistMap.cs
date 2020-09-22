using EBook.Core.Infrastructure;

namespace EBook.Core.Entites.Mapping
{
    public class BlacklistMap : EntityTypeConfigurationBase<Blacklist>
    {
        public BlacklistMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Table & Column Mappings
            ToTable("Blacklist");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.UserId).HasColumnName("UserId");
            Property(t => t.Count).HasColumnName("Count");

        }
    }
}
