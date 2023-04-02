using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Favotites;

public class CreateFavoriteDto
{
    public Guid[] PostsId { get; set; }

}
