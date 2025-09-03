using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sandbox.Domain.Constants;
using Sandbox.Domain.Entities;

namespace Sandbox.DataAccess.Configurations;

public class UserEntityConfigurations : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        // Таблица
        builder.ToTable("users");

        // Первичный ключ
        builder.HasKey(u => u.Id);

        // Свойства с настройками
        builder.Property(u => u.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(u => u.Created)
            .HasColumnName("created")
            .IsRequired();

        builder.Property(u => u.Modified)
            .HasColumnName("modified")
            .IsRequired();

        builder.Property(u => u.Login)
            .HasColumnName("login")
            .HasMaxLength(EntityConstraints.User.LoginMaxLength)
            .IsRequired();

        builder.Property(u => u.FirstName)
            .HasColumnName("first_name")
            .HasMaxLength(EntityConstraints.User.FirstNameMaxLength)
            .IsRequired();

        builder.Property(u => u.LastName)
            .HasColumnName("last_name")
            .HasMaxLength(EntityConstraints.User.LastNameMaxLength)
            .IsRequired();

        builder.Property(u => u.Email)
            .HasColumnName("email")
            .HasMaxLength(EntityConstraints.User.EmailMaxLength)
            .IsRequired();

        builder.Property(u => u.PasswordHash)
            .HasColumnName("password_hash")
            .HasMaxLength(EntityConstraints.User.PasswordHashMaxLength)
            .IsRequired();

        builder.Property(u => u.Right)
            .HasColumnName("right")
            .IsRequired();

        builder.Property(u => u.ProfileImage)
            .HasColumnName("profile_image")
            .HasMaxLength(EntityConstraints.User.ProfileImageMaxLength)
            .IsRequired();

        // Индексы для оптимизации поиска
        builder.HasIndex(u => u.Login)
            .IsUnique()
            .HasDatabaseName("ix_users_login");

        builder.HasIndex(u => u.Email)
            .IsUnique()
            .HasDatabaseName("ix_users_email");

        builder.HasIndex(u => new { u.LastName, u.FirstName })
            .HasDatabaseName("ix_users_lastname_firstname");
    }
}
