using System;
using System.Collections.Generic;
using GameStore.BLL.DTO;

namespace GameStore.BLL.Interfaces
{
    public interface IGameService: IGameCrudService
    {
        IEnumerable<GameDTO> GetGamesByGenre(Guid genreId);

        IEnumerable<GameDTO> GetGamesByPlatformType(Guid platformTypeId);

        int GetCountGame();
    }
}
