using System.Collections.Generic;

namespace BatwingShooter.Data
{
    public interface IHighscoreStorage
    {
        void Add(PlayerHighscore highscore);
        void Save();

        IEnumerable<PlayerHighscore> Highscores { get; }
    }
}
