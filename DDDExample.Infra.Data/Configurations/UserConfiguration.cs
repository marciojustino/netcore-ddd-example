namespace DDDExample.Infra.Data.Configurations
{
    using System;
    using Domain.Entities;
    using Domain.Enums;
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

            builder.Property(p => p.LastLoggedAt)
                .HasColumnName("Last_Logged_At")
                .HasColumnType("datetime")
                .IsRequired()
                .HasDefaultValue(DateTime.Now);

            builder.Property(p => p.Status)
                .HasColumnName("Status")
                .HasColumnType("integer")
                .IsRequired()
                .HasDefaultValue(RegistrationStatus.WaitingApproval);
        }
    }
}