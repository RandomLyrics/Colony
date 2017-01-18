using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeGame.Engine
{
    public class Player
    {
        public string Name { get; set; }
        public decimal Coins { get; set; }
        public int Transactions { get; set; }


        public int Exchange(decimal fromCoins, decimal toCoins)
        {
            Coins = Coins - fromCoins + toCoins;
            if (Coins < 0)
            {
                Coins = Coins + fromCoins - toCoins;
                return 0;
            }
            return 1;
        }


    }
}
