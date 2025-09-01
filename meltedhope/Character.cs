using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meltedhope
{
    public class Character : Sprite
    {
        public EllipseShape shadow;
        public float health;
        public Character(Texture texture,Vector2f vector2F) : base(texture)
        {
            this.Position = vector2F;
            shadow = new EllipseShape(20f, new Vector2f(2f, 0.5f));
            shadow.FillColor = new Color(0, 0, 0, 120);
            shadow.Origin = new Vector2f(shadow.Radius, shadow.Radius);
            
            this.Origin = new SFML.System.Vector2f(texture.Size.X / 2f, texture.Size.Y / 2f);
            health = 100;
            changeposition(new Vector2f(0, 0));
        }
        public bool healthdecrease(int amount)
        {
            bool isdead = false;
            if (health > amount)
                health -= amount;
            else
                isdead = true;
            return isdead;


        }
        public bool heallthincrease(int amount)
        {
            bool isfull = false;
            if (health + amount <= 100)
                health += amount;
            else
            {
                health = 100;
                isfull = true;
            }
            return isfull;
        }
        public void changeposition(Vector2f vector2F)
        {
            this.Position += vector2F;
            shadow.Position = new Vector2f(
            this.Position.X,
            (this.Position.Y + this.GetGlobalBounds().Height / 2f) - 3);
        }
    }
}
