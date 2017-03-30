using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Asteroids
{
    static class Constants
    {
        public const int MAX_ASTEROID_SIZE = 50;
    }
    class Asteroids : Game
    {
        private Ship playerShip;
        List<Projectile> listProjectiles;
        List<Asteroid> listAsteroids;

        List<int> projectileDeletions;
        List<int> asteroidDeletions;

        // Random object
        Random rnd;

        public Asteroids(uint width, uint height, string title, Color clrColor) : base(width, height, title, clrColor)
        {
            // Initialize Random
            rnd = new Random();

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

            listAsteroids.Add(SpawnAsteroid(window));

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
            CollisionChecks();
            DeletionPhase();
            UpdateAndDraw();
        }
        private Asteroid SpawnAsteroid(RenderWindow window)
        {
            Vector2f p = new Vector2f(800, 800);
            Vector2f v = new Vector2f(rnd.Next(-10, 10), rnd.Next(-10, 10));
            return new Asteroid(p, v);
        }
        private void CollisionChecks()
        {
            // For loops to check for collisions between everything
            for (int i = 0; i < listAsteroids.Count; i++)
            {
                Asteroid a = listAsteroids[i]; // Current asteroid for convienience 
                
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
                    
                    if (p.IsExpired) projectileDeletions.Add(j);
                    else if (a.ShouldExplode(p))
                    {
                        projectileDeletions.Add(j);
                        asteroidDeletions.Add(i);
                        listAsteroids.Add(SpawnAsteroid(window));
                    }
                }
                
            }
        }
        private void DeletionPhase()
        {
            // Delete all expired items
            // Note: reverse for loop to avoid messing up list
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
        private void UpdateAndDraw()
        {
            playerShip.Update(dt);
            // Check if player can shoot && wants to shoot
            if (playerShip.WantsToShoot && playerShip.IsShotCharged) playerShip.Shoot(listProjectiles);
            playerShip.Draw(window);

            foreach (Asteroid a in listAsteroids)
            {
                a.Update(dt);
                a.Draw(window);
            }
            foreach(Projectile p in listProjectiles)
            {
                p.Update(dt);
                p.Draw(window);
            }
        }
    }
}
