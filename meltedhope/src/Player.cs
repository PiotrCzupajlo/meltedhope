using SFML.Graphics;
using SFML.System;
using SFML.Window;
using StadnardGameLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meltedhope
{
    public class Player : GameObject
    {
        static readonly List<Texture> idleTextures =
        [
            new Texture("assets/art/candle_idle.png"),
            new Texture("assets/art/candle_idle2.png"),
        ];
        static readonly List<Texture> walkTextures =
        [
            new Texture("assets/art/candle_move1.png"),
            new Texture("assets/art/candle_move2.png"),
        ];

        public Player(Vector2f position) : base(idleTextures[0], position)
        {
            this.Tag = "Player";
            GameScreen.Instance?.AddGameObject(new Healthbar());
        }

        public bool isMoving = false;
        public float maxHealth = 10f;
        public float health = 10f;
        public float speed = 500f;
        public float shootCooldown = 0.5f;
        public float iFramesCooldown = 0.2f;

        private float animationTimer = 0f;
        private float shootTimer = 0f;
        private float iFramesTimer = 0f;

        public override void OnUpdate(float deltaTime)
        {
            if (iFramesTimer > 0) iFramesTimer -= deltaTime;
            animationTimer += deltaTime;
            shootTimer += deltaTime;
            HandleMovment(deltaTime);
            HandleAnimation();
            HandleShooting();
        }

        void HandleMovment(float deltaTime)
        {
            var direction = new Vector2f(0, 0);
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                direction.Y -= 1;
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                direction.Y += 1;
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                direction.X -= 1;
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                direction.X += 1;

            if (direction.X > 0)
                this.Scale = new Vector2f(1, Scale.Y);
            if (direction.X < 0)
                this.Scale = new Vector2f(-1, Scale.Y);

            isMoving = direction.X != 0 || direction.Y != 0;
            this.Position += direction * (speed * deltaTime);
        }

        void HandleAnimation()
        {
            if (!isMoving)
            {
                int frame = (int)(animationTimer * 2) % idleTextures.Count;
                this.Texture = idleTextures[frame];
            }
            else
            {
                int frame = (int)(animationTimer * 7) % walkTextures.Count;
                this.Texture = walkTextures[frame];
            }
        }

        Vector2f GetShootingDirection()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                return new Vector2f(0, -1);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                return new Vector2f(1, 0);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
                return new Vector2f(0, 1);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                return new Vector2f(-1, 0);

            return new Vector2f(0, 0);
        }

        void HandleShooting()
        {
            var direction = GetShootingDirection();
            if (direction.X == 0 && direction.Y == 0)
                return;
            if (shootTimer < shootCooldown)
                return;

            shootTimer = 0;
            GameScreen.Instance?.AddGameObject(new Bullet(this.Position, direction, damage: 1));
        }

        public void TakeDamage(float damage)
        {
            if (iFramesTimer > 0)
                return;

            this.health -= damage;
            iFramesTimer = iFramesCooldown;
        }

        // public EllipseShape shadow;
        // public bool healthdecrease(int amount)
        // {
        //     bool isdead = false;
        //     if (health > amount)
        //         health -= amount;
        //     else
        //         isdead = true;
        //     return isdead;
        // }
        // public bool heallthincrease(int amount)
        // {
        //     bool isfull = false;
        //     if (health + amount <= 100)
        //         health += amount;
        //     else
        //     {
        //         health = 100;
        //         isfull = true;
        //     }
        //     return isfull;
        // }
        // public void changeposition(Vector2f vector2F)
        // {
        //     this.Position += vector2F;
        //     shadow.Position = new Vector2f(
        //     this.Position.X,
        //     (this.Position.Y + this.GetGlobalBounds().Height / 2f) - 3);
        // }
    }
}
