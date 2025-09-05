using SFML.Graphics;
using SFML.System;
using StadnardGameLib;

namespace meltedhope
{
    public class Bullet : GameObject<Sprite>
    {
        public List<Texture> animations = new List<Texture>();
        private static readonly Texture baseTexture = new Texture("assets/art/fireball.png");
        private static readonly Texture Texture2 = new Texture("assets/art/fireball2.png");
        Vector2f direction;
        float damage;
        float speed = 750f;
        float range;
        float traveledDistance = 0f;
        float animationchange = 0.1f;
        float animation_state = 1;
        float animation_counter = 0;
        double offset;

        private Bullet(Texture texture) : base(new Sprite(texture))
        {
            Random random = new Random();
            this.offset = random.NextDouble() * 0.1;
            this.animations.Add(baseTexture);
            this.animations.Add(Texture2);
            Obj!.Origin = new Vector2f(texture.Size.X / 2f, texture.Size.Y / 2f);
        }
        public Bullet(Vector2f position, Vector2f direction, float damage, float range) : this(baseTexture)
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

            if (traveledDistance < range)
            {
                if (traveledDistance > range * 0.7f)
                {
                    speed -= 8f;
                }
                this.traveledDistance += deltaTime * speed;
                this.Position += direction * (speed * deltaTime);
                IGameObject? gameObject = GameScreen.Instance?.CheckCollisionBlacklist(this, ["Player"]);
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
            if (gameObject is Enemy enemy)
            {
                this.Destroy();
                enemy.TakeDamage(damage,direction);
            }
        }
    }
}
