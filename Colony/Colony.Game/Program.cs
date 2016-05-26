using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colony.Game.Helpers;

using Colony.Game.Models.Enums;

namespace Colony.Game
{
    using Colony = Colony.Game.Models.Colony;
    class Program
    {
        static void Main(string[] args)
        {
            var rnd = new Random();
            var list = new List<Colony>();
            var colony = new Colony();
            colony.Initialize("Mars", "Wójcik", ColonyType.Authoritarian);
            var year = 1;
            while(true)
            {
                Console.WriteLine("Year: " + year);
               
                Console.SetCursorPosition(0, 0);
                colony.CalculateIncomes();
                colony.CalculateQualities();
                //begin
                colony.GiveResources();
                //players decisions
                //var adv = AdvanceType.Industry;
                //var adv = GenerateDecision<AdvanceType>(rnd);
                var sup = GenerateDecision<SupportType>(rnd);
                AdvanceType adv = AdvanceType.None;
                var decision = 0;
                EnumToString<AdvanceType>();
                if(int.TryParse(Console.ReadKey().KeyChar.ToString(), out decision))
                {
                    Console.Write("\r         ");
                    adv = (AdvanceType)decision;
                    decision = 0;
                    Console.Clear();
                }
                Console.WriteLine();
                //Console.Write);
                colony.Process(adv, sup);
                Console.WriteLine("[{0}]({1})", adv, sup);
                Console.WriteLine(colony.ToText());
                
                //end
                
                var cl = new List<Colony>();
                list.Add(colony.Clone());
                ++year;
                if (colony.IsDead())
                    break;
            }
            Console.WriteLine("Year: " + year);
            Console.WriteLine("Koniec gry");
            Console.ReadLine();

        }

        private static void EnumToString<T>()
        {
            var sb = new StringBuilder();
            var names = Enum.GetNames(typeof(T));
            for (int i = 0; i < names.Length; i++)
            {
                sb.AppendFormat("[{0}]: {1}\t", names[i], i);
                sb.Append("\t");
            }
            Console.WriteLine(sb.ToString());
        }

        private static T GenerateDecision<T>(Random rnd)
        {
            var myEnumMemberCount = Enum.GetNames(typeof(T)).Length;
            var x = rnd.Next(1, myEnumMemberCount);
            return (T)(object)x;
        }
    }
}
