using System;
using System.Collections.Generic;
using BatwingShooter.GameObjects;
using BatwingShooter.Misc;

namespace BatwingShooter.Renderer
{
    public interface IRenderer
    {
        int ScreenWidth { get; }

        int ScreenHeight { get; }

        void Draw(params GameObject[] gameObjects);

        void Clear();

        event EventHandler<KeyDownEventArgs> UiActionHappened;
    }
}