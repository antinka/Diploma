using GameStore.BAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BAL.Interfaces
{
    public interface IGameService: IGameCrudService
    {
        IEnumerable<GameDTO> GetGamesByGenre(Guid genreId);
        IEnumerable<GameDTO> GetGamesByPlatformType(Guid platformTypeId);
    }
}
