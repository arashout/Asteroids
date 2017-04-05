using System;

namespace Asteroids
{
    public static class Helper
    {
        /// <summary>
        /// Array slicing doesn't really exist, so implementing version I
        /// found on stackoverflow. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}
