using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meltedhope.Items
{
    public class Old_Wax:Item
    {
        public int healamount = 40;
        public Old_Wax(int x, int y) :base(new SFML.Graphics.Texture("assets/old_wax_item.png"),x,y,15,60)
        {

        }
        public override void collectitem(Character character)
        {
            character.heallthincrease(healamount);
        }
    }
}
