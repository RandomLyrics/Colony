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
            var list = new List<Colony>();
            var colony = new Colony();
            colony.Initialize("Mars", "Wójcik", ColonyType.Authoritarian);
            for (int i = 0; i < 100; i++)
            {
                
                colony.CalculateQualities();
                //begin
                colony.GiveResources();
                //players decisions
                //end
                colony.CalculateIncomes();
                var cl = new List<Colony>();
                list.Add(colony.Clone());
            }
            
            
        }
    }
}
