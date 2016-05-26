using Colony.Game.Helpers;
using Colony.Game.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

            this.Dead = false;
        }

        //CHARS
        private string Name { get; set; }
        private string LordName { get; set; }
        private ColonyType ColonyType { get; set; }
        private bool Dead { get; set; }

        //CONTROL
        private double Tax { get; set; } //0.0 - 1.0
        private double Liberty { get; set; } //0.0 - 1.0
        private AdvanceType AdvanceType { get; set; }
        public SupportType SupportType { get; set; }

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
        private double Industry { get; set; }
        private double Education { get; set; }
        private double Technology { get; set; }

        private void CalculatePopulationIncome()
        {
            var v1 = Happiness * Liberty;
            this.PopulationIncome = (Population * v1);// * GLOBALS.BreedingRate; //population + income
        }
        private void CalculateMoneyIncome()
        {
            var v1 = CalculateHelper.Limit((Morality * GLOBALS.MoralityTaxWage), 0.0, 1.0);
            this.MoneyIncome = (Population * Tax) * v1;
        }

        public void GiveResources()
        {
            GiveMoney();
            GivePopulation();
        }
        private void GivePopulation()
        {
            var v1 = Population + PopulationIncome;
            if (Population < 1)
            {
                this.Dead = true;
            }
            else
                this.Population = CalculateHelper.Limit(v1, 0.0, Double.MaxValue);
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
            var v1 = Math.Min(x1, x2);
            if (v1 > 0) v1 = Math.Max(x1, x2);
            var f1 = (v1) / pop;
            var f2 = (Culture - Industry) / pop;
            var fhap = f1 + f2;
            this.Happiness = CalculateHelper.Limit(fhap, -1.0, 1.0);
        }
        private void CalculateMorality()
        {
            var x1 = (Population * Liberty) / (Population * 10);
            var v1 = Liberty == 0.5 ? 0.0 : (Liberty < 0.5 ? -x1 : x1);
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

        //PROCESS
        public void Process(AdvanceType advance, SupportType support)
        {
            var v1 = Technology > 0 ? (Technology * Industry) / (Technology + Industry) : 0;
            if (advance == AdvanceType.Food)
            {
                this.Food = Food + GLOBALS.AdvFoodCount + v1; //+ (Industry * GLOBALS.IndustryMaterialsWage) 
            }
            else
            if (advance == AdvanceType.Houses)
            {
                this.Houses = Houses + GLOBALS.AdvHouseCount  + v1;
            }
            else
            if (advance == AdvanceType.Industry)
            {
                this.Industry = Industry + GLOBALS.AdvIndustryCount + (Technology * GLOBALS.TechnologyWage);
            }
            else
            if (advance == AdvanceType.Education)
            {
                // this.Industry = Industry + (int)GLOBALS.AdvIndustryCount;
            }
            else
            if (advance == AdvanceType.Culture)
            {
                this.Culture = Culture + GLOBALS.AdvCultureCount + (Technology * GLOBALS.TechnologyWage);
            }
            else
            if (advance == AdvanceType.Technology)
            {
                this.Industry = Industry * GLOBALS.AdvTechnologyMultipliar;
                this.Houses = Houses * GLOBALS.AdvTechnologyMultipliar;
                this.Food = Food * GLOBALS.AdvTechnologyMultipliar;
                this.Culture = Culture * GLOBALS.AdvTechnologyMultipliar;

                this.Technology = Technology + 1;
            }

        }
        public bool IsDead()
        {
            return this.Dead;
        }

        //UTILS
        internal string ToText()
        {
            var banlist = "Name,LordName,Tax,ColonyType,Liberty,Morality".Split(',');
            var perclist = "Happiness";
            var intlist = "Food,Houses,Population,PopulationIncome,Money,MoneyIncome,Culture,Industry,Technology".Split(',');
            var sb = new StringBuilder();
            foreach (var prop in this.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
            {
                if (!banlist.Contains(prop.Name))
                {
                    var val = prop.GetValue(this);
                    if (perclist.Contains(prop.Name))
                        val = String.Format("{0:P2}.", val);
                    if (intlist.Contains(prop.Name))
                        val = Math.Round((double)val);
                    sb.AppendFormat("[{0}]:\t{1}", prop.Name, val.ToString());
                    sb.Append("\n");
                }

            }
            return sb.ToString();
        }
    }
}
