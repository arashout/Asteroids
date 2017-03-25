using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;
using System;
using SFML.Window;

namespace Asteroids
{
    public class Projectile : Entity
    {
        private const float radius = 5;
        private const float bulletSpeed = 10;
        private float heading;
        // How many frames the projectile will persist for
        private bool isExpired = false;
        private Byte frameCounter = 0;
        private const Byte maxFrames = 30;

        public Projectile(Vector2f p, float h)
        {
            shape = new CircleShape(radius);
            shape.Origin = new Vector2f(radius, radius);
            shape.FillColor = Color.Cyan;
            shape.Position = p;

            // Add additionally velocity to bullet in addition to ship velocity
            heading = h.degToRads();
            Vector2f components = new Vector2f((float)Math.Sin(heading), (float) Math.Cos(heading)*-1);
            velocity = components * bulletSpeed;
        }
        public override void Draw(RenderWindow window)
        {
            window.Draw(shape);
        }
        public Vector2f GetPostion()
        {
            return shape.Position;
        }
        public float Radius
        {
            get
            {
                return radius;
            }
        }

        public override void Update(float dt)
        {
            // If lifetime exceeded
            if (frameCounter > maxFrames) isExpired = true;
            frameCounter++;
            // Update new position of projectile
            shape.Position += velocity;
        }

        protected override Edge OutOfBoundsEdge(Window window)
        {
            throw new NotImplementedException();
        }

        protected override void ResetPosition(Edge edge, Window window)
        {
            throw new NotImplementedException();
        }

        public bool IsExpired
        {
            get
            {
                return isExpired;
            }
        }
    }
}
