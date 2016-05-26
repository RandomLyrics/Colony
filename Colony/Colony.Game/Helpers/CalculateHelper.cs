using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony.Game.Helpers
{
    public static class CalculateHelper
    {
        public static bool InRange(double source, double from, double to)
        {
            return (source > from && source < to);
        }
        public static double Limit(double source, double min, double max)
        {
            if (source > max) source = max;
            else if (source < min ) source = min;
            return source;
        }
    }
}
