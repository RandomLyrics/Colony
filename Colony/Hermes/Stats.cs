using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes
{
    /// <summary>
    /// Performance Analytics
    /// </summary>
    public static class Stats
    {
        /// <summary>
        ///Memory Size based on GC, output objects per MB
        /// </summary>
        /// <param name="t"></param>
        /// <param name="mb"></param>
        /// <param name="permb"></param>
        /// <returns></returns>
        public static double SizeOf(Type t, bool mb = true, bool permb = true)
        {
            var c = GC.GetTotalMemory(false);
            var tmp = Activator.CreateInstance(t);
            var r = GC.GetTotalMemory(false) - c;
            return mb ? permb ? 1048576d / r : r / 1048576d : r;
        }

        public static void For(int iterations, Action body)
        {
            for (int i = iterations - 1; i >= 0; i--)
            {
                using (new SpeedTimer())
                {
                    body();
                }
            }
        }

        public static void PrintOutTicks(Action call, int iterations)
        {
            using (new SpeedTimer())
            {
                for (int i = 0; i < iterations; i++)
                {
                    call();
                };
            }
        }
    }
}
