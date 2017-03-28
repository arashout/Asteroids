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
        virtual protected Edge OutOfBoundsEdge(Window window, float halfCharacteristicLength)
        {
            if ((shape.Position.X + halfCharacteristicLength) < 0) return Edge.LEFT;
            else if ((shape.Position.X - halfCharacteristicLength) > window.Size.X) return Edge.RIGHT;
            else if ((shape.Position.Y + halfCharacteristicLength) < 0) return Edge.UP;
            else if ((shape.Position.Y - halfCharacteristicLength) > window.Size.Y) return Edge.DOWN;
            else return Edge.NULL;
        }
        virtual protected void ResetPosition(Edge edge, Window window, float halfCharacteristicLength)
        {
            if (edge == Edge.LEFT) shape.Position = new Vector2f(window.Size.X + halfCharacteristicLength, shape.Position.Y);
            else if (edge == Edge.RIGHT) shape.Position = new Vector2f(-halfCharacteristicLength, shape.Position.Y);
            else if (edge == Edge.UP) shape.Position = new Vector2f(shape.Position.X, window.Size.Y + halfCharacteristicLength);
            else shape.Position = new Vector2f(shape.Position.X, -halfCharacteristicLength);
        }
    }
}
