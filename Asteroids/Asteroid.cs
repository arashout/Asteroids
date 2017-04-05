using System;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using System.Collections.Generic;

namespace Asteroids
{
    class Asteroid : Entity
    {
        // Static variable for unique ID creation
        private static long count = 0;

        private float radius;
        private const float BASE_LINE_SPEED = 5;
        private const float MIN_BREAK_APART_RADIUS = 30;

        public float Radius { get => radius; }

        public Asteroid(Vector2f p, Vector2f v, int r)
        {
            // ID creation
            this.Id = "A" + count.ToString();
            count++;

            // Getting asteroid speed based on radius
            radius = r;
            // Bigger asteroids should move slower
            velocity = v / radius + v / v.Magnitude() * BASE_LINE_SPEED;

            Vector2f o = new Vector2f(radius, radius);
            shape = new CircleShape(radius);
            shape.Origin = o;
            // Colours
            shape.FillColor = Color.Black;
            shape.OutlineColor = Color.Yellow;
            shape.OutlineThickness = -2;
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
                if (c.DotProduct(c) <= (radius * radius)) return true;
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
        /// Checks if the asteroid will spawn children when destroyed
        /// </summary>
        /// <returns></returns>
        public bool WillBreakApart()
        {
            if (radius > MIN_BREAK_APART_RADIUS)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Useful for spawning child asteroids
        /// </summary>
        /// <returns></returns>
        public Vector2f GetPosition()
        {
            return shape.Position;
        }
    }
}
