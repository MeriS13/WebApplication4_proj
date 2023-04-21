using Board.Domain.Categories;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Board.Domain.Comments;

namespace Board.Infrastructure.DataAccess.Contexts.Comments.Configuration;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.UserName).IsRequired().HasMaxLength(50);
        builder.Property(a => a.Content).IsRequired().HasMaxLength(800);
        builder.Property(a => a.CreationDate).HasConversion(s => s, s => DateTime.SpecifyKind(s, DateTimeKind.Utc));



        builder.HasMany(s => s.Answers).WithOne(s => s.Comment).
        HasForeignKey(c => c.CommentId).IsRequired().OnDelete(DeleteBehavior.Cascade);
    }
}