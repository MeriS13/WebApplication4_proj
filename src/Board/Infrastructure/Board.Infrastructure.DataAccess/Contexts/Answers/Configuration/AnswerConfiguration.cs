using Board.Domain.Answers;
using Board.Domain.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastructure.DataAccess.Contexts.Answers.Configuration;

public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.CreationDate).HasConversion(s => s, s => DateTime.SpecifyKind(s, DateTimeKind.Utc));
        builder.Property(a => a.UserName).IsRequired().HasMaxLength(256);
        builder.Property(a => a.Content).IsRequired().HasMaxLength(800);


    }
}
