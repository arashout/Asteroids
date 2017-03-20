using System;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Asteroids
{
    class Asteroid : Entity
    {
        private float radius;
        private float speed = 10;

        public Asteroid(Vector2f p, uint r = 100)
        {
            // Asteroid default
            radius = r;
            shape = new CircleShape(radius);
            Vector2f o = new Vector2f(radius, radius);

            shape.FillColor = Color.Yellow;
            shape.Position = p;
        }
        public override void Draw(RenderWindow window)
        {
            window.Draw(shape);
        }

        public override bool entityCollision(Entity e)
        {
            throw new NotImplementedException();
        }

        public override void Update(float dt)
        {
            Vector2f p = new Vector2f(position.X + speed, position.Y + speed);
            position = p;
            shape.Position = p;
        }

        public override bool wallCollision(RenderWindow window)
        {
            throw new NotImplementedException();
        }
    }
}
