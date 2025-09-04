using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meltedhope.src.Abilities
{
    public class Bigger_Range_Ability:Ability
    {
        public static Texture texture = new SFML.Graphics.Texture("assets/art/br_card_1.png");

        public Bigger_Range_Ability() : base(texture)
        {
            max_level = 1;
        }
        public override bool MakeAChange(Player player)
        {
            bool result = false;
            player.bulletrange += 100f;
            player.damagefromburn += 0.001f;
            counter++;
            if (counter <= max_level)
                Obj!.Texture = new SFML.Graphics.Texture($"assets/art/br_card_{counter}.png");
            else
            {
                result = true;
            }
            return result;
        }
    }
}
