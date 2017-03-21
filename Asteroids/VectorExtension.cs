using SFML.System;

namespace Asteroids
{
    public static class VectorExtension
    {
        public static float magnitudeSquared(this Vector2f v)
        {
            float mag = v.X * v.X + v.Y * v.Y;
            return mag;
        }
    }
}
