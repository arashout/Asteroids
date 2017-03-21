using System;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

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

            shape.FillColor = Color.Yellow;
            shape.Position = p;
        }
        public override void Draw(RenderWindow window)
        {
            window.Draw(shape);
        }

        public override void Update(float dt)
        {
            Vector2f p = new Vector2f(position.X + scaledSpeed, position.Y + scaledSpeed);
            position = p;
            shape.Position = p;
        }

        private void Kinematics(float dt)
        {
            
        }
    }
}
