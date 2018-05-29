using System;
using System.Collections.Generic;
using GameStore.BLL.DTO;
using GameStore.BLL.Enums;

namespace GameStore.BLL.Interfaces
{
    public interface IGameService: IGameCrudService
    {
        IEnumerable<GameDTO> GetGamesByGenre(Guid genreId);

        IEnumerable<GameDTO> GetGamesByPlatformType(Guid platformTypeId);

        int GetCountGame();

        bool IsUniqueKey(ExtendGameDTO gameDTO);

        void IncreaseGameView(Guid gameId);

        IEnumerable<GameDTO> GetGamesByFilter(FilterDTO filter, int page = 1, PageSize pageSize = PageSize.All);
    }
}
