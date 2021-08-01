using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;

namespace SocialMedia.Infrastructure.Data.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
              .HasColumnName("IdUser");

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasColumnName("Names")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasColumnName("Surname")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.DateOfBirth)
                .HasColumnName("DateBirth")
                .HasColumnType("date");

            builder.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.IsActive)
                .HasColumnName("Active");
        }
    }
}
