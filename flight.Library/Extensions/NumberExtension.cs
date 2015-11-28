using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flight.Library.Extensions
{
    public class NumberExtension
    {

        //Function to get random number
        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();

        public static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return getrandom.Next(min, max);
            }
        }

        public static string GetRandomUniqueCodeWithLength(int length)
        {

            StringBuilder output = new StringBuilder();

            for (int i = 0; i < length; i++)
                output.Append(GetRandomNumber(0, 9));

            return output.ToString();

        }

    }
}
