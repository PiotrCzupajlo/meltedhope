using OpenTK.Graphics.ES20;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meltedhope.src.Abilities
{
    public class Faster_Reload_Ability:Ability
    {
        public static Texture texture = new SFML.Graphics.Texture("assets/art/fr_card_1.png");

        public Faster_Reload_Ability() : base(texture)
        {
            max_level = 5;
        }
        public override bool MakeAChange(Player player)
        {
            bool result = false;
            player.damagefromburn += 0.001f;
            player.shootCooldown *= 0.9f;
            counter++;
            if (counter <= max_level)
                Obj!.Texture = new SFML.Graphics.Texture($"assets/art/fr_card_{counter}.png");
            else
            {
                result = true;
            }
            return result;
        }
    }
}
