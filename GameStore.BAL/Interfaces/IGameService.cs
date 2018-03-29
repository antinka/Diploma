using GameStore.BAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BAL.Interfaces
{
    public interface IGameService: IGameCRUDService
    {
        IEnumerable<GameDTO> GetGamesByGenre(Guid GenreId);
        IEnumerable<GameDTO> GetGamesByPlatformType(Guid platformTypeId);
    }
}
