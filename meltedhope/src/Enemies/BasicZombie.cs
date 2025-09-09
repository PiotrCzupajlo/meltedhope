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
            new Texture("assets/art/enemy_new_1.png"),
            new Texture("assets/art/enemy_new_7.png"),
            new Texture("assets/art/enemy_new_8.png"),
            new Texture("assets/art/enemy_new_5.png"),
            new Texture("assets/art/enemy_new_6.png"),
        ];
        static readonly List<Texture> walkTexture_damaged = [
            new Texture("assets/art/enemy_new_1.png"),
            new Texture("assets/art/enemy_new_7.png"),
            new Texture("assets/art/enemy_new_8.png"),
            new Texture("assets/art/enemy_new_5.png"),
            new Texture("assets/art/enemy_new_6.png"),
            ];
        static readonly List<Texture> attack_textures = [
            new Texture("assets/art/enemy_new_4.png"),
            new Texture("assets/art/enemy_new_2.png"),
            new Texture("assets/art/enemy_new_3.png"),
            
            
            ];
        static readonly List<Texture> takingdamage = [
    new Texture("assets/art/enemy_new_11.png"),
    new Texture("assets/art/enemy_new_10.png"),
    new Texture("assets/art/enemy_new_9.png")


    ];

        public bool lock_attack_animation = false;
        public BasicZombie(Vector2f position) : base(walkTextures,walkTexture_damaged,takingdamage, position, health: 10f, damage: 1f, speed: 50f,shadow_offset_x:15,shadow_offset_y:-3,dynamic_mirrored_offset:-20,25f,10,80,0,-100, 100,170)
        {
            Obj!.Scale = new Vector2f(6f, 6f);
        }
        public override void OnUpdate(RenderWindow window,float deltatime)
        {
            
            if (health != 5f)
            { 
            if(health<3)
                    damagestate = 2;
                else
                    damagestate = 1;
            }
            
            if (Obj!.Scale == new Vector2f(-6, 6))
            {
                shadow.Position = new Vector2f(
                this.Position.X + shadow_offset_x,
                (this.Position.Y + this.GetGlobalBounds().Height / 2f) + shadow_offset_y);
                this.hitbox.Position = new Vector2f(this.Position.X - hitbox_offset_x + dynamic_mirrored_offset, this.Position.Y + hitbox_offset_y);
            }
            else
            {
                shadow.Position = new Vector2f(
                this.Position.X - shadow_offset_x + dynamic_mirrored_offset,
                (this.Position.Y + this.GetGlobalBounds().Height / 2f) + shadow_offset_y);
                this.hitbox.Position = new Vector2f(this.Position.X-100  , this.Position.Y + hitbox_offset_y);
            }
            changed_hitbox_position = true;
            if (attacking_player == true)
            {
                counter_after_attack += deltatime;
                if (counter_after_attack > animation_after_attack)
                {
                    counter_after_attack = 0;

                    this.Obj.Texture = attack_textures.ElementAt(attack_animation_frame);
                    attack_animation_frame++;
                    if(attack_animation_frame== attack_textures.Count())
                    {
                        attack_animation_frame = 0;
                        lock_attack_animation = false;
                        attacking_player = false;
                    }
                }
            
            }
            base.OnUpdate(window,deltatime);

        }
        public override void OnDeletion()
        {
            GameScreen.Instance?.AddLessImportant(new Old_Wax(this.Position.X + 50, this.Position.Y));

            GameScreen.Instance?.AddLessImportant(new YellowXpStar(this.Position.X - 50, this.Position.Y));
            
        }
    }
}
