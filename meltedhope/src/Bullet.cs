using SFML.Graphics;
using SFML.System;
using StadnardGameLib;

namespace meltedhope
{
    public class Bullet : GameObject<Sprite>
    {
        private static readonly Texture baseTexture = new Texture("assets/art/fireball.png");
        Vector2f direction;
        float damage;
        float speed = 750f;
        float range;
        float traveledDistance = 0f;

        private Bullet(Texture texture) : base(new Sprite(texture))
        {
            Obj!.Origin = new Vector2f(texture.Size.X / 2f, texture.Size.Y / 2f);
        }
        public Bullet(Vector2f position, Vector2f direction, float damage, float range) : this(baseTexture)
        {
            Position = position;
            this.direction = direction;
            this.damage = damage;
            this.range = range;

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
            if (traveledDistance < range)
            {
                if (traveledDistance > range * 0.8f)
                {
                    speed -= 10f;
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
                enemy.TakeDamage(damage);
            }
        }
    }
}
