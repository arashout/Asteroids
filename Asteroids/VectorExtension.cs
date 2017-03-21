using SFML.System;

namespace Asteroids
{
    public static class VectorExtension
    {
        /// <summary>
        /// Computes the magnitude squared of the vector
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static float magnitudeSquared(this Vector2f v)
        {
            float mag = v.X * v.X + v.Y * v.Y;
            return mag;
        }
    }
}
