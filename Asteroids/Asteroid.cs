using System;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using System.Collections.Generic;

namespace Asteroids
{
    class Asteroid : Entity
    {
        private float radius;
        private const float unscaledSpeed = 25;
        private float scaledSpeed;

        public Asteroid(Vector2f p, Vector2f v, uint r = 25)
        {
            // Asteroid default
            radius = r;
            scaledSpeed = unscaledSpeed / r;
            shape = new CircleShape(radius);

            velocity = v;
            Vector2f o = new Vector2f(radius, radius);
            shape.Origin = o;

            shape.FillColor = Color.Yellow;
            shape.Position = p;
        }
        public override void Draw(RenderWindow window)
        {
            // Check if out of bounds
            Edge curEdge = OutOfBoundsEdge(window);
            if (curEdge != Edge.NULL) ResetPosition(curEdge, window);

            window.Draw(shape);
        }

        public override void Update(float dt)
        {
            // Position updates
            Kinematics(dt);
        }

        private void Kinematics(float dt)
        {
            shape.Position += velocity;
        }
        public Vector2f getCenterVertex()
        {
            return shape.Position;
        }
        /// <summary>
        /// Checks if an asteroid has collided with ship s
        /// </summary>
        /// <param name="s">A ship</param>
        /// <returns></returns>
        public bool HasCollided(Ship s)
        {
            // CHECK 1 : Triangle vertex within circle
            List<Vector2f> shipVertices = s.GetVertices();
            Vector2f c;
            foreach (Vector2f p in shipVertices)
            {
                c = p - shape.Position;
                if (c.MagnitudeSquared() <= (radius * radius) ) return true;
            }
            return false;
        }
        /// <summary>
        /// Checks if a projectile has collided with this asteroid
        /// </summary>
        /// <param name="proj"></param>
        /// <returns></returns>
        public bool ShouldExplode(Projectile proj)
        {
            // Circle Circle Collision check
            // The distance between centers is less than sum of radii
            float xDif = shape.Position.X - proj.GetPostion().X;
            float yDif = shape.Position.Y - proj.GetPostion().Y;
            float sumRadii = radius + proj.Radius;
            return ((xDif * xDif) + (yDif * yDif)) <= (sumRadii * sumRadii);
        }
        /// <summary>
        /// Determines if the asteroid is COMPLETELY out of bounds and 
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
            if (edge == Edge.LEFT) shape.Position = new Vector2f(window.Size.X + radius, shape.Position.Y);
            else if (edge == Edge.RIGHT) shape.Position = new Vector2f(-radius, shape.Position.Y);
            else if (edge == Edge.UP) shape.Position = new Vector2f(shape.Position.X, window.Size.Y + radius);
            else shape.Position = new Vector2f(shape.Position.X, -radius);
        }
    }
}
