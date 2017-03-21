using System;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Asteroids
{

    class Asteroids : Game
    {
        private Ship playerShip;

        public Asteroids(uint width, uint height, string title, Color clrColor) : base(width, height, title, clrColor)
        {
        }

        public override void Init()
        {
            Vector2f p = new Vector2f(window.Size.X / 2, window.Size.Y / 2);
            playerShip = new Ship(p, 20);
            Console.WriteLine("Asteroids started!");
        }

        public override void Update(RenderWindow window, float dt)
        {
            playerShip.Update(dt);
            playerShip.Draw(window);
        }
    }
}
