using Board.Contracts.Comments;
using Board.Contracts.Favotites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Favorites;


public interface IUserSetService
{
    Task<UsersetDto> AddFavorite(UsersetDto dto, CancellationToken cancellationToken);

    /// Идея - сделать что-то среднее между "Избранное" и "Категории"
    /// По сути - возможность для пользователей создавать списки постов, объединенных одной лог.темой
    /// Как плейлист. Устроено должно быть как категории.(но в отличии от последних, доступны для 
    /// редактирования обычным пользователям".

}

