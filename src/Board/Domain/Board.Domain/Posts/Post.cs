using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Domain.Posts;

/// <summary>
/// Это начало для доменных моделей
/// </summary>

public class Post
{
    
    public string Name { get; set; }

    
    public string Description { get; set; }

    
    public Guid Id { get; set; } 

    
    public string[] Tags { get; set; }

    
    public DateTime CreationDate { get; set; }
}
