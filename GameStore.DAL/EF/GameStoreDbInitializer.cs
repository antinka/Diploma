using GameStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.EF
{
    public class GameStoreDbInitializer : DropCreateDatabaseIfModelChanges<GameStoreContext>
    {
        protected override void Seed(GameStoreContext db)
        {
            Genre Strategy = new Genre("Strategy");
            Genre RTS = new Genre("RTS", Strategy.Id);
            Genre TBS = new Genre("TBS", Strategy.Id);

            db.Genres.Add(RTS);
            db.Genres.Add(TBS);
            db.Genres.Add(Strategy);

            Genre Races = new Genre("Races");


            Genre rally = new Genre("rally", Races.Id);
            Genre arcade = new Genre("arcade", Races.Id);
            Genre formula = new Genre("formula", Races.Id);
            Genre offRoad = new Genre("off-road", Races.Id);

            db.Genres.Add(rally);
            db.Genres.Add(arcade);
            db.Genres.Add(formula);
            db.Genres.Add(offRoad);
            db.Genres.Add(Races);

            Genre Action = new Genre("Action");
            Genre FPS = new Genre("FPS", Action.Id);
            Genre TPS = new Genre("TPS", Action.Id);
            Genre SubMisc = new Genre("Misc", Action.Id);

            db.Genres.Add(FPS);
            db.Genres.Add(TPS);
            db.Genres.Add(SubMisc);
            db.Genres.Add(Action);

            Genre RPG = new Genre("RPG");
            Genre Sports = new Genre("Sports");
            Genre Adventure = new Genre("Adventure");
            Genre PuzzleSkill = new Genre("Puzzle&Skill");
            db.Genres.Add(RPG);
            db.Genres.Add(Sports);
            db.Genres.Add(Adventure);
            db.Genres.Add(PuzzleSkill);

            PlatformType windows = new PlatformType("windows");
            PlatformType android = new PlatformType("android");
            PlatformType ios = new PlatformType("ios");

            Game wow = new Game("wow", "smth");
            wow.Genres = new List<Genre>() { FPS, Adventure };
            wow.PlatformTypes = new List<PlatformType>() { windows };
            db.Games.Add(wow);

            Game hero = new Game("hero", "smth");
            hero.Genres = new List<Genre>() { PuzzleSkill, Adventure, SubMisc };
            hero.PlatformTypes = new List<PlatformType>() { windows, android, ios };
            db.Games.Add(hero);


            Comment cm1 = new Comment(wow, "Vova", "cool");
            Comment cm2 = new Comment(wow, "Viola", "realy cool");
            Comment cm3 = new Comment(wow, "Vasya", "why you think so?", cm1.Id);
            Comment cm4 = new Comment(hero, "Vova", "soso");
            db.Comments.Add(cm1);
            db.Comments.Add(cm2);
            db.Comments.Add(cm3);
            db.Comments.Add(cm4);



            Genre q = new Genre("q");
            db.Genres.Add(q);
            Genre q1 = new Genre("q123", q.Id);
            db.Genres.Add(q1);

            db.SaveChanges();
        }
    }
}
