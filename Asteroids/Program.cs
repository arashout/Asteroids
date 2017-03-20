using System;
using SFML.Window;
using SFML.Graphics;
using Asteroids;

namespace Asteroids
{

    static class Program
    {

        static void Main()
        {
            Asteroids asteroidGame = new Asteroids(800, 800, "Asteroids", Color.Black);
            asteroidGame.Run();
        }
    }
}