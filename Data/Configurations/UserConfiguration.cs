using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(10);
        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(20);
        builder.HasMany(u => u.Todos)
            .WithOne()
            .HasForeignKey(t => t.UserId)
            .IsRequired();
    }
}