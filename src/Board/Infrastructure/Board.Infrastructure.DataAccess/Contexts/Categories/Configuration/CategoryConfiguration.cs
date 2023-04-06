using Board.Domain.Categories;
using Board.Domain.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastructure.DataAccess.Contexts.Categories.Configuration;

/// <summary>
/// Конфигурация сущности категории
/// </summary>
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name).IsRequired().HasMaxLength(256);

        builder.HasMany(s => s.Posts).WithOne(s => s.Category).
            HasForeignKey(c => c.CategoryId).IsRequired().OnDelete(DeleteBehavior.Cascade);
    }

}
