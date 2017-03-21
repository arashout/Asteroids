using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Asteroids
{
    abstract class Entity
    {
        //Protected allows child classes to use properties
        protected Shape shape;
        protected uint mass;
        protected Vector2f position;
        protected Vector2f velocity;

        abstract public void Draw(RenderWindow window);
        abstract public void Update(float dt);
    }
}
