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
        public Old_Wax() 
        {
            this.Texture = new SFML.Graphics.Texture("assets/old_wax.png");
        }
    }
}
