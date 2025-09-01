using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meltedhope
{
    public class Item :Sprite
    {
        public Item(float x, float y)
        { 
        this.Position = new SFML.System.Vector2f(x, y);
        }
        public bool Update(Character character)
        {
            FloatRect itemBounds = this.GetGlobalBounds();
            FloatRect playerBounds = character.GetGlobalBounds();

            bool result = itemBounds.Intersects(playerBounds);

            if (result)
            {
                collectitem(character);
            }

            return result;
        }

        public virtual void collectitem(Character character) { }
    }
}
