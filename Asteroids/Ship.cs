using System;
using SFML.System;
using SFML.Window;
using SFML.Graphics;


namespace Asteroids
{
    class Ship : Entity
    {
        // How much to increase velocity by when key is pressed
        private const float rotationPower = 5;
        private const float thrustPower = 1;

        // Need angular vectors for spinning and rotation, but don't need vectors since 2D
        private float angularVelocity;
        private float angularHeading;


        public Ship(Vector2f p, float sideLength)
        {
            // A circle shape with 3 sides allows us to create a triangle
            shape = new CircleShape(sideLength, 3);
            shape.Scale = new Vector2f(1, 2); // Make the ship length twice that of width

            // Set the center of the shape for convienience later
            Vector2f o = new Vector2f(sideLength, sideLength);
            shape.Origin = o;

            // Kinematics 
            position = p;
            // Ship starts off with no velocity or acceleration
            velocity = new Vector2f(0, 0); 
            // Rotation Kinematics
            angularHeading = shape.Rotation; // In degrees
            angularVelocity = 0;

            shape.FillColor = Color.White;
            shape.Position = p;
        }
        public override void Draw(RenderWindow window)
        {
            window.Draw(shape);
        }

        public override bool entityCollision(Entity e)
        {
            throw new NotImplementedException();
        }

        public override void Update(float dt)
        {
            Kinematics(dt);
        }
        public void Thrust(sbyte direction)
        {
            Console.WriteLine("Thrusting");
        }
        public void Rotate(sbyte direction)
        {
            Console.WriteLine("Rotating");
            angularVelocity += rotationPower * direction;
        }
        private void Kinematics(float dt)
        {
            // Rotation
            angularHeading += angularVelocity * dt;
            shape.Rotation = angularHeading;

        }
        public override bool wallCollision(RenderWindow window)
        {
            throw new NotImplementedException();
        }
    }
}
