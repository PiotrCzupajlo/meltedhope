using meltedhope.Items;
using meltedhope;
using SFML.Graphics;
using SFML.System;
using StadnardGameLib;
using System.ComponentModel.Design;

namespace meltedhope
{
    public class BasicZombie : Enemy
    {
        static readonly List<Texture> walkTextures =
        [
            new Texture("assets/art/candle_enemy_1.png"),
            new Texture("assets/art/candle_enemy_2.png"),
        ];
        static readonly List<Texture> walkTexture_damaged = [
            new Texture("assets/art/candle_enemy_damaged_1.png"),
            new Texture("assets/art/candle_enemy_damaged_2.png")
            ];
        static readonly List<Texture> takingdamage = [
    new Texture("assets/art/candle_enemy_taking_1.png"),
            new Texture("assets/art/candle_enemy_taking_2.png")
    ];


        public BasicZombie(Vector2f position) : base(walkTextures,walkTexture_damaged,takingdamage, position, health: 5f, damage: 1f, speed: 100f,shadow_offset_x:15,shadow_offset_y:-3,dynamic_mirrored_offset:-2)
        {
            Obj!.Scale = new Vector2f(2f, 2f);
        }
        public override void OnUpdate()
        {
            if (health != 5f)
            { 
            if(health<3)
                    damagestate = 2;
                else
                    damagestate = 1;
            }

            if (Obj!.Scale == new Vector2f(-2, 2))
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
        public override void OnDeletion()
        {
            GameScreen.Instance?.AddGameObject(new Old_Wax(this.Position.X + 50, this.Position.Y));

            GameScreen.Instance?.AddGameObject(new YellowXpStar(this.Position.X - 50, this.Position.Y));
            
        }
    }
}
