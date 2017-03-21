using System;
using SFML.System;

namespace Asteroids
{
    public static class MathExtension
    {
        public static float degToRads(this float deg)
        {
            return  deg / (float) 180.0 * (float) Math.PI ; // This casting seems excessive!
        }
    }
}
