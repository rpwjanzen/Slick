using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Slick
{
    class Player
    {
        public string Name;
        public int Money;

        public Player(string name, int initialMoney)
        {
            this.Name = name;
            this.Money = initialMoney;
        }
    }
}
