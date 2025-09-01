using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meltedhope
{
    public class Character : Sprite
    {
        public float health;
        public Character(Texture texture) : base(texture)
        {
            this.Origin = new SFML.System.Vector2f(texture.Size.X / 2f, texture.Size.Y / 2f);
            health = 100;
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
    }
}
