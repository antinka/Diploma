using GameStore.DAL.Entities;
using System.Collections.Generic;

namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<GameStore.DAL.EF.GameStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GameStore.DAL.EF.GameStoreContext context)
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
                ParentGenreId = strategy.Id
            };

            var tbs = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "TBS",
                ParentGenreId = strategy.Id
            };

            context.Genres.AddOrUpdate(rts);
            context.Genres.AddOrUpdate(tbs);
            context.Genres.AddOrUpdate(strategy);

            var races = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "Races",
            };

            var rally = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "rally",
                ParentGenreId = races.Id
            };
            var arcade = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "arcade",
                ParentGenreId = races.Id
            };
            var formula = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "formula",
                ParentGenreId = races.Id
            };
            var offRoad = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "off-road",
                ParentGenreId = races.Id
            };

            context.Genres.AddOrUpdate(rally);
            context.Genres.AddOrUpdate(arcade);
            context.Genres.AddOrUpdate(formula);
            context.Genres.AddOrUpdate(offRoad);
            context.Genres.AddOrUpdate(races);

            var action = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "Action"
            };
            var fps = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "FPS",
                ParentGenreId = action.Id
            };
            var tps = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "TPS",
                ParentGenreId = action.Id
            };
            var subMisc = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "Misc",
                ParentGenreId = action.Id
            };

            context.Genres.AddOrUpdate(fps);
            context.Genres.AddOrUpdate(tps);
            context.Genres.AddOrUpdate(subMisc);
            context.Genres.AddOrUpdate(action);


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

            context.Genres.AddOrUpdate(rpg);
            context.Genres.AddOrUpdate(sports);
            context.Genres.AddOrUpdate(adventure);
            context.Genres.AddOrUpdate(puzzleSkill);

            var windows = new PlatformType
            {
                Id = Guid.NewGuid(),
                Name = "windows"
            };

            var android = new PlatformType
            {
                Id = Guid.NewGuid(),
                Name = "android"
            };

            var ios = new PlatformType
            {
                Id = Guid.NewGuid(),
                Name = "ios"
            };

            var wow = new Game
            {
                Id = Guid.NewGuid(),
                Name = "wow",
                Description = "smth",
                Key = Guid.NewGuid().ToString(),
                Genres = new List<Genre>() {fps, adventure},
                PlatformTypes = new List<PlatformType>() {windows}
            };
            context.Games.Add(wow);

            var hero = new Game
            {
                Id = Guid.NewGuid(),
                Name = "hero",
                Description = "smth",
                Key = Guid.NewGuid().ToString(),
                Genres = new List<Genre>() {puzzleSkill, adventure, subMisc},
                PlatformTypes = new List<PlatformType>() {windows, android, ios}
            };

            context.Games.AddOrUpdate(hero);

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

            context.Comments.AddOrUpdate(cm1);
            context.Comments.AddOrUpdate(cm2);
            context.Comments.AddOrUpdate(cm3);
            context.Comments.AddOrUpdate(cm4);


            context.SaveChanges();
            base.Seed(context);
        }
    }
}
