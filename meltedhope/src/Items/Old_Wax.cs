using StadnardGameLib;
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
        public Old_Wax(float x,float y) :base(new SFML.Graphics.Texture("assets/art/old_wax_item.png"),x,y,15,60)
        {

        }
        public override void collectitem(Player character)
        {
             character.TakeDamage(-1*healamount);
        }
    }
}
