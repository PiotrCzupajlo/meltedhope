using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meltedhope.src.Items
{
    public class YellowXpStar:Item
    {
        public int XpAmount = 40;
        public YellowXpStar(float x, float y) : base(new SFML.Graphics.Texture("assets/art/yellow.png"), x, y, -1,12)
        {

        }
        public override void collectitem(Player character)
        {
            character.AddXp(XpAmount);
        }
    }
}
