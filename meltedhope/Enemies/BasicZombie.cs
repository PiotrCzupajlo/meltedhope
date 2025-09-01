using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace meltedhope.Enemies
{
    public class BasicZombie :Enemy
    {
        public BasicZombie() : base(100, 10, 1.0f,15,-3,-2)
        {
            this.animation = new List<SFML.Graphics.Texture>();
            this.animation.Add(new SFML.Graphics.Texture("assets/candle_enemy_1.png"));
            this.animation.Add(new SFML.Graphics.Texture("assets/candle_enemy_2.png"));
            this.Origin = new SFML.System.Vector2f(animation.ElementAt(0).Size.X / 2f, animation.ElementAt(0).Size.Y / 2f);
            this.Texture = animation.ElementAt(0);
            this.Scale = new SFML.System.Vector2f(2, 2);
        }
        public override void iskilled(List<Enemy> enemies, List<Item> items)
        {
            items.Add(new Items.Old_Wax(Convert.ToInt16(this.Position.X+50),Convert.ToInt16(this.Position.Y+50)));
        }
    }
}
