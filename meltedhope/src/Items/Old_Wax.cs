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
        public float healamount = 4f;
        public Old_Wax(float x,float y) :base(new SFML.Graphics.Texture("assets/art/old_wax_item.png"),x,y,-1,12)
        {

        }
        public override void collectitem(Player character)
        {
             character.IncreaseHealth(healamount);
        }
    }
}
