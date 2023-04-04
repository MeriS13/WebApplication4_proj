using Board.Contracts.Comments;
using Board.Contracts.Favotites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Favorites;


public interface IFavoritesService
{
    Task<FavoriteDto> AddFavorite(FavoriteDto dto, CancellationToken cancellationToken);
}

