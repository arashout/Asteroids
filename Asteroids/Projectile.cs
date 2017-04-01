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
        private const float PROJECTILE_RADIUS = 5;
        private const float BASE_PROJECTILE_SPEED = 10;
        private const float TERMINAL_SPEED = 50;
        private float heading;
        // How many frames the projectile will persist for
        private bool isExpired = false;
        private Byte frameCounter = 0;
        private const Byte MAX_LIFETIME_FRAMES = 30;

        public Projectile(Vector2f p, Vector2f v, float direction)
        {
            id = "P" + count.ToString();
            count++;
            shape = new CircleShape(PROJECTILE_RADIUS);
            shape.Origin = new Vector2f(PROJECTILE_RADIUS, PROJECTILE_RADIUS);
            shape.FillColor = Color.Cyan;
            shape.Position = p;

            // Determine direction of bullet based on ship direction
            heading = direction.degToRads();
            Vector2f components = new Vector2f((float)Math.Sin(heading), (float) Math.Cos(heading)*-1);
            // Scale bullet velocity with ship velocity + base bullet speed
            float scale = v.Magnitude()/100 + BASE_PROJECTILE_SPEED;
            if (scale > TERMINAL_SPEED) scale = TERMINAL_SPEED;
            velocity = components * scale;
        }
        public override void Draw(RenderWindow window)
        {
            // If projectile leaves window, mark for deletion
            if (OutOfBoundsEdge(window, PROJECTILE_RADIUS) != Edge.NULL) isExpired = true;
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
                return PROJECTILE_RADIUS;
            }
        }

        public override void Update(float dt)
        {
            // If lifetime exceeded
            if (frameCounter > MAX_LIFETIME_FRAMES) isExpired = true;
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
