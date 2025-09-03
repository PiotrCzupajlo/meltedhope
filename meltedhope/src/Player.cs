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
        public float CurrentXp = 0;
        public float XpToNextLvL = 100;
        public float lvl = 1;
        public EllipseShape shadow;
        public int current_bullet_multiplyer = 1;
        public float bullet_speed = 1f;
        public float bullet_damage = 1f;
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
            shadow = new EllipseShape(20f, new Vector2f(2f, 0.5f));
            shadow.FillColor = new Color(0, 0, 0, 120);
            shadow.Origin = new Vector2f(shadow.Radius, shadow.Radius);
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
        public float damagefromburn = 0.001f;
        private float burningcooldown = 1000000;
        private float burningtimer = 0;

        public override void OnUpdate(RenderWindow window,float deltaTime)
        {
            if (iFramesTimer > 0) iFramesTimer -= deltaTime;
            animationTimer += deltaTime;
            shootTimer += deltaTime;
            window.Draw(shadow);
            HandleMovment(deltaTime);
            HandleAnimation();
            HandleShooting();
            burningdmg(deltaTime);

        }
        public void burningdmg(float deltatime) { 
        this.health -= damagefromburn;
            burningtimer += deltatime;
            if (burningtimer >= burningcooldown)
            {
                this.health -= damagefromburn;
                burningtimer = 0;
            }
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
            shadow.Position = new Vector2f(
            this.Position.X,
            (this.Position.Y + this.GetGlobalBounds().Height / 2f) - 3);
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
            if (direction.X == 0)
            {
                for (int i = 0; i < current_bullet_multiplyer; i++)
                {
                    if(i%2==0)
                        GameScreen.Instance?.AddGameObject(new Bullet(new Vector2f(this.Position.X - ((25 * i) - 25 * (i / 2)), this.Position.Y), direction, bullet_damage));
                    else   
                        GameScreen.Instance?.AddGameObject(new Bullet(new Vector2f(this.Position.X + ((25 * i) - 25 * (i / 2)), this.Position.Y), direction, bullet_damage));
                }
            }
            else
            {
                for (int i = 0; i < current_bullet_multiplyer; i++)
                {
                    if(i%2==0)
                        GameScreen.Instance?.AddGameObject(new Bullet(new Vector2f(this.Position.X , this.Position.Y- ((25 * i) - 25 * (i / 2))), direction, bullet_damage));
                    else
                        GameScreen.Instance?.AddGameObject(new Bullet(new Vector2f(this.Position.X, this.Position.Y + ((25 * i) - 25 * (i / 2))), direction, bullet_damage));
                }
            }
        }

        public void TakeDamage(float damage)
        {
            if (iFramesTimer > 0)
                return;

            this.health -= damage;
            iFramesTimer = iFramesCooldown;
        }
        public void IncreaseHealth(float heal)
        {
            if(heal+this.health<=10)
                this.health += heal;
            else
                this.health = 10;
        }
        public void AddXp(float xp)
        {
            this.CurrentXp += xp;
            while (this.CurrentXp >= XpToNextLvL)
            {
                this.CurrentXp -= XpToNextLvL;
                LevelUp();
            }
        }
        public void LevelUp()
        {
            lvl++;
            XpToNextLvL *= 1.5f;
            XpBar.Instance?.OnUpdate();
            AbilitySelection();
            GameScreen.Instance.isPaused = true;

        }
        public void AbilitySelection() { }



    }
}
