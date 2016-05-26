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
        public static double StartMoney = 100;
        public static double StartPopulation = 20;
        public static double StartFood = 10;
        public static double StartHouses = 20;
        public static double StartTax = 0.5;
        public static double StartLiberty = 0.5;
        public static double StartMorality = 0.5;
        public static double StartHappiness = 1.0;
        public static double StartCulture = 5.0;

        //WAGES
        public static double MoralityTaxWage = 1.2;
        public static double IndustryHappinessWage = 0.33;
        public static double HappinessBreedingWage = 0.5;
        public static double IndustryMaterialsWage = 0.05;
        public static double TechnologyWage = 0.1;

        /// <summary>
        /// population * beedingRate = income
        /// </summary>
        public static double BreedingRate = 1.2; //

        //ADVANCED
        public static double AdvFoodCount = 1;
        public static double AdvHouseCount = 1;
        public static double AdvIndustryCount = 1;
        public static double AdvCultureCount = 1;
        public static double AdvEducationMultipliar = 1.2;
        public static double AdvTechnologyMultipliar = 1.01;

    }
}
