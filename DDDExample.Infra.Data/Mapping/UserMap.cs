namespace DDDExample.Infra.Data.Mapping
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");
            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Name)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("name")
                .HasColumnType("varchar(100)");

            builder.Property(prop => prop.Email)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired()
                .HasColumnName("email")
                .HasColumnType("varchar(200)");

            builder.OwnsOne(x => x.Password, newBuilder =>
            {
                newBuilder.Property(x => x.CurrentPassword)
                    .IsRequired()
                    .HasMaxLength(128);

                newBuilder.Property(x => x.LastPassword)
                    .HasMaxLength(256);
            });
        }
    }
}