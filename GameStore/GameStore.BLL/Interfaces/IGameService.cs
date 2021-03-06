﻿using System;
using System.Collections.Generic;
using GameStore.BLL.DTO;
using GameStore.BLL.Enums;

namespace GameStore.BLL.Interfaces
{
    public interface IGameService : IGameCrudService
    {
        IEnumerable<GameDTO> GetDeleteGames();

        IEnumerable<GameDTO> GetGamesByPublisherId(Guid publisherId);

        ExtendGameDTO GetGameByPublisherIdAndGameKey(Guid publisherId, string gameKey);

        IEnumerable<GameDTO> GetGamesByGenre(Guid genreId);

        IEnumerable<GameDTO> GetGamesByPlatformType(Guid platformTypeId);

        int GetCountGame();

        bool IsUniqueKey(ExtendGameDTO gameExtendGameDto);

        void Renew(string gameKey);

        void UpdateImage(string gameKey, string pictureName, string imageMimeType);

        IEnumerable<GameDTO> GetGamesByFilter(FilterDTO filter, int page, PageSize pageSize, out int totalItemsByFilter);

        IEnumerable<GameDTO> GetGamesByCollaborative(Guid gamekey,UserDTO userDTO);

        IEnumerable<GameDTO> GetGamesByContent(Guid gamekey, UserDTO userDTO);
    }
}
