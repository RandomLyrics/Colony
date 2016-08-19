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
        ///Memory Size based on GC, default: output objects per MB
        /// </summary>
        /// <param name="t"></param>
        /// <param name="mb"></param>
        /// <param name="permb"></param>
        /// <returns></returns>
        public static double SizeOf(Type t, bool mb = true, bool permb = true, bool force = false)
        {
            var c = GC.GetTotalMemory(force);
            var tmp = Activator.CreateInstance(t);
            var r = GC.GetTotalMemory(force) - c;
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

        /// <summary>
        ///Memory Size based on GC, default: output objects per MB
        /// </summary>
        /// <param name="act"></param>
        /// <param name="mb"></param>
        /// <param name="permb"></param>
        /// <returns></returns>
        public static double SizeOf(Func<object> act, bool mb = true, bool permb = true, bool force = false)
        {
            var c = GC.GetTotalMemory(force);
            var tmp = act();
            var r = GC.GetTotalMemory(force) - c;
            return mb ? permb ? 1048576d / r : r / 1048576d : r;
        }
    }
}
