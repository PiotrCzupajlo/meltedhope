using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meltedhope.src.Abilities
{
    public class Speed_Ability:Ability
    {
        public static Texture texture = new SFML.Graphics.Texture("assets/art/s_ability_card.png");

        public Speed_Ability() : base(texture)
        {
            max_level = 1;
        }
        public override bool MakeAChange(Player player)
        {
            bool result = false;
            player.speed *= 1.1f;
            player.damagefromburn += 0.001f;
            counter++;
            if (counter <= max_level)
                Obj!.Texture = new SFML.Graphics.Texture($"assets/art/bf_card_{counter}.png");
            else
            {
                result = true;
            }
            return result;
        }
    }
}
