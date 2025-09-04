using SFML.Graphics;
using SFML.System;
using StadnardGameLib;

namespace meltedhope.src
{
    public class Ability: GameObject<Sprite>
    {
        public int counter = 1;
        public int max_level = 5;
        public Ability(Texture texture) : base(new Sprite(texture))
        {
            Obj!.Origin = new Vector2f(texture.Size.X / 2f, texture.Size.Y / 2f);
        }

        public override FloatRect GetLocalBounds()
        {
            return Obj!.GetLocalBounds();
        }
        public override FloatRect GetGlobalBounds()
        {
            return Obj!.GetGlobalBounds();
        }

        public virtual bool MakeAChange() { return false; }
        public virtual bool MakeAChange(Player player) { return false; }
    }
}
