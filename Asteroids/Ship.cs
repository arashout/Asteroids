using System;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using System.Collections.Generic;

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
        // For out of bounds checks
        private float shipLength;

        // Has the user rotated, thrust, or shot recently
        private bool hasThrust = false;
        private bool hasSpin = false;

        // More specific shooting variables
        private bool isShotCharged = false;
        private bool wantsToShoot = false; // This variable is checked in the game loop
        private Byte shotCounter;
        private const Byte shotChargingTime = 5; //Amount of frames to be wait before shot is charged

        // How much to increase velocity by when key is pressed
        private const float rotationPower = 30;
        private const float thrustPower = 30;

        // Keeping the spaceship velocities reasonable
        private float terminalVelocitySquared = 300*300; // To avoid SQRT
        private float decayRate = .9f;
        private float angularDecayRate = .7f;

        // Note that shape.Rotation acts as angular position
        private float angularVelocity;

        public Ship(Vector2f p, float sideLength)
        {
            shipLength = sideLength;
            shape = new CircleShape(sideLength, 3); // CircleShape(~,3) creates a triangle
            shape.Scale = new Vector2f(0.7f, 1); // Make the ship longer than it is wide

            // Set the center of the shape for convienience later
            shape.Origin = new Vector2f(sideLength, sideLength);

            shape.Position = p;
            velocity = new Vector2f(0, 0);

            shape.FillColor = Color.White;
        }
        public override void Draw(RenderWindow window)
        {
            Edge curEdge = OutOfBoundsEdge(window);
            if (curEdge != Edge.NULL) ResetPosition(curEdge, window); 
            window.Draw(shape);
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
            // Shooting controls
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && isShotCharged) wantsToShoot = true;
            else ChargeShot();
            // Position updates
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
            // NOTE: The reason for the orientation of sin and cos is because shape.Rotation is 0 deg at "90 degrees"
            // Essentially phase shifted by 90 degrees - SUJECT TO CHANGE
            float headingRads = shape.Rotation.degToRads();
            float xComponent = (float) Math.Sin(headingRads) * thrustPower * direction;
            float yComponent = (float) Math.Cos(headingRads) * thrustPower * direction;

            if (velocity.MagnitudeSquared() < terminalVelocitySquared)
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
        /// <summary>
        /// Applies toy kinematic equations to determine current
        /// position and heading of the ship
        /// </summary>
        /// <param name="dt">A small timestep based on framerate</param>
        private void Kinematics(float dt)
        {
            // Update new position and heading
            shape.Position += velocity * dt;
            shape.Rotation += angularVelocity * dt;

            // Decaying Velocities, if the user hasn't recently
            // pressed down the thrust or spin keys then reduce speed
            if (!hasThrust) velocity = velocity * decayRate;
            if (!hasSpin) angularVelocity = angularVelocity * angularDecayRate;

            hasThrust = false;
            hasSpin = false;
        }
        public void Shoot(List<Projectile> listProjectiles)
        {
            // Impart the ships current position and velocity to projectile
            listProjectiles.Add(new Projectile(GetGunPosition(), shape.Rotation));
            // Restart counter
            wantsToShoot = false;
            isShotCharged = false;
            shotCounter = 0;

        }
        /// <summary>
        /// A utility function for collision checks that
        /// returns all the coordinates of each vertex
        /// in absolute frame coordinates
        /// </summary>
        /// <returns></returns>
        public List<Vector2f> GetVertices()
        {
            List<Vector2f> points = new List<Vector2f> { };
            for(Byte i = 0; i < shape.GetPointCount(); i++)
            {
                // Found this on SFML-dev.org
                // Note: That GetPoint gives you only vertices from point of 
                // view of the shape (local), that's why you need transform
                points.Add(shape.Transform.TransformPoint(shape.GetPoint(i)));
            }
            return points;
        }
        private void ChargeShot()
        {
            if (shotCounter >= shotChargingTime) isShotCharged = true;
            else shotCounter++;
            wantsToShoot = false;
        }
        private Vector2f GetGunPosition()
        {
            return shape.Transform.TransformPoint(shape.GetPoint(3));
        }
        /// <summary>
        /// Check if ENTIRE ship is out of bounds and return 
        /// corresponding edge
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        protected override Edge OutOfBoundsEdge(Window window)
        {
            if ((shape.Position.X + shipLength) < 0) return Edge.LEFT;
            else if ((shape.Position.X - shipLength) > window.Size.X) return Edge.RIGHT;
            else if ((shape.Position.Y + shipLength) < 0) return Edge.UP;
            else if ((shape.Position.Y - shipLength) > window.Size.Y) return Edge.DOWN;
            else return Edge.NULL;
        }
        /// <summary>
        /// Reset the ships position to the opposite edge of the one it just left
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="window"></param>
        protected override void ResetPosition(Edge edge, Window window)
        {
            if (edge == Edge.LEFT) shape.Position = new Vector2f(window.Size.X + shipLength, shape.Position.Y);
            else if (edge == Edge.RIGHT) shape.Position = new Vector2f(-shipLength, shape.Position.Y);
            else if (edge == Edge.UP) shape.Position = new Vector2f(shape.Position.X, window.Size.Y + shipLength);
            else shape.Position = new Vector2f(shape.Position.X, -shipLength);
        }

        public bool IsShotCharged
        {
            get
            {
                return isShotCharged;
            }
        }

        public bool WantsToShoot
        {
            get
            {
                return wantsToShoot;
            }
        }
    }
}
