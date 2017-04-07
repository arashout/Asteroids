using SFML.System;
using SFML.Graphics;
using System.Collections.Generic;

namespace Asteroids
{
    /// <summary>
    /// The asteroid class in our asteroids game
    /// Asteroids are represented by plain circles, textures haven't been added
    /// KEY IDEAS:
    /// 1. In general bigger asteroids move slower than smaller ones
    ///     1a. Thus why speed is inversely proportional to radius
    ///     1b. I didn't want situations where the asteroids moves very slowly
    ///         or doesn't move at all due to random generation so I also add
    ///         a base line speed
    /// 2. Collision checks with projectiles and the ship are taken care of with
    ///    this class (arbitrary decision)
    /// </summary>
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
            Vector2f difference = new Vector2f(shape.Position.X - proj.GetPostion().X, shape.Position.Y - proj.GetPostion().Y);
            float sumRadii = radius + proj.Radius;
            return difference.Magnitude() <= sumRadii;
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
