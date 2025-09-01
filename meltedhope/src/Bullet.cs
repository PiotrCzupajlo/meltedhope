using SFML.Graphics;
using SFML.System;
using StadnardGameLib;

namespace meltedhope
{
    public class Bullet : GameObject
    {
        private static readonly Texture baseTexture = new Texture("assets/art/fireball.png");
        Vector2f direction;
        int damage;
        float speed = 750f;
        public Bullet(Vector2f position, Vector2f direction, int damage) : base(baseTexture, position)
        {
            this.direction = direction;
            this.damage = damage;
        }

        public override void OnUpdate(float deltaTime)
        {
            this.Position += direction * (speed * deltaTime);
            GameObject? gameObject = GameScreen.Instance?.CheckCollisionBlacklist(this, ["Player"]);
            if (gameObject != null)
                OnCollision(gameObject);
        }

        private void OnCollision(GameObject gameObject)
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
