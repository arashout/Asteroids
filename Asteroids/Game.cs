using System;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Asteroids
{
    abstract class Game
    {
        // protected means child classes have access
        protected RenderWindow window;
        protected Color clearColor;

        // Timing 
        protected const uint FRAMERATE = 30;
        protected float dt = 1.0f / FRAMERATE;

        public Game(uint width, uint height, String title, Color clrColor)
        {
            // Create the main window
            window = new RenderWindow(new VideoMode(width, height), title);

            window.SetFramerateLimit(FRAMERATE);

            // Set color for window background
            clearColor = clrColor;

            // Setup up event handlers - Using delegates <- Read up on this
            window.Closed += Window_Closed;
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
                
                // Update the game
                Update(window,dt);

                // Update the window
                window.Display();
            }
            // End of game loop
        }
        public abstract void Init();
        public abstract void Update(RenderWindow window, float dt);
    }
}
