using System;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Asteroids
{

    class Asteroids : Game
    {
        private Ship playerShip;
        private float dt;
        private bool up, down, right, left;

        public Asteroids(uint width, uint height, string title, Color clrColor) : base(width, height, title, clrColor)
        {
            dt = (float) 1.0 / framerate;
        }

        public override void Init()
        {
            Vector2f p = new Vector2f(window.Size.X / 2, window.Size.Y / 2);
            playerShip = new Ship(p, 20);
            Console.WriteLine("Asteroids started!");
        }

        public override void Update(RenderWindow window)
        {
            playerShip.Update(dt);
            playerShip.Draw(window);
        }

        protected override void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            up = Keyboard.IsKeyPressed(Keyboard.Key.Up);
            down = Keyboard.IsKeyPressed(Keyboard.Key.Down);
            right = Keyboard.IsKeyPressed(Keyboard.Key.Right);
            left = Keyboard.IsKeyPressed(Keyboard.Key.Left);

            if (up) playerShip.Thrust(1);
            else if (down) playerShip.Thrust(-1);

            if (right) playerShip.Rotate(1);
            else if (left) playerShip.Rotate(-1);
        }
    }
}
