using SFML.Graphics;
using SFML.System;
using StadnardGameLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meltedhope.src
{
    public class Enemy_bullet :GameObject<Sprite>
    {
        public List<Texture> animations = new List<Texture>();
        private static readonly Texture baseTexture = new Texture("assets/art/enemy_standing/fire.png");
        private static readonly Texture Texture2 = new Texture("assets/art/enemy_standing/fire.png");
        Vector2f direction;
        float damage;
        float speed = 750f;
        float range;
        float traveledDistance = 0f;
        float animationchange = 0.1f;
        float animation_state = 1;
        float animation_counter = 0;
        float particle_cooldown = 0.05f;
        float particle_counter = 0;
        double offset;

        private Enemy_bullet(Texture texture) : base(new Sprite(texture))
        {
            Random random = new Random();
            this.offset = random.NextDouble() * 0.1;
            this.animations.Add(baseTexture);
            this.animations.Add(Texture2);
            Obj!.Origin = new Vector2f(texture.Size.X / 2f, texture.Size.Y / 2f);
        }
        public Enemy_bullet(Vector2f position, Vector2f direction, float damage, float range) : this(baseTexture)
        {
            Position = position;
            this.direction = direction;
            this.damage = damage;
            this.range = range;
            if (this.direction == new Vector2f(1, 0))
                Obj!.Rotation = 270f;


            else if (this.direction == new Vector2f(-1, 0))
                Obj!.Rotation = 90f;

            else if (this.direction == new Vector2f(0, -1))
                Obj!.Rotation = 180f;
            this.Obj.Scale = new Vector2f(3, 3);

        }

        public override FloatRect GetLocalBounds()
        {
            return Obj!.GetLocalBounds();
        }
        public override FloatRect GetGlobalBounds()
        {
            return Obj!.GetGlobalBounds();
        }

        public override void OnUpdate(float deltaTime)
        {
            particle_counter += deltaTime;
            if (offset > 0)
                offset -= deltaTime;
            else
            {
                animation_counter += deltaTime;
                if (animation_counter > animationchange)
                {
                    if (animation_state == 1)
                    {
                        Obj!.Texture = animations[1];
                        animation_state = 2;
                        animation_counter = 0;
                    }
                    else
                    {
                        Obj!.Texture = animations[0];
                        animation_state = 1;
                        animation_counter = 0;
                    }
                }
            }
            if (this.speed <= 0)
                this.Destroy();
            if (traveledDistance < range)
            {
                if (particle_counter > particle_cooldown)
                {
                    Random random = new Random();
                    GameScreen.Instance.AddGameObject(new Particle(this.Position, new Vector2f(random.NextSingle(), random.NextSingle() + 1), 0.25f, 70f,false));
                    particle_counter = 0;
                }
                if (traveledDistance > range * 0.7f)
                {
                    speed -= 8f;
                }
                this.traveledDistance += deltaTime * speed;
                this.Position += direction * (speed * deltaTime);
                IGameObject? gameObject = GameScreen.Instance?.CheckCollisionBlacklist(this, ["Enemy","Boss"]);
                if (gameObject != null)
                    OnCollision(gameObject);
            }
            else
            {
                this.Destroy();
            }

        }

        private void OnCollision(IGameObject gameObject)
        {
            if (gameObject is Barrier)
            {
                this.Destroy();
            }
            if (gameObject is Player enemy)
            {
                if (this.GetGlobalBounds().Intersects(enemy.GetGlobalBounds()))
                {
                    Random random = new Random();
                    for (int i = 0; i < 5; i++)
                    {

                        GameScreen.Instance.AddGameObject(new Particle(this.Position, new Vector2f(random.NextSingle() + this.direction.X, random.NextSingle() + this.direction.Y), 0.25f, this.speed / 2,false));
                    }
                    this.Destroy();
                    enemy.TakeDamage(damage);
                }
            }
        }
    }
}
