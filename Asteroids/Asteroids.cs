using System;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Asteroids
{

    class Asteroids : Game
    {
        private Ship playerShip;
        private Asteroid a1;

        public Asteroids(uint width, uint height, string title, Color clrColor) : base(width, height, title, clrColor)
        {
        }

        public override void Init()
        {
            playerShip = new Ship(new Vector2f(window.Size.X / 2, window.Size.Y / 2), 20);

            Vector2f p = new Vector2f(window.Size.X / 3, window.Size.Y / 3);
            Vector2f v = new Vector2f(2, 2);
            a1 = new Asteroid(p, v);
            Console.WriteLine("Asteroids started!");
        }

        public override void Update(RenderWindow window, float dt)
        {
            playerShip.Update(dt);
            playerShip.Draw(window);
            a1.Update(dt);
            a1.Draw(window);
        }
    }
}
