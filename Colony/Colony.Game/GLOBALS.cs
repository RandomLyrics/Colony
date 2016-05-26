using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony.Game
{
    public static class GLOBALS
    {
        //START
        public static double StartMoney = 100d;
        public static double StartPopulation = 20d;
        public static double StartFood = 10d;
        public static double StartHouses = 20d;
        public static double StartTax = 0.5d;
        public static double StartLiberty = 0.5d;
        public static double StartMorality = 0.5d;
        public static double StartHappiness = 1.0d;
        public static double StartCulture = 5.0d;
        /// <summary>
        /// population * beedingRate = income
        /// </summary>
        public static double BreedingRate = 0.2d; //
        
    }
}
