﻿using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Filters.Interfaces;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Filters.GameFilters.Implementation
{
    public class GameFilterByPlatform : IPipeLine<IEnumerable<Game>>
    {
        private readonly IEnumerable<string> _selectedPlatformTypes;

        public GameFilterByPlatform(IEnumerable<string> selectedPlatformTypes)
        {
            _selectedPlatformTypes = selectedPlatformTypes;
        }

        public IEnumerable<Game> Execute(IEnumerable<Game> input)
        {
            if (_selectedPlatformTypes.Any())
            {
                return input.Where(game => game.PlatformTypes.Any(platform => _selectedPlatformTypes.Contains(platform.NameEn) || _selectedPlatformTypes.Contains(platform.NameRu)));
            }

            return input;
        }
    }
}
