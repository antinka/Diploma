using GameStore.DAL.Entities;
using System.Collections.Generic;

namespace GameStore.BLL.Filtration.Implementation
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
