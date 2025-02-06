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
        builder.Property(p => p.Name)
            .IsRequired();
        builder.HasIndex(p => p.Name)
            .IsUnique();
        builder.Property(u => u.Password)
            .IsRequired();
        builder.Property(u => u.Salt)
            .IsRequired();
        builder.HasMany(u => u.Todos)
            .WithOne()
            .HasForeignKey(t => t.UserId)
            .IsRequired();
    }
}