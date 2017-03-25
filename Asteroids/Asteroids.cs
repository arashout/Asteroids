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
        List<Projectile> listProjectiles;
        List<Asteroid> listAsteroids;

        List<int> projectileDeletions;
        List<int> asteroidDeletions;

        public Asteroids(uint width, uint height, string title, Color clrColor) : base(width, height, title, clrColor)
        {
            listAsteroids = new List<Asteroid>();
            listProjectiles = new List<Projectile>();

            projectileDeletions = new List<int>();
            asteroidDeletions = new List<int>();
        }

        public override void CleanUp()
        {
            // Clear all lists
            listAsteroids.Clear();
            listProjectiles.Clear();

            asteroidDeletions.Clear();
            projectileDeletions.Clear();

            playerShip = null;
        }

        public override void Init()
        {
            playerShip = new Ship(new Vector2f(window.Size.X / 2, window.Size.Y / 2), 20);

            Vector2f p = new Vector2f(window.Size.X / 3, window.Size.Y / 3);
            Vector2f v = new Vector2f(3, 9);
            listAsteroids.Add(new Asteroid(p, v));

            Console.WriteLine("Asteroids started!");
        }

        public override void Restart()
        {
            Console.WriteLine("Press enter when you are ready to restart");
            Console.Out.Flush();
            Console.ReadLine();
            CleanUp();
            Init();
        }

        public override void Update(RenderWindow window, float dt)
        {
            // Updates playerShip kinematics
            playerShip.Update(dt);
            // Check if player can shoot && wants to shoot
            if (playerShip.WantsToShoot && playerShip.IsShotCharged) playerShip.Shoot(listProjectiles);
            playerShip.Draw(window);

            // For loops to check for collisions between everything
            for (int i = 0; i < listAsteroids.Count; i++)
            {
                Asteroid a = listAsteroids[i]; // Current asteroid for convienience 
                a.Update(dt);
                // Check asteroid collision with ship
                // If true then restart game
                if (a.HasCollided(playerShip))
                {
                    Restart();
                    return;
                }
                // Check asteroid collision with ship projectiles
                for (int j = 0; j < listProjectiles.Count; j++)
                {
                    Projectile p = listProjectiles[j]; // Current projectile for convienience
                    p.Update(dt);
                    if (p.IsExpired) projectileDeletions.Add(j);
                    else if (a.ShouldExplode(p))
                    {
                        projectileDeletions.Add(j);
                        asteroidDeletions.Add(i);
                    }
                    else p.Draw(window);
                }
                a.Draw(window);
            }
            // Delete all expired items
            // Note: reverse for loop messing up list
            for (int i = asteroidDeletions.Count - 1; i >= 0; i--)
            {
                listAsteroids.RemoveAt(asteroidDeletions[i]);
            }
            for (int j = projectileDeletions.Count - 1; j >= 0; j--)
            {
                listProjectiles.RemoveAt(projectileDeletions[j]);
            }
            asteroidDeletions.Clear();
            projectileDeletions.Clear();
        }

    }
}
