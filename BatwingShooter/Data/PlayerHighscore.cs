using System;

namespace BatwingShooter.Data
{
    public class PlayerHighscore
    {
        private string nickname;
        public int Score { get; set; }

        public string Nickname
        {
            get
            {
                return nickname;
            }
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 3 || value.Length > 15)
                {
                    throw new ArgumentOutOfRangeException("The nickname must be a string between 3 and 15 characters");
                }
                nickname = value;
            }
        }


        public PlayerHighscore()
        {

        }

        public PlayerHighscore(string nickname, int score)
            : this()
        {
            Score = score;
            Nickname = nickname;
        }
    }
}
