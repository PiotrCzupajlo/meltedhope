using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meltedhope
{
    public class Healthbar : Sprite
    {
        public Sprite body { get; set; }
        public Healthbar(Texture texture, Sprite pointer) : base(texture)
        {
            this.Origin = new SFML.System.Vector2f(this.Texture.Size.X / 2, this.Texture.Size.Y / 2);
            this.body = pointer;
        }
        public void Update(float health, float maxHealth)
        {
            float ratio = health / maxHealth;
            if (ratio < 0) ratio = 0;
            if (ratio > 1) ratio = 1;
            this.body.Scale = new SFML.System.Vector2f(1,ratio);
            this.body.Position = new SFML.System.Vector2f(this.Position.X - (this.Texture.Size.X / 2) + (this.body.Texture.Size.X * ratio) / 2, this.Position.Y);
        }
    }
}
