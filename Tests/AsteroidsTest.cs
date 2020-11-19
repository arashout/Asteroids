using Asteroids;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace Tests
{
    /// <summary>
    /// Summary description for AsteroidsTest
    /// </summary>
    [TestClass]
    public class AsteroidsTest
    {
        private Asteroids.Asteroids _game;

        public AsteroidsTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        [TestInitialize]
        public void GameInitialize()
        {
            _game = new Asteroids.Asteroids(800, 800, "Test", Color.Black);
        }

        [TestCleanup]
        public void GameCleanUp()
        {
            var game = new PrivateObject(_game);

            var window = (RenderWindow)game.GetField("window");

            _game.CleanUp();

            window.Close();

            window.Dispose();

            _game = null;
        }
        #endregion

        [TestMethod]
        public void CleanUpTest()
        {
            MockData();

            _game.CleanUp();

            var game = new PrivateObject(_game);

            var asteroids = (Dictionary<string, Asteroid>)game.GetField("dictAsteroids");
            var player = (Ship)game.GetField("playerShip");
            var projectiles = (Dictionary<string, Projectile>)game.GetField("dictProjectiles");
            var score = (int)game.GetField("score");

            Assert.IsTrue(
                asteroids.Count == 0 && player is null && projectiles.Count == 0 && score == 0,
                "Failed to clean up."
            );
        }

        [TestMethod]
        public void CollisionChecksTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void DeletionPhaseTest()
        {
            MockData();

            var game = new PrivateObject(_game);

            var asteroidDeletions = (HashSet<string>)game.GetField("asteroidDeletions");
            var asteroids = (Dictionary<string, Asteroid>)game.GetField("dictAsteroids");
            var projectileDeletions = (HashSet<string>)game.GetField("projectileDeletions");
            var projectiles = (Dictionary<string, Projectile>)game.GetField("dictProjectiles");

            Assert.IsTrue(
                asteroidDeletions.Count == 0 && projectileDeletions.Count == 0,
                "Failed to initialize data."
            );

            foreach (var asteroidId in asteroids.Keys)
            {
                asteroidDeletions.Add(asteroidId);
            }

            foreach (var projectileId in projectiles.Keys)
            {
                projectileDeletions.Add(projectileId);
            }

            Assert.IsTrue(
                asteroidDeletions.Count > 0 && projectileDeletions.Count > 0,
                "Failed to initialize data."
            );

            game.Invoke("DeletionPhase");

            Assert.IsTrue(
                asteroidDeletions.Count == 0 && asteroids.Count == 0 && projectileDeletions.Count == 0 && projectiles.Count == 0,
                "Failed to delete elements."
            );
        }

        [TestMethod]
        public void InitTest()
        {
            var game = new PrivateObject(_game);

            var player = (Ship)game.GetField("playerShip");

            Assert.IsNull(player, "Failed to initalize data.");

            _game.Init();

            player = (Ship)game.GetField("playerShip");

            Assert.IsNotNull(player, "Failed to initialize game.");
        }

        [TestMethod]
        public void RestartTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void RunTest()
        {
            throw new NotImplementedException();
        }

        [DataTestMethod]
        [DataRow(Edge.LEFT)]
        [DataRow(Edge.RIGHT)]
        [DataRow(Edge.UP)]
        [DataRow(Edge.DOWN)]
        [DataRow(Edge.NULL)]
        public void SpawnAsteroidTest(Edge edge)
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void SpawnAsteroidTest(Vector2f pos, int maxRadius)
        {
            // TODO: for loop to test the other spawnAsteroid method
            throw new NotImplementedException();
        }

        [TestMethod]
        public void SpawningPhaseTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void UpdateTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void UpdateAndDrawPhaseTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void UpdateScoreTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Window_ClosedTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Window_KeyPressedTest()
        {
            throw new NotImplementedException();
        }

        private void MockData()
        {
            var game = new PrivateObject(_game);

            _game.Init();

            game.SetField("score", 1000);

            var asteroids = (Dictionary<string, Asteroid>)game.GetField("dictAsteroids");
            var player = (Ship)game.GetField("playerShip");
            var projectiles = (Dictionary<string, Projectile>)game.GetField("dictProjectiles");
            var score = (int)game.GetField("score");

            var asteroid = (Asteroid)game.Invoke("SpawnAsteroid", Edge.LEFT);
            asteroids.Add(asteroid.Id, asteroid);

            asteroid = (Asteroid)game.Invoke("SpawnAsteroid", Edge.RIGHT);
            asteroids.Add(asteroid.Id, asteroid);

            asteroid = (Asteroid)game.Invoke("SpawnAsteroid", Edge.UP);
            asteroids.Add(asteroid.Id, asteroid);

            asteroid = (Asteroid)game.Invoke("SpawnAsteroid", Edge.DOWN);
            asteroids.Add(asteroid.Id, asteroid);

            player.Shoot(projectiles);
            player.Shoot(projectiles);
            player.Shoot(projectiles);
            player.Shoot(projectiles);

            Assert.IsTrue(
                asteroids.Count > 0 && player is object && projectiles.Count > 0 && score > 0,
                "Failed to mock data."
            );
        }
    }
}
