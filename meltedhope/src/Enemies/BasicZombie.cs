using meltedhope.Items;
using meltedhope;
using SFML.Graphics;
using SFML.System;
using StadnardGameLib;

namespace meltedhope
{
    public class BasicZombie : Enemy
    {
        static readonly List<Texture> walkTextures =
        [
            new Texture("assets/art/candle_enemy_1.png"),
            new Texture("assets/art/candle_enemy_2.png"),
        ];

        public BasicZombie(Vector2f position) : base(walkTextures, position, health: 5f, damage: 1f, speed: 100f,shadow_offset_x:15,shadow_offset_y:-3,dynamic_mirrored_offset:-2)
        {
            this.Scale = new Vector2f(2f, 2f);
        }
        public override void OnUpdate()
        {
            if (this.Scale == new Vector2f(-2, 2))
            {
                shadow.Position = new Vector2f(
                this.Position.X + shadow_offset_x,
                (this.Position.Y + this.GetGlobalBounds().Height / 2f) + shadow_offset_y);
            }
            else
            {
                shadow.Position = new Vector2f(
                this.Position.X - shadow_offset_x + dynamic_mirrored_offset,
                (this.Position.Y + this.GetGlobalBounds().Height / 2f) + shadow_offset_y);
            }
            base.OnUpdate();
        }
        public override List<GameObject> OnDeletionCreateNewObj()
        {
            List<GameObject> gameObjects = new List<GameObject>();
            Item old_wax = new Old_Wax(this.Position.X + 50, this.Position.Y);
            gameObjects.Add(old_wax);
            YellowXpStar yellowXpStar = new YellowXpStar(this.Position.X - 50, this.Position.Y);
            gameObjects.Add(yellowXpStar);
            return gameObjects;
            //Drop the shit
        }
    }
}
