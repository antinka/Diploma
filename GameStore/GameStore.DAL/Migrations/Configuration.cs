using System.Collections.Generic;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<GameStore.DAL.EF.GameStoreDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GameStore.DAL.EF.GameStoreDBContext context)
        {
            //var strategy = new Genre
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "Strategy"
            //};

            //var rts = new Genre
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "RTS",
            //    ParentGenreId = strategy.Id
            //};

            //var tbs = new Genre
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "TBS",
            //    ParentGenreId = strategy.Id
            //};

            //context.Genres.AddOrUpdate(rts);
            //context.Genres.AddOrUpdate(tbs);
            //context.Genres.AddOrUpdate(strategy);

            //var races = new Genre
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "Races",
            //};

            //var rally = new Genre
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "rally",
            //    ParentGenreId = races.Id
            //};
            //var arcade = new Genre
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "arcade",
            //    ParentGenreId = races.Id
            //};
            //var formula = new Genre
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "formula",
            //    ParentGenreId = races.Id
            //};
            //var offRoad = new Genre
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "off-road",
            //    ParentGenreId = races.Id
            //};

            //context.Genres.AddOrUpdate(rally);
            //context.Genres.AddOrUpdate(arcade);
            //context.Genres.AddOrUpdate(formula);
            //context.Genres.AddOrUpdate(offRoad);
            //context.Genres.AddOrUpdate(races);

            //var action = new Genre
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "Action"
            //};
            //var fps = new Genre
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "FPS",
            //    ParentGenreId = action.Id
            //};
            //var tps = new Genre
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "TPS",
            //    ParentGenreId = action.Id
            //};
            //var subMisc = new Genre
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "Misc",
            //    ParentGenreId = action.Id
            //};

            //context.Genres.AddOrUpdate(fps);
            //context.Genres.AddOrUpdate(tps);
            //context.Genres.AddOrUpdate(subMisc);
            //context.Genres.AddOrUpdate(action);


            //var rpg = new Genre
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "RPG"
            //};
            //var sports = new Genre
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "Sports"
            //};
            //var adventure = new Genre
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "Adventure"
            //};
            //var puzzleSkill = new Genre
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "Puzzle&Skill"
            //};

            //context.Genres.AddOrUpdate(rpg);
            //context.Genres.AddOrUpdate(sports);
            //context.Genres.AddOrUpdate(adventure);
            //context.Genres.AddOrUpdate(puzzleSkill);

            //var windows = new PlatformType
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "windows"
            //};

            //var android = new PlatformType
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "android"
            //};

            //var ios = new PlatformType
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "ios"
            //};

            //var wow = new Game
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "wow",
            //    Description = "smth",
            //    Key = "wo223w",
            //    PublishDate = DateTime.UtcNow.AddYears(-1),
            //    Views = 140,
            //    Genres = new List<Genre>() { fps, adventure },
            //    PlatformTypes = new List<PlatformType>() { windows }
            //};
            //context.Games.AddOrUpdate(wow);

            //var hero = new Game
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "hero",
            //    Description = "smth",
            //    Key = "hero232",
            //    PublishDate = DateTime.UtcNow.AddYears(-1),
            //    Views = 140,
            //    Genres = new List<Genre>() { puzzleSkill, adventure, subMisc },
            //    PlatformTypes = new List<PlatformType>() { windows, android, ios }

            //};

            //context.Games.AddOrUpdate(hero);

            //var mobile = new PlatformType { Id = Guid.NewGuid(), Name = "Mobile" };
            //var browser = new PlatformType { Id = Guid.NewGuid(), Name = "Browser" };
            //var desktop = new PlatformType { Id = Guid.NewGuid(), Name = "Desktop" };
            //var console = new PlatformType { Id = Guid.NewGuid(), Name = "Console" };

            //context.PlatformTypes.AddOrUpdate(mobile);
            //context.PlatformTypes.AddOrUpdate(browser);
            //context.PlatformTypes.AddOrUpdate(desktop);
            //context.PlatformTypes.AddOrUpdate(console);

            //var p1 = new Publisher()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "test"
            //};
            //context.Publishers.AddOrUpdate(p1);

            //var left4Dead = new Game
            //{
            //    Id = Guid.NewGuid(),
            //    Key = "Key-Left4Dead232",
            //    Name = "Left 4 Dead",
            //    Price = 6,
            //    UnitsInStock = 0,
            //    PublishDate = DateTime.UtcNow.AddYears(-2),
            //    Views = 130,
            //    Genres = new List<Genre>
            //    {
            //        fps
            //    },
            //    PlatformTypes = new List<PlatformType>
            //    {
            //        desktop
            //    },
            //    Publisher = p1,
            //};

            //var cod = new Game
            //{
            //    Id = Guid.NewGuid(),
            //    Key = "Key-CoD232",
            //    Name = "Call of Duty",
            //    Price = 12,
            //    UnitsInStock = 13,
            //    Publisher = p1,
            //    PublishDate = DateTime.UtcNow.AddYears(-1),
            //    Views = 1400,
            //    Genres = new List<Genre>
            //    {
            //        fps
            //    },
            //    PlatformTypes = new List<PlatformType>
            //    {
            //        desktop,
            //        console,
            //        mobile
            //    },
            //};

            //var fifa = new Game
            //{
            //    Id = Guid.NewGuid(),
            //    Key = "Key-FIFA232",
            //    Name = "FIFA",
            //    Price = 20,
            //    UnitsInStock = 5,
            //    PublishDate = DateTime.UtcNow.AddYears(-1),
            //    Views = 140,
            //    Genres = new List<Genre>
            //    {
            //        sports
            //    },
            //    PlatformTypes = new List<PlatformType>
            //    {
            //        desktop,
            //        console,
            //        mobile,
            //        browser
            //    },
            //};

            //var nfs = new Game
            //{
            //    Id = Guid.NewGuid(),
            //    Key = "Key-NFS232",
            //    Name = "Need for Speed",
            //    Price = 15,
            //    UnitsInStock = 17,
            //    PublishDate = DateTime.UtcNow.AddYears(-3),
            //    Views = 190,
            //    PlatformTypes = new List<PlatformType>
            //    {
            //        desktop,
            //        console,
            //        mobile
            //    },
            //    Publisher = p1,
            //};

            //var qqq = new Game
            //{
            //    Id = Guid.NewGuid(),
            //    Key = "Key-WorldOfWarcraft",
            //    Name = "World of Warcraft",
            //    Price = 7,
            //    UnitsInStock = 56,
            //    PublishDate = DateTime.UtcNow.AddYears(-1),
            //    Views = 140,
            //    PlatformTypes = new List<PlatformType>
            //    {
            //        desktop
            //    },
            //};

            //context.Games.AddOrUpdate(left4Dead);
            //context.Games.AddOrUpdate(cod);
            //context.Games.AddOrUpdate(fifa);
            //context.Games.AddOrUpdate(nfs);
            //context.Games.AddOrUpdate(qqq);

            //var left4Dead2 = new Game
            //{
            //    Id = Guid.NewGuid(),
            //    Key = "2Key-Left4Dead232",
            //    Name = "Left 4 Dead",
            //    Price = 6,
            //    UnitsInStock = 0,
            //    PublishDate = DateTime.UtcNow.AddMonths(-1),
            //    Views = 14,
            //    Genres = new List<Genre>
            //    {
            //        rally
            //    },
            //    PlatformTypes = new List<PlatformType>
            //    {
            //        desktop

            //    },
            //};

            //var cod2 = new Game
            //{
            //    Id = Guid.NewGuid(),
            //    Key = "2Key-CoD232",
            //    Name = "Call of Duty",
            //    Price = 12,
            //    UnitsInStock = 13,
            //    PublishDate = DateTime.UtcNow.AddMonths(-1),
            //    Views = 14,
            //    PlatformTypes = new List<PlatformType>
            //    {
            //        desktop
            //    },
            //};

            //var fifa2 = new Game
            //{
            //    Id = Guid.NewGuid(),
            //    Key = "2Key-FIFA232",
            //    Name = "FIFA",
            //    Price = 20,
            //    UnitsInStock = 5,
            //    PublishDate = DateTime.UtcNow.AddMonths(-1),
            //    Views = 140,
            //    PlatformTypes = new List<PlatformType>
            //    {
            //        desktop
            //    },
            //};
            //var cm2 = new Comment()
            //{
            //    Id = Guid.NewGuid(),
            //    Game = wow,
            //    Name = "Viola",
            //    Body = "realy cool"
            //};

            //var nfs2 = new Game
            //{
            //    Id = Guid.NewGuid(),
            //    Key = "2Key-NFS232",
            //    Name = "Need for Speed",
            //    Price = 15,
            //    UnitsInStock = 17,
            //    PublishDate = DateTime.UtcNow.AddMonths(-2),
            //    Views = 104,
            //    PlatformTypes = new List<PlatformType>
            //    {
            //        console,
            //        mobile
            //    },
            //};

            //var cm3 = new Comment()
            //{
            //    Id = Guid.NewGuid(),
            //    Game = wow,
            //    Name = "Vasya",
            //    Body = "why you think so?",
            //    ParentCommentId = cm2.Id
            //};

            //var qqq2 = new Game
            //{
            //    Id = Guid.NewGuid(),
            //    Key = "2Key-WorldOfWarcraft",
            //    Name = "World of Warcraft",
            //    Price = 7,
            //    UnitsInStock = 56,
            //    PublishDate = DateTime.UtcNow.AddMonths(-1),
            //    Views = 1400,
            //    PlatformTypes = new List<PlatformType>
            //        {
            //            desktop
            //},
            //};
            //var cm4 = new Comment()
            //{
            //    Id = Guid.NewGuid(),
            //    Game = hero,
            //    Name = "hero",
            //    Body = "soso"
            //};

            //context.Comments.AddOrUpdate(cm2);
            //context.Comments.AddOrUpdate(cm3);
            //context.Comments.AddOrUpdate(cm4);

            //context.Games.AddOrUpdate(left4Dead2);
            //context.Games.AddOrUpdate(cod2);
            //context.Games.AddOrUpdate(fifa2);
            //context.Games.AddOrUpdate(nfs2);
            //context.Games.AddOrUpdate(qqq2);

            //context.SaveChanges();
            //base.Seed(context);

        }
    }
}
