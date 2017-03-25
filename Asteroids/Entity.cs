using SFML.System;
using SFML.Window;
using SFML.Graphics;
using System.Collections.Generic;

namespace Asteroids
{
    abstract public class Entity
    {
        // To determine out of bounds edge
        protected enum Edge { UP, DOWN, LEFT, RIGHT, NULL }

        //Protected allows child classes to use properties
        protected Shape shape;
        protected Vector2f velocity;

        abstract public void Draw(RenderWindow window);
        abstract public void Update(float dt);
        abstract protected Edge OutOfBoundsEdge(Window window);
        abstract protected void ResetPosition(Edge edge, Window window);
    }
}
