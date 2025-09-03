using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using meltedhope;
using SFML.Graphics;
using StadnardGameLib;

namespace meltedhope.src
{
    public class Ability: GameObject
    {
        public int counter = 1;
        public int max_level = 5;
        public Ability(Texture texture) :base(texture)
        {
        }
        public virtual bool MakeAChange() {return false;
        }
        public virtual bool MakeAChange(Player player) { return false; }
    }
}
