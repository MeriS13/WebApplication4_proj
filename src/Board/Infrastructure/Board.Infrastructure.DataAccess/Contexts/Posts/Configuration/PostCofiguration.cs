using Board.Domain.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastructure.DataAccess.Contexts.Posts.Configuration;

/// <summary>
/// Конфигурация сущности объявления
/// </summary>
public class PostCofiguration : IEntityTypeConfiguration<Post>
{

    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name).IsRequired().HasMaxLength(256);
        builder.Property(a => a.Description).IsRequired().HasMaxLength(1024);
        builder.Property(a => a.CreationDate).HasConversion(s => s, s => DateTime.SpecifyKind(s, DateTimeKind.Utc));
        builder.Property(a => a.IsFavorite).IsRequired();

        builder.HasMany(s => s.Comments).WithOne(s => s.Post).
        HasForeignKey(c => c.PostId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                

    }
}
