using System;
using SFML.System;
using SFML.Window;
using SFML.Graphics;


namespace Asteroids
{
    /// <summary>
    /// Class for the ship that the player will be controlling.
    /// All ships are initialized with zero velocity
    /// Args:
    ///     p(Vector2f) : the position that the ship is starting at
    ///     sideLength(float) : The side length of the triangle that makes up the ship
    /// </summary> 
    class Ship : Entity
    {
        // Has the user rotated or thrust recently
        private bool hasThrust = false;
        private bool hasSpin = false;

        // How much to increase velocity by when key is pressed
        private const float rotationPower = 30;
        private const float thrustPower = 30;

        private float terminalVelocitySquared = 30000;
        private float decayRate = .9f;

        private float heading;
        private float angularVelocity;

        public Ship(Vector2f p, float sideLength)
        {
           
            shape = new CircleShape(sideLength, 3); // CircleShape(~,3) creates a triangle
            shape.Scale = new Vector2f(0.7f, 1); // Make the ship longer than it is wide

            // Set the center of the shape for convienience later
            Vector2f o = new Vector2f(sideLength, sideLength);
            shape.Origin = o;

            position = p;
            shape.Position = p;
            velocity = new Vector2f(0, 0);
            heading = shape.Rotation; // Initialize heading (degrees)

            shape.FillColor = Color.White;
            
        }
        public override void Draw(RenderWindow window)
        {
            window.Draw(shape);
        }

        /// <summary>
        /// Checks if the ship has collided with a given asteroid "a"
        /// </summary>
        /// <param name="a">an asteroid</param>
        /// <returns></returns>
        public bool HasCollided(Asteroid a)
        {
            return true;
        }
        

        /// <summary>
        /// The ship update function applies thrust & rotation if applicable
        /// (User has pressed the appriopiate keys) and then applies the 
        /// kinematic equations to change the ships position
        /// </summary>
        /// <param name="dt">The elapsed time, used in kinematic equations</param>
        public override void Update(float dt)
        {
            // Thrust controls
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up)) Thrust(-1);
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Down)) Thrust(1);
            // Rotation controls
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right)) Rotate(1);
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Left)) Rotate(-1);
            Kinematics(dt);
        }
        /// <summary>
        /// Applies a thrust in the direction that the ship
        /// is currently facing, forward and backward depends on the
        /// direction parameter
        /// </summary>
        /// <param name="direction">
        /// Dictates whether the ship moves backward or forward 
        /// </param>
        public void Thrust(sbyte direction)
        {
            
            float headingRads = heading.degToRads();
            float xComponent = (float) Math.Sin(headingRads) * thrustPower * direction;
            float yComponent = (float) Math.Cos(headingRads) * thrustPower * direction;

            if (velocity.magnitudeSquared() < terminalVelocitySquared)
            {
                velocity = new Vector2f(velocity.X - xComponent, velocity.Y + yComponent);
                hasThrust = true;
            }
        }
        public void Rotate(sbyte direction)
        {
            angularVelocity += rotationPower * direction;
            hasSpin = true;
        }
        private void Kinematics(float dt)
        {
            position += velocity * dt;
            heading += angularVelocity * dt;

            shape.Position = position;
            shape.Rotation = heading;

            // Decaying Velocities, if the user hasn't recently
            // pressed down the thrust or spin keys then reduce speed
            if (!hasThrust) velocity = velocity * decayRate;
            if (!hasSpin) angularVelocity = angularVelocity * decayRate;

            hasThrust = false;
            hasSpin = false;
        }
    }
}
