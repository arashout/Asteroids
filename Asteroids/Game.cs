using System;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Asteroids
{
    // To determine out of bounds edge
    // NULL edge represents inside of screen
    public enum Edge { UP, DOWN, LEFT, RIGHT, NULL }

    abstract class Game
    {
        protected RenderWindow window;
        protected Color clearColor;

        // Timing variables
        protected const uint FRAMERATE = 30;
        protected float dt = 1.0f / FRAMERATE;
        protected bool isPaused = false;

        // Main menu text, and default font settings
        protected Text menuText;
        protected Font font;
        protected const uint FONT_SIZE = 30;

        public Game(uint width, uint height, String title, Color clrColor)
        {
            // Create the main window
            window = new RenderWindow(new VideoMode(width, height), title);

            window.SetFramerateLimit(FRAMERATE);

            // Set color for window background
            clearColor = clrColor;

            // Setup up event handlers - Using delegates <- Read up on this
            window.Closed += Window_Closed;
            window.KeyPressed += Window_KeyPressed;

            // Font for menu text
            font = new Font(@"Roboto-Bold.ttf");
            menuText = new Text("Game Paused", font); // This will be the default text
            menuText.CharacterSize = FONT_SIZE;
            menuText.Color = Color.White;
            // Approximately center based on font size
            menuText.Position = new Vector2f(window.Size.X / 2 - FONT_SIZE * 3, window.Size.Y / 2 - FONT_SIZE * 2);
        }

        private void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            if ((e.Code == Keyboard.Key.Escape))
            {
                if (!isPaused)
                {
                    isPaused = true;
                }
                else if (isPaused)
                {
                    isPaused = false;
                }
            }
        }

        protected void Window_Closed(object sender, EventArgs e)
        {
            window.Close();
        }

        public void Run()
        {
            Init();
            // Start of game loop
            while (window.IsOpen)
            {
                // Process events - keypress, mouse movement & clicks
                window.DispatchEvents();

                // Clear screen - to pre-determined color
                window.Clear(clearColor);

                // Update the game if player hasn't paused
                if (isPaused) window.Draw(menuText);
                else Update(window, dt);
                // Update the window
                window.Display();


            }
            // End of game loop
        }
        public abstract void Init();
        public abstract void CleanUp();
        public abstract void Restart();
        /// <summary>
        /// This is where the game logic happens
        /// Window is passed so that we can draw shapes
        /// And dt is passed for calculating kinematics (movement)
        /// Note: for smooth movement key presses are taken care of in here
        /// instead of using events
        /// </summary>
        /// <param name="window"></param>
        /// <param name="dt"></param>
        public abstract void Update(RenderWindow window, float dt);
    }
}
