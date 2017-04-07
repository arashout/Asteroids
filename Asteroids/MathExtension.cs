using System;

namespace Asteroids
{
    /// <summary>
    /// Point of this class is to add any common math functions
    /// that aren't in the math class
    /// </summary>
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
