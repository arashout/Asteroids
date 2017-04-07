using SFML.System;
using System;

namespace Asteroids
{
    /// <summary>
    /// Point of this class is to provide vector operations
    /// that SFML.Net doesn't provide
    /// </summary>
    public static class VectorExtension
    {
        /// <summary>
        /// 2D definition of a dot product 
        /// </summary>
        /// <param name="v"></param>
        /// <param name="u"></param>
        /// <returns></returns>
        public static float DotProduct(this Vector2f v, Vector2f u)
        {
            return v.X * u.X + v.Y * u.Y;
        }
        /// <summary>
        /// Returns the magnitude of a vector
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static float Magnitude(this Vector2f v)
        {
            double dotItself = v.X * v.X + v.Y * v.Y;
            return (float) Math.Sqrt(dotItself);
        }
    }
}
