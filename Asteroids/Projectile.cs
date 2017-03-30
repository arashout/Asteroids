using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;
using System;
using SFML.Window;

namespace Asteroids
{
    public class Projectile : Entity
    {
        private static int count = 0;
        private const float radius = 5;
        private const float baseProjectileSpeed = 10;
        private const float terminalSpeed = 50;
        private float heading;
        // How many frames the projectile will persist for
        private bool isExpired = false;
        private Byte frameCounter = 0;
        private const Byte maxFrames = 30;

        public Projectile(Vector2f p, Vector2f v, float direction)
        {
            id = "P" + count.ToString();
            count++;
            shape = new CircleShape(radius);
            shape.Origin = new Vector2f(radius, radius);
            shape.FillColor = Color.Cyan;
            shape.Position = p;

            // Determine direction of bullet based on ship direction
            heading = direction.degToRads();
            Vector2f components = new Vector2f((float)Math.Sin(heading), (float) Math.Cos(heading)*-1);
            // Scale bullet velocity with ship velocity + base bullet speed
            float scale = v.Magnitude()/100 + baseProjectileSpeed;
            if (scale > terminalSpeed) scale = terminalSpeed;
            velocity = components * scale;
        }
        public override void Draw(RenderWindow window)
        {
            // If projectile leaves window, mark for deletion
            if (OutOfBoundsEdge(window, radius) != Edge.NULL) isExpired = true;
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
        public bool IsExpired
        {
            get
            {
                return isExpired;
            }
        }
    }
}
