using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;
using System;

namespace Asteroids
{
    public class Projectile : Entity
    {
        private const float radius = 5;
        private const float bulletSpeed = 10;
        private float heading;

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
        public float GetRadius()
        {
            return radius;
        }
        public override void Update(float dt, List<Projectile> listProjectiles)
        {
            // Update new position of projectile
            shape.Position += velocity;
        }
    }
}
