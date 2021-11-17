namespace DDDExample.Infra.Data.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(user => user.Id);

            builder.Property(user => user.Name)
                .HasColumnName("Name")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.OwnsOne(user => user.Email)
                .Property(email => email.Value)
                .HasColumnName("Email")
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder.OwnsOne(user => user.CurrentPassword)
                .Property(password => password.Value)
                .HasColumnName("Current_Password")
                .HasColumnType("varchar(400)")
                .IsRequired();

            builder.OwnsOne(user => user.LastPassword)
                .Property(password => password.Value)
                .HasColumnName("Last_Password")
                .HasColumnType("varchar(400)");
        }
    }
}