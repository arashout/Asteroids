using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Asteroids
{

    class Asteroids : Game
    {
        private Ship playerShip;
        private Asteroid a1;
        List<Projectile> listProjectiles;
        List<Asteroid> listAsteroids;

        public Asteroids(uint width, uint height, string title, Color clrColor) : base(width, height, title, clrColor)
        {
        }

        public override void Init()
        {
            playerShip = new Ship(new Vector2f(window.Size.X / 2, window.Size.Y / 2), 20);

            Vector2f p = new Vector2f(window.Size.X / 3, window.Size.Y / 3);
            Vector2f v = new Vector2f(0, 0);
            a1 = new Asteroid(p, v, 20);
            listAsteroids = new List<Asteroid> ();
            listProjectiles = new List<Projectile>();
            Console.WriteLine("Asteroids started!");
        }

        public override void Update(RenderWindow window, float dt)
        {
            playerShip.Update(dt, listProjectiles);
            playerShip.Draw(window);
            foreach(Projectile proj in listProjectiles)
            {
                if (a1.ShouldExplode(proj)) Console.WriteLine("Asteroid should die");

                proj.Update(dt, listProjectiles);
                proj.Draw(window);
            }
            if (a1.HasCollided(playerShip) == true)
            {
                Console.WriteLine("Press enter when you are ready to restart");
                Console.Out.Flush();
                Console.ReadLine();
                Init();
                return;
            }
            
            a1.Update(dt, listProjectiles);
            a1.Draw(window);
        }
    }
}
