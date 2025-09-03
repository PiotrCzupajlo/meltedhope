using SFML.Graphics;
using StadnardGameLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace meltedhope
{
    public class YellowXpStar:Item
    {
        public int XpAmount = 100;
        public static Texture texture = new SFML.Graphics.Texture("assets/art/yellow.png");
        public YellowXpStar(float x, float y) : base(texture, x, y, -1,12)
        {

        }
        public override void collectitem(Player character)
        {
            character.AddXp(XpAmount);
        }
    }
}
