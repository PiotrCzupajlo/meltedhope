using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meltedhope.src.Abilities
{
    public class Double_Shoot_Ability:Ability
    {
        public static Texture texture = new SFML.Graphics.Texture("assets/art/ds_card_1.png");
        
        public Double_Shoot_Ability() : base(texture)
        {
            max_level = 2;
        }
        public override bool MakeAChange(Player player)
        {
            bool result = false;
            player.current_bullet_multiplyer++;
            player.damagefromburn += 0.001f;
            counter++;
            if (counter <= max_level)
                this.Texture = new SFML.Graphics.Texture($"assets/art/ds_card_{counter}.png");
            else {
                result = true;
            }
            return result;
        }
    }
}
