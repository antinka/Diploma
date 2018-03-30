using GameStore.BAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BAL.Interfaces
{
    public interface IGameCRUDService
    {
        void AddNewGame(GameDTO gameDTO);
        void EditGame(GameDTO gameDto);
        void DeleteGame(Guid id);
        GameDTO GetGame(Guid id);
        IEnumerable<GameDTO> GetAllGame();
    }
}
