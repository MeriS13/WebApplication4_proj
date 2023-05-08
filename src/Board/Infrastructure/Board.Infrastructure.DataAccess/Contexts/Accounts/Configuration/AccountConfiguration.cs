using Board.Domain.Accounts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastructure.DataAccess.Contexts.Accounts.Configuration;

/// <summary>
/// Конфигурация сущности аккаунта.
/// </summary>
public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name).HasMaxLength(256).IsRequired();
        builder.Property(a => a.Login).HasMaxLength(50).IsRequired();
        builder.Property(a => a.Email).HasMaxLength(50).IsRequired();
        builder.Property(a => a.Password).HasMaxLength(50).IsRequired();
        builder.Property(a => a.Created).HasConversion(s => s, s => DateTime.SpecifyKind(s, DateTimeKind.Utc));

        builder.HasMany(s => s.Posts).WithOne(s => s.Account).
         HasForeignKey(c => c.AccountId).IsRequired().OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Comments).WithOne(s => s.Account).
        HasForeignKey(c => c.AccId).IsRequired().OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Answers).WithOne(s => s.Account).
        HasForeignKey(c => c.AccId).IsRequired().OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Files).WithOne(s => s.Account).
        HasForeignKey(c => c.AccountId).IsRequired().OnDelete(DeleteBehavior.Cascade);
    }
}
