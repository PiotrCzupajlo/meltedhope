using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meltedhope.src.Abilities
{
    public class Bigger_Flame_Ability:Ability
    {
        public static Texture texture = new SFML.Graphics.Texture("assets/art/bf_card_1.png");

        public Bigger_Flame_Ability() : base(texture)
        {
            max_level = 1;
        }
        public override bool MakeAChange(Player player)
        {
            bool result = false;
            player.bullet_damage*=1.5f;
            player.damagefromburn += 0.002f;
            counter++;
            if (counter <= max_level)
                this.Texture = new SFML.Graphics.Texture($"assets/art/bf_card_{counter}.png");
            else
            {
                result = true;
            }
            return result;
        }
    }
}
