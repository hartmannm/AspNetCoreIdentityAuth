using ANCIA.Authentication.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ANCIA.Authentication.Infra.Data.Mapping
{
    public class AppRoleMap : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.Property(x => x.Id)
                        .HasColumnType("varchar(255)");

            builder.Property(x => x.ConcurrencyStamp)
                .IsConcurrencyToken()
                .HasColumnType("varchar(255)");

            builder.Property(x => x.Name)
                .HasMaxLength(256)
                .HasColumnType("varchar(256)");

            builder.Property(x => x.NormalizedName)
                .HasMaxLength(256)
                .HasColumnType("varchar(256)");

            builder.Property(x => x.Active)
                .HasColumnType("bit")
                .HasDefaultValue(true);

            builder.Property(x => x.CreatedBy)
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnType("datetimeoffset")
                .IsRequired();

            builder.Property(x => x.LastUpdatedBy)
                .HasColumnType("varchar(255)");

            builder.Property(x => x.LastUpdatedAt)
                .HasColumnType("datetimeoffset");

            builder.HasKey(x => x.Id);

            builder.HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(c => c.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(c => c.LastUpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(x => x.NormalizedName)
                .IsUnique()
                .HasDatabaseName("RoleNameIndex")
                .HasFilter("[NormalizedName] IS NOT NULL");

            builder.ToTable("AspNetRoles");
        }
    }
}
