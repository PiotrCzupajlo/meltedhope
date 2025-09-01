using OpenTK.Graphics.OpenGL;
using SFML.Graphics;
using SFML.System;
using StadnardGameLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace meltedhope
{
    public class Enemy : GameObject
    {
        public float health;
        public float damage;
        public float speed;
        private readonly List<Texture> walkTextures;

        public Enemy(List<Texture> walkTextures, Vector2f position , float health, float damage, float speed) : base(walkTextures[0], position)
        {
            this.Tag = "Enemy";
            this.walkTextures = walkTextures;
            this.health = health;
            this.damage = damage;
            this.speed = speed;
        }

        private float animationTimer = 0f;

        public override void OnUpdate(float deltaTime)
        {
            animationTimer += deltaTime;
            var player = GameScreen.Instance?.GetFirst<Player>();
            if (player != null)
                GoToPlayer(player, deltaTime);
            HandleAnimation();

            if (health <= 0)
                this.Destroy();
        }

        private void GoToPlayer(Player player, float deltaTime)
        {
            float deltaX = player.Position.X - Position.X;
            float deltaY = player.Position.Y - Position.Y;
            float length = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

            var direction = new Vector2f(deltaX / length, deltaY / length);

            if (direction.X > 0)
                this.Scale = new Vector2f(Math.Abs(this.Scale.X), this.Scale.Y);
            if (direction.X < 0)
                this.Scale = new Vector2f(-Math.Abs(this.Scale.X), this.Scale.Y);

            if (length > 10)
                this.Position += direction * (speed * deltaTime);
            else
                player.TakeDamage(damage);
        }

        void HandleAnimation()
        {
            int frame = (int)(animationTimer * 7) % walkTextures.Count;
            this.Texture = walkTextures[frame];
        }

        public void TakeDamage(float damage)
        {
            this.health -= damage;
        }


    //     public  bool Update(Player character)
        //     {
        //         tick++;
        //         if (tick == 50)
        //         {
        //             tick = 0;
        //             current_texture_id++;
        //             if (current_texture_id >= animation.Count)
        //                 current_texture_id = 0;
        //             this.Texture = animation.ElementAt(current_texture_id);

        //         }
        //         if(attackcooldown>0)
        //             attackcooldown--;
        //         bool ischaracterdead = false;
        //         UpdateCorners();
        //         float deltaX = character.Position.X - Position.X;
        //         float deltaY = character.Position.Y - Position.Y;
        //         float length = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        //         if (length >10)
        //         {
        //             deltaX /= length;
        //             if (deltaX > 0) {
        //                 this.Scale = new Vector2f(2, 2);
        //             }
        //             else
        //             {
        //                     this.Scale = new Vector2f(-2, 2);
        //             }
        //             deltaY /= length;
        //             Position = new Vector2f(Position.X + deltaX * Speed, Position.Y + deltaY * Speed);
        //             if (this.Scale == new Vector2f(-2, 2))
        //             {
        //                 shadow.Position = new Vector2f(
        //                 this.Position.X + shadow_offset_x,
        //                 (this.Position.Y + this.GetGlobalBounds().Height / 2f) + shadow_offset_y);
        //             }
        //             else
        //             {
        //                 shadow.Position = new Vector2f(
        //                 this.Position.X - shadow_offset_x+dynamic_mirrored_offset,
        //                 (this.Position.Y + this.GetGlobalBounds().Height / 2f) + shadow_offset_y);
        //             }
        //         }
        //         else
        //         {
        //             //atack
        //             if (attackcooldown == 0)
        //             {
        //                 // ischaracterdead = character.healthdecrease(Damage);
        //                 attackcooldown = 50;
        //             }
        //         }

        //         return ischaracterdead;
    }
}
