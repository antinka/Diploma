using GameStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.EF
{
    public class GameStoreDbInitializer :DropCreateDatabaseAlways<GameStoreContext>
    {
        protected override void Seed(GameStoreContext context)
        {
            var strategy = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "Strategy"
            };

            var rts = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "RTS",
                IdParentGanre = strategy.Id
            };

            var tbs = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "TBS",
                IdParentGanre = strategy.Id
            };

            context.Genres.Add(rts);
            context.Genres.Add(tbs);
            context.Genres.Add(strategy);

            var races = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "Races",
            };

            var rally = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "rally",
                IdParentGanre = races.Id
            };
            var arcade = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "arcade",
                IdParentGanre = races.Id
            };
            var formula = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "formula",
                IdParentGanre = races.Id
            };
            var offRoad = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "off-road",
                IdParentGanre = races.Id
            };

            context.Genres.Add(rally);
            context.Genres.Add(arcade);
            context.Genres.Add(formula);
            context.Genres.Add(offRoad);
            context.Genres.Add(races);

            var action = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "Action"
            };
            var fps = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "FPS",
                IdParentGanre = action.Id
            };
            var tps = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "TPS",
                IdParentGanre = action.Id
            };
            var subMisc = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "Misc",
                IdParentGanre = action.Id
            };

            context.Genres.Add(fps);
            context.Genres.Add(tps);
            context.Genres.Add(subMisc);
            context.Genres.Add(action);


            var rpg = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "RPG"
            };
            var sports = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "Sports"
            };
            var adventure = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "Adventure"
            };
            var puzzleSkill = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "Puzzle&Skill"
            };

            context.Genres.Add(rpg);
            context.Genres.Add(sports);
            context.Genres.Add(adventure);
            context.Genres.Add(puzzleSkill);

            var windows = new PlatformType
            {
                Id = Guid.NewGuid(),
                Type = "windows"
            };

            var android = new PlatformType
            {
                Id = Guid.NewGuid(),
                Type = "android"
            };

            var ios = new PlatformType
            {
                Id = Guid.NewGuid(),
                Type = "ios"
            };

            var wow = new Game
            {
                Id = Guid.NewGuid(),
                Name = "wow",
                Description = "smth",
                Key =Guid.NewGuid().ToString()
            };
            wow.Genres = new List<Genre>() { fps, adventure };
            wow.PlatformTypes = new List<PlatformType>() { windows };
            context.Games.Add(wow);

            var hero = new Game()
            {
                Id = Guid.NewGuid(),
                Name = "hero",
                Description = "smth",
               Key = Guid.NewGuid().ToString()
            };

            hero.Genres = new List<Genre>() { puzzleSkill, adventure, subMisc };
            hero.PlatformTypes = new List<PlatformType>() { windows, android, ios };
            context.Games.Add(hero);

            var cm1 = new Comment()
            {
                Id = Guid.NewGuid(),
                Game = wow,
                Name = "Vova",
                Body = "cool"
            };

            var cm2 = new Comment()
            {
                Id = Guid.NewGuid(),
                Game = wow,
                Name = "Viola",
                Body = "realy cool"
            };

            var cm3 = new Comment()
            {
                Id = Guid.NewGuid(),
                Game = wow,
                Name = "Vasya",
                Body = "why you think so?",
                ParentCommentId = cm1.Id
            };

            var cm4 = new Comment()
            {
                Id = Guid.NewGuid(),
                Game = hero,
                Name = "hero",
                Body = "soso"
            };

            context.Comments.Add(cm1);
            context.Comments.Add(cm2);
            context.Comments.Add(cm3);
            context.Comments.Add(cm4);

        
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
