using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Slick
{
    class Player
    {
        public string Name;
        public int Money;
        public Color color;

        public Player(string name, int initialMoney, Color color)
        {
            this.color = color;
            this.Name = name;
            this.Money = initialMoney;
        }
    }
}
