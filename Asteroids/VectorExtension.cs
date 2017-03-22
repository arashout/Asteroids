using SFML.System;
using System;

namespace Asteroids
{
    public static class VectorExtension
    {
        /// <summary>
        /// Computes the magnitude squared of the vector
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static float MagnitudeSquared(this Vector2f v)
        {
            float mag = v.X * v.X + v.Y * v.Y;
            return mag;
        }
        
        public static float Magnitude(this Vector2f v)
        {
            double mag = v.X * v.X + v.Y * v.Y;
            return (float) Math.Sqrt(mag);
        }
    }
}
