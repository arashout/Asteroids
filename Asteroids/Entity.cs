using SFML.System;
using SFML.Window;
using SFML.Graphics;
using System.Collections.Generic;

namespace Asteroids
{
    /// <summary>
    /// Any object on the screen besides the score text, is a child 
    /// of the Entity abstract class. The reason for this is that 
    /// they all share pretty much every method in this class
    /// </summary>
    abstract public class Entity
    {
        private string id;
        protected Shape shape;
        protected Vector2f velocity;

        abstract public void Draw(RenderWindow window);
        abstract public void Update(float dt);
        /// <summary>
        /// Method that returns an edge that the entity exited from
        /// It uses the halfCharacteristicLength to determine if the
        /// entity is "fully" out of bounds or not
        /// </summary>
        /// <param name="window"></param>
        /// <param name="halfCharacteristicLength"></param>
        /// <returns></returns>
        virtual protected Edge OutOfBoundsEdge(Window window, float halfCharacteristicLength)
        {
            if ((shape.Position.X + halfCharacteristicLength) < 0) return Edge.LEFT;
            else if ((shape.Position.X - halfCharacteristicLength) > window.Size.X) return Edge.RIGHT;
            else if ((shape.Position.Y + halfCharacteristicLength) < 0) return Edge.UP;
            else if ((shape.Position.Y - halfCharacteristicLength) > window.Size.Y) return Edge.DOWN;
            else return Edge.NULL;
        }
        /// <summary>
        /// Method that resets the entity position depending on the edge they exited from
        /// Given an edge and a characteristic length it resets the entity at the OPPOSITE edge 
        /// it left from
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="window"></param>
        /// <param name="halfCharacteristicLength"></param>
        virtual protected void ResetPosition(Edge edge, Window window, float halfCharacteristicLength)
        {
            if (edge == Edge.LEFT) shape.Position = new Vector2f(window.Size.X + halfCharacteristicLength, shape.Position.Y);
            else if (edge == Edge.RIGHT) shape.Position = new Vector2f(-halfCharacteristicLength, shape.Position.Y);
            else if (edge == Edge.UP) shape.Position = new Vector2f(shape.Position.X, window.Size.Y + halfCharacteristicLength);
            else shape.Position = new Vector2f(shape.Position.X, -halfCharacteristicLength);
        }
        public string Id { get => id; set => id = value; }
    }
}
