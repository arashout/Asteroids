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
            // If projectile leaves window, mark for deletion
            if (OutOfBoundsEdge(window) != Edge.NULL) isExpired = true;
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

        /// <summary>
        /// Determines if the projectile is COMPLETELY out of bounds and 
        /// return the corresponding EDGE that it is outside of
        /// </summary>
        /// <param name="window"></param>
        /// <returns>an Edge</returns>
        protected override Edge OutOfBoundsEdge(Window window)
        {
            if ((shape.Position.X + radius) < 0) return Edge.LEFT;
            else if ((shape.Position.X - radius) > window.Size.X) return Edge.RIGHT;
            else if ((shape.Position.Y + radius) < 0) return Edge.UP;
            else if ((shape.Position.Y - radius) > window.Size.Y) return Edge.DOWN;
            else return Edge.NULL;
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
