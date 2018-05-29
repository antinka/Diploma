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
            var strategy = new Genre
            {
                Id = Guid.NewGuid(),
                NameEn = "Strategy",
                NameRu = "Стратегия"
            };

            var rts = new Genre
            {
                Id = Guid.NewGuid(),
                NameEn = "RTS",
                NameRu = "Стратегия в реальном времени",
                ParentGenreId = strategy.Id
            };

            var tbs = new Genre
            {
                Id = Guid.NewGuid(),
                NameEn = "TBS",
                NameRu = "Пошаговая стратегия",
                ParentGenreId = strategy.Id
            };

            context.Genres.AddOrUpdate(rts);
            context.Genres.AddOrUpdate(tbs);
            context.Genres.AddOrUpdate(strategy);

            var races = new Genre
            {
                Id = Guid.NewGuid(),
                NameEn = "Races",
                NameRu = "Гонки"
            };

            var rally = new Genre
            {
                Id = Guid.NewGuid(),
                NameEn = "rally",
                NameRu = "Ралли",
                ParentGenreId = races.Id
            };
            var arcade = new Genre
            {
                Id = Guid.NewGuid(),
                NameEn = "arcade",
                NameRu = "Аркада",
                ParentGenreId = races.Id
            };
            var formula = new Genre
            {
                Id = Guid.NewGuid(),
                NameEn = "formula",
                NameRu = "Формула",
                ParentGenreId = races.Id
            };
            var offRoad = new Genre
            {
                Id = Guid.NewGuid(),
                NameEn = "off-road",
                NameRu = "Внедорожние",
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
                NameEn = "Action",
                NameRu = "Экшн"
            };
            var fps = new Genre
            {
                Id = Guid.NewGuid(),
                NameEn = "FPS",
                NameRu = "Шутер от первого лица",
                ParentGenreId = action.Id
            };
            var tps = new Genre
            {
                Id = Guid.NewGuid(),
                NameEn = "TPS",
                NameRu = "Шутер от третьего лица",
                ParentGenreId = action.Id
            };
            var subMisc = new Genre
            {
                Id = Guid.NewGuid(),
                NameEn = "Misc",
                NameRu = "Разное",
                ParentGenreId = action.Id
            };

            context.Genres.AddOrUpdate(fps);
            context.Genres.AddOrUpdate(tps);
            context.Genres.AddOrUpdate(subMisc);
            context.Genres.AddOrUpdate(action);


            var rpg = new Genre
            {
                Id = Guid.NewGuid(),
                NameEn = "RPG",
                NameRu = "Ролевая игра"
            };
            var sports = new Genre
            {
                Id = Guid.NewGuid(),
                NameEn = "Sports",
                NameRu = "Спорт"
            };
            var adventure = new Genre
            {
                Id = Guid.NewGuid(),
                NameEn = "Adventure",
                NameRu = "Приключение"
            };
            var puzzleSkill = new Genre
            {
                Id = Guid.NewGuid(),
                NameEn = "PuzzleAndSkill",
                NameRu = "Головоломки"
            };

            context.Genres.AddOrUpdate(rpg);
            context.Genres.AddOrUpdate(sports);
            context.Genres.AddOrUpdate(adventure);
            context.Genres.AddOrUpdate(puzzleSkill);

            var windows = new PlatformType
            {
                Id = Guid.NewGuid(),
                NameEn = "windows",
            };

            var android = new PlatformType
            {
                Id = Guid.NewGuid(),
                NameEn = "android",
            };

            var ios = new PlatformType
            {
                Id = Guid.NewGuid(),
                NameEn = "ios"
            };

            var wow = new Game
            {
                Id = Guid.NewGuid(),
                NameEn = "wow",
                NameRu = "вов",
                DescriptionEn = "smth",
                Key = "wo223w",
                PublishDate = DateTime.UtcNow.AddYears(-1),
                Views = 140,
                Genres = new List<Genre>() { fps, adventure },
                PlatformTypes = new List<PlatformType>() { windows }
            };
            context.Games.AddOrUpdate(wow);

            var hero = new Game
            {
                Id = Guid.NewGuid(),
                NameEn = "hero",
                NameRu = "герой",
                DescriptionEn = "smth",
                Key = "hero232",
                PublishDate = DateTime.UtcNow.AddYears(-1),
                Views = 140,
                Genres = new List<Genre>() { puzzleSkill, adventure, subMisc },
                PlatformTypes = new List<PlatformType>() { windows, android, ios }

            };

            context.Games.AddOrUpdate(hero);

            var mobile = new PlatformType
            {
                Id = Guid.NewGuid(),
                NameEn = "Mobile",
                NameRu = "Мобильные"
            };
            var browser = new PlatformType
            {
                Id = Guid.NewGuid(),
                NameEn = "Browser",
                NameRu = "Браузерные"
            };
            var desktop = new PlatformType
            {
                Id = Guid.NewGuid(),
                NameEn = "Desktop"
            };
            var console = new PlatformType
            {
                Id = Guid.NewGuid(),
                NameEn = "Console"
            };

            context.PlatformTypes.AddOrUpdate(mobile);
            context.PlatformTypes.AddOrUpdate(browser);
            context.PlatformTypes.AddOrUpdate(desktop);
            context.PlatformTypes.AddOrUpdate(console);

            var p1 = new Publisher()
            {
                Id = Guid.NewGuid(),
                Name = "blizzard"
            };

            var p2 = new Publisher()
            {
                Id = Guid.NewGuid(),
                Name = "Electronic Arts"
            };

            context.Publishers.AddOrUpdate(p1);
            context.Publishers.AddOrUpdate(p2);

            var left4Dead = new Game
            {
                Id = Guid.NewGuid(),
                Key = "Key-Left4Dead232",
                NameEn = "Left 4 Dead",
                NameRu = "Оставленные для мертвых",
                Price = 6,
                UnitsInStock = 0,
                PublishDate = DateTime.UtcNow.AddYears(-2),
                Views = 130,
                Genres = new List<Genre>
                {
                    fps
                },
                PlatformTypes = new List<PlatformType>
                {
                    desktop
                },
                Publisher = p1,
            };

            var cod = new Game
            {
                Id = Guid.NewGuid(),
                Key = "Key-CoD232",
                NameEn = "Call of Duty",
                NameRu = "Чувство долга",
                Price = 12,
                UnitsInStock = 13,
                Publisher = p1,
                PublishDate = DateTime.UtcNow.AddYears(-1),
                Views = 1400,
                Genres = new List<Genre>
                {
                    fps
                },
                PlatformTypes = new List<PlatformType>
                {
                    desktop,
                    console,
                    mobile
                },
            };

            var fifa = new Game
            {
                Id = Guid.NewGuid(),
                Key = "Key-FIFA232",
                NameEn = "FIFA",
                NameRu = "ФИФА",
                Price = 20,
                UnitsInStock = 5,
                PublishDate = DateTime.UtcNow.AddYears(-1),
                Views = 140,
                Genres = new List<Genre>
                {
                    sports
                },
                PlatformTypes = new List<PlatformType>
                {
                    desktop,
                    console,
                    mobile,
                    browser
                },
            };

            var nfs = new Game
            {
                Id = Guid.NewGuid(),
                Key = "Key-NFS232",
                NameEn = "Need for Speed 2",
                NameRu = "Жажда Скорости 2",
                Price = 15,
                UnitsInStock = 17,
                PublishDate = DateTime.UtcNow.AddYears(-3),
                Views = 190,
                PlatformTypes = new List<PlatformType>
                {
                    desktop,
                    console,
                    mobile
                },
                Publisher = p1,
            };

            var qqq = new Game
            {
                Id = Guid.NewGuid(),
                Key = "Key-WorldOfWarcraft",
                NameEn = "World of Warcraft",
                NameRu = "Мир Warcraft",
                Price = 7,
                UnitsInStock = 56,
                PublishDate = DateTime.UtcNow.AddYears(-1),
                Views = 140,
                PlatformTypes = new List<PlatformType>
                {
                    desktop
                },
            };

            context.Games.AddOrUpdate(left4Dead);
            context.Games.AddOrUpdate(cod);
            context.Games.AddOrUpdate(fifa);
            context.Games.AddOrUpdate(nfs);
            context.Games.AddOrUpdate(qqq);

            var left4Dead2 = new Game
            {
                Id = Guid.NewGuid(),
                Key = "2Key-Left4Dead232",
                NameEn = "Left 4 Dead 2",
                NameRu = "Оставленные для мертвых 2",
                Price = 6,
                UnitsInStock = 0,
                PublishDate = DateTime.UtcNow.AddMonths(-1),
                Views = 14,
                Genres = new List<Genre>
                {
                    rally
                },
                PlatformTypes = new List<PlatformType>
                {
                    desktop

                },
            };

            var cod2 = new Game
            {
                Id = Guid.NewGuid(),
                Key = "2Key-CoD232",
                NameEn = "Call of Duty 2",
                NameRu = "Чувство долга 2",
                Price = 12,
                UnitsInStock = 13,
                PublishDate = DateTime.UtcNow.AddMonths(-1),
                Views = 14,
                PlatformTypes = new List<PlatformType>
                {
                    desktop
                },
            };

            var fifa2 = new Game
            {
                Id = Guid.NewGuid(),
                Key = "2Key-FIFA232",
                NameEn = "FIFA 2",
                NameRu = "ФИФА 2",
                Price = 20,
                UnitsInStock = 5,
                PublishDate = DateTime.UtcNow.AddMonths(-1),
                Views = 140,
                PlatformTypes = new List<PlatformType>
                {
                    desktop
                },
            };
            var cm2 = new Comment()
            {
                Id = Guid.NewGuid(),
                Game = wow,
                Name = "Viola",
                Body = "realy cool"
            };

            var nfs2 = new Game
            {
                Id = Guid.NewGuid(),
                Key = "2Key-NFS232",
                NameEn = "Need for Speed",
                NameRu = "Жажда Скорости",
                Price = 15,
                UnitsInStock = 17,
                PublishDate = DateTime.UtcNow.AddMonths(-2),
                Views = 104,
                PlatformTypes = new List<PlatformType>
                {
                    console,
                    mobile
                },
            };

            var cm3 = new Comment()
            {
                Id = Guid.NewGuid(),
                Game = wow,
                Name = "Vasya",
                Body = "why you think so?",
                ParentCommentId = cm2.Id
            };

            var qqq2 = new Game
            {
                Id = Guid.NewGuid(),
                Key = "2Key-WorldOfWarcraft",
                NameEn = "World of Warcraft 2",
                NameRu = "Мир Warcraft 2",
                Price = 7,
                UnitsInStock = 56,
                PublishDate = DateTime.UtcNow.AddMonths(-1),
                Views = 1400,
                PlatformTypes = new List<PlatformType>
                    {
                        desktop
            },
            };
            var cm4 = new Comment()
            {
                Id = Guid.NewGuid(),
                Game = hero,
                Name = "hero",
                Body = "soso"
            };

            context.Comments.AddOrUpdate(cm2);
            context.Comments.AddOrUpdate(cm3);
            context.Comments.AddOrUpdate(cm4);

            context.Games.AddOrUpdate(left4Dead2);
            context.Games.AddOrUpdate(cod2);
            context.Games.AddOrUpdate(fifa2);
            context.Games.AddOrUpdate(nfs2);
            context.Games.AddOrUpdate(qqq2);

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
