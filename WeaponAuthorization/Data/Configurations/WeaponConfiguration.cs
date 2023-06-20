using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeaponAuthorization.Data.Models;

namespace WeaponAuthorization.Data.Configurations
{
    public class WeaponConfiguration : IEntityTypeConfiguration<Weapon>
    {
        public void Configure(EntityTypeBuilder<Weapon> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasColumnName("WeaponName").HasMaxLength(200);
            builder.Property(x => x.Description).IsRequired().HasColumnName("WeaponDescription").HasMaxLength(int.MaxValue);
            builder.Property(x => x.Type);
        }
    }
}
