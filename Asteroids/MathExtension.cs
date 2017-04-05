using System;
using SFML.System;

namespace Asteroids
{
    public static class MathExtension
    {
        /// <summary>
        /// Very simple math function to change degrees into RADs
        /// I wonder if it is even necessary?
        /// </summary>
        /// <param name="deg"></param>
        /// <returns></returns>
        public static float degToRads(this float deg)
        {
            return  deg / (float) 180.0 * (float) Math.PI ; // This casting seems excessive!
        }
    }
}
