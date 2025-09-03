using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meltedhope.src.Abilities
{
    public class WaxRegenerationAbility : Ability
    {
        public static Texture texture = new SFML.Graphics.Texture("assets/art/wr_card_1.png");
        public WaxRegenerationAbility() : base(texture)
        {
            max_level = 1;
        }
        public override bool MakeAChange(Player player)
        {
            bool result = false;
            player.damagefromburn -= 0.001f;
            counter++;
            if (counter <= max_level)
                this.Texture = new SFML.Graphics.Texture($"assets/art/ds_card_{counter}.png");
            else
            {
                result = true;
            }
            return result;
        }
    }
}
