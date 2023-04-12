using Board.Domain.Categories;
using Board.Domain.ParentCategories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastructure.DataAccess.Contexts.ParentCategories.Configuration;

/// <summary>
/// Конфигурация сущности родительской категории
/// </summary>
public class ParentCategoryConfiguration : IEntityTypeConfiguration<ParentCategory>
{
    public void Configure(EntityTypeBuilder<ParentCategory> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name).IsRequired().HasMaxLength(256);

        builder.HasMany(s => s.Categories).WithOne(s => s.ParentCategory).
            HasForeignKey(c => c.ParentId).IsRequired().OnDelete(DeleteBehavior.Cascade);
    }
}
