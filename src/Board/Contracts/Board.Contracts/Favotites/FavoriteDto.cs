using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Favotites;

/// <summary>
/// Модель раздела избранное
/// </summary>
public class FavoriteDto
{
    //public string Name { get; set; }
    public Guid[] PostsId { get; set; }

}
