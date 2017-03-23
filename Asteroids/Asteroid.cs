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

            Vector2f o = new Vector2f(radius, radius);
            shape.Origin = o;

            shape.FillColor = Color.Yellow;
            shape.Position = p;
        }
        public override void Draw(RenderWindow window)
        {
            window.Draw(shape);
        }

        public override void Update(float dt, List<Projectile> listProjectiles)
        {
        
        }

        private void Kinematics(float dt)
        {
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
        public bool ShouldExplode(Projectile p)
        {
            // Circle Circle Collision check
            // The distance between centers is less than sum of radii
            float xDif = shape.Position.X - p.GetPostion().X;
            float yDif = shape.Position.Y - p.GetPostion().Y;
            float sumRadii = radius + p.GetRadius();
            return ((xDif * xDif) + (yDif * yDif)) <= (sumRadii * sumRadii);
        }
    }
}
