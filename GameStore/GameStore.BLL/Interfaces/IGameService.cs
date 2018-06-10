using System;
using System.Collections.Generic;
using GameStore.BLL.DTO;
using GameStore.BLL.Enums;

namespace GameStore.BLL.Interfaces
{
    public interface IGameService : IGameCrudService
    {
        IEnumerable<GameDTO> GetDeleteGames();

        IEnumerable<GameDTO> GetGamesByGenre(Guid genreId);

        IEnumerable<GameDTO> GetGamesByPlatformType(Guid platformTypeId);

        int GetCountGame();

        bool IsUniqueKey(ExtendGameDTO gameExtendGameDto);

        void Renew(string gameKey);

        IEnumerable<GameDTO> GetGamesByFilter(FilterDTO filter, int page, PageSize pageSize, out int totalItemsByFilter);
    }
}
