using SFML.System;
using SFML.Window;
using SFML.Graphics;
using System.Collections.Generic;

namespace Asteroids
{
    abstract public class Entity
    {
        //Protected allows child classes to use properties
        protected Shape shape;
        protected Vector2f velocity;

        abstract public void Draw(RenderWindow window);
        abstract public void Update(float dt, List<Projectile> listProjectiles);
    }
}
