using Colony.Game.Helpers;
using Colony.Game.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony.Game.Models
{
    public class Colony
    {
        //MAIN
        public Colony() { }

        public void Initialize(string name, string lordName, ColonyType ctype)
        {
            this.Name = name;
            this.LordName = lordName;
            this.ColonyType = ctype;

            this.Tax = GLOBALS.StartTax;
            this.Liberty = GLOBALS.StartLiberty;

            this.Happiness = GLOBALS.StartHappiness;
            this.Morality = GLOBALS.StartMorality;

            this.Money = GLOBALS.StartMoney;
            this.Population = GLOBALS.StartPopulation;

            this.Food = GLOBALS.StartFood;
            this.Houses = GLOBALS.StartHouses;
            this.Culture = GLOBALS.StartCulture;

        }

        //CHARS
        private string Name { get; set; }
        private string LordName { get; set; }
        private ColonyType ColonyType { get; set; }

        //CONTROL
        private double Tax { get; set; } //0.0 - 1.0
        private double Liberty { get; set; } //0.0 - 1.0

        //INFO
        private double Happiness { get; set; }  //-1.0 - 0.0 - 1.0
        private double Morality { get; set; } //0.0 - 1.0

        //INCOMES
        private double Money { get; set; }
        private double MoneyIncome { get; set; }
        private double Population { get; set; }
        private double PopulationIncome { get; set; }
        
       

        private double Food { get; set; }
        private double Houses { get; set; }
        private double Culture { get; set; }

        //LEVEL
        private int Industry { get; set; }
        private int Education { get; set; }
        private int Technology { get; set; }

        private void CalculatePopulationIncome()
        {
            this.PopulationIncome = (Population * GLOBALS.BreedingRate) * Happiness; //population + income
        }
        private void CalculateMoneyIncome()
        {
            this.MoneyIncome = (Population * Tax) * Morality;
        }

        public void GiveResources()
        {
            GiveMoney();
            GivePopulation();
        }
        private void GivePopulation()
        {
            this.Population = Population + PopulationIncome;
        }
        private void GiveMoney()
        {
            this.Money = Money + MoneyIncome;
        }


        //CALC
        private void CalculateHappiness()
        {
            var pop = Population;
            var x1 = Food - pop;
            var x2 = Houses - pop;
            var v1 = x1 + x2;

            var fhap = (( v1 + Culture ) * Liberty) / pop;
            this.Happiness = fhap;
        }
        private void CalculateMorality()
        {
            var x1 = (Population * Liberty) / (Population * 10);
            var v1 = Liberty < 0.5d ? -x1 : x1;
            var t1 = Morality + v1;
            this.Morality = CalculateHelper.Limit(t1, 0.0, 1.0);
            
        }

        public void CalculateIncomes()
        {
            CalculateMoneyIncome();
            CalculatePopulationIncome();
        }
        public void CalculateQualities()
        {
            CalculateHappiness();
            CalculateMorality();
        }
    }
}
