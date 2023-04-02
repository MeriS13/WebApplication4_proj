using Board.Application.AppData.Contexts.Favorites;
using Board.Contracts.Favotites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Posts.Services;

/// <summary>
/// Сервис для работы с разделом "Избранное"
/// </summary>
public class FavoritesService : IFavoritesService
{
    /// <summary>
    /// Добавление модели в избранное (неправильно! происходит создание)
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<CreateFavoriteDto> AddFavorite(CreateFavoriteDto dto, CancellationToken cancellationToken)
    {

        //вызов репозитория для сохранения в бд

        //возврат результата
        await Task.Run(() => dto, cancellationToken);


        return new CreateFavoriteDto();
    }

}
