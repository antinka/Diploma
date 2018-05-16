namespace GameStore.DAL.Migrations
{
    using GameStore.DAL.Entities;
    using System;
    using System.Collections.Generic;
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
            //    PublishDate = DateTime.UtcNow.AddYears(-1),
            //    Key = "wo223w",
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
            //    PublishDate = DateTime.UtcNow.AddYears(-3),
            //    Genres = new List<Genre>() { puzzleSkill, adventure, subMisc },
            //    PlatformTypes = new List<PlatformType>() { windows, android, ios }

            //};

            //context.Games.AddOrUpdate(hero);

           

            //var mobile = new PlatformType { Id = Guid.NewGuid(), Name = "Mobile" };
            //var browser = new PlatformType { Id = Guid.NewGuid(), Name = "Browser" };
            //var desktop = new PlatformType { Id = Guid.NewGuid(), Name = "Desktop" };
            //var console = new PlatformType { Id = Guid.NewGuid(), Name = "Console" };

            //context.PlatformTypes.Add(mobile);
            //context.PlatformTypes.Add(browser);
            //context.PlatformTypes.Add(desktop);
            //context.PlatformTypes.Add(console);

            //var left4Dead = new Game
            //{
            //    Id = Guid.NewGuid(),
            //    Key = "Key-Left4Dead232",
            //    Name = "Left 4 Dead",
            //    Price = 6,
            //    UnitsInStock = 0,
            //    Genres = new List<Genre>
            //    {
            //        fps
            //    },
            //    PublishDate = DateTime.UtcNow.AddYears(-1),
            //    PlatformTypes = new List<PlatformType>
            //    {
            //        desktop
            //    },
            //    Views = 300
            //};
            //var cod = new Game
            //{
            //    Id = Guid.NewGuid(),
            //    Key = "Key-CoD232",
            //    Name = "Call of Duty",
            //    Price = 12,
            //    UnitsInStock = 13,
            //    Genres = new List<Genre>
            //    {
            //        fps
            //    },
            //    PublishDate = DateTime.UtcNow.AddYears(-1),
            //    PlatformTypes = new List<PlatformType>
            //    {
            //        desktop,
            //        console,
            //        mobile
            //    },
            //    Views = 100
            //};
            //var fifa = new Game
            //{
            //    Id = Guid.NewGuid(),
            //    Key = "Key-FIFA232",
            //    Name = "FIFA",
            //    Price = 20,
            //    UnitsInStock = 5,
            //    Genres = new List<Genre>
            //    {
            //        sports
            //    },
            //    PublishDate = DateTime.UtcNow.AddYears(-1),
            //    PlatformTypes = new List<PlatformType>
            //    {
            //        desktop,
            //        console,
            //        mobile,
            //        browser
            //    },
            //    Views = 77
            //};
            //var nfs = new Game
            //{
            //    Id = Guid.NewGuid(),
            //    Key = "Key-NFS232",
            //    Name = "Need for Speed",
            //    Price = 15,
            //    UnitsInStock = 17,
            //    PublishDate = DateTime.UtcNow.AddYears(-1),
            //    PlatformTypes = new List<PlatformType>
            //    {
            //        desktop,
            //        console,
            //        mobile
            //    },
            //    Views = 140
            //};
            //var qqq = new Game
            //{
            //    Id = Guid.NewGuid(),
            //    Key = "Key-WorldOfWarcraft",
            //    Name = "World of Warcraft",
            //    Price = 7,
            //    UnitsInStock = 56,
            //    PublishDate = DateTime.UtcNow.AddYears(-1),
            //    PlatformTypes = new List<PlatformType>
            //    {
            //        desktop
            //    },
            //    Views = 95
            //};


            //context.Games.AddOrUpdate(left4Dead);
            //context.Games.AddOrUpdate(cod);
            //context.Games.AddOrUpdate(fifa);
            //context.Games.AddOrUpdate(nfs);
            //context.Games.AddOrUpdate(qqq);


            context.SaveChanges();
            base.Seed(context);
        }
    }
}

