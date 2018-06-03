using System.Collections.Generic;
using GameStore.BLL.Filters.Abstract;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Filters.GameFilters.Implementation
{
    public class GamePipeline : Pipeline<IEnumerable<Game>>
    {
        public override IEnumerable<Game> Process(IEnumerable<Game> input)
        {
            foreach (var filter in filters)
            {
                input = filter.Execute(input);
            }

            return input;
        }
    }
}
