using Back_End.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Back_End.Data.config
{
    public partial class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
               .HasMaxLength(55).IsRequired();


            builder.Property(p => p.Password)
               .HasMaxLength(55).IsRequired();

            builder.ToTable("Users");
        }
    }

    public partial class UserPermissionsConfiguration : IEntityTypeConfiguration<UserPermissions>
    {
        public void Configure(EntityTypeBuilder<UserPermissions> builder)
        {
            builder.HasKey(p => new { p.PermissionId, p.UserId });


            builder.ToTable("UserPermissions");
        }
    }
}
