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
            Edge curEdge = OutOfBoundsEdge(window, radius);
            if (curEdge != Edge.NULL) ResetPosition(curEdge, window, radius);

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
    }
}
