using GameStore.BAL.DTO;
using System;
using System.Collections.Generic;

namespace GameStore.BAL.Interfaces
{
    public interface IGameService: IGameCrudService
    {
        IEnumerable<GameDTO> GetGamesByGenre(Guid genreId);
        IEnumerable<GameDTO> GetGamesByPlatformType(Guid platformTypeId);
    }
}
