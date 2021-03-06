﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatwingShooter.Misc
{
    public enum GameCommand
    {
        MoveUp,
        MoveDown,
        Fire,
        MoveLeft,
        MoveRight,
        PlayPause
    }

    public class KeyDownEventArgs : EventArgs
    {
        public GameCommand Command { get; set; }

        public KeyDownEventArgs(GameCommand command)
        {
            Command = command;
        }
    }
}
