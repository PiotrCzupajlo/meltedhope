using SFML.Graphics;
using SFML.System;
using StadnardGameLib;

namespace meltedhope
{
    public class Healthbar : GameObject
    {
        private static readonly Texture baseTexture = new Texture("assets/art/healthcandle.png");
        private static readonly Texture bodyTexture = new Texture("assets/art/body.png");
        private GameObject body;
        public Healthbar() : base(baseTexture)
        {
            this.Position = new Vector2f(1830, 500);
            this.body = new GameObject(bodyTexture, this.Position);
            body.Origin = new Vector2f(0, 0);
            GameScreen.Instance?.AddGameObject(body);
        }

        public override void OnUpdate()
        {
            var player = GameScreen.Instance?.GetFirst<Player>();
            if (player == null)
                return;

            float ratio = player.health / player.maxHealth;
            if (ratio < 0) ratio = 0;
            if (ratio > 1) ratio = 1;

            this.body.Scale = new Vector2f(1, ratio);

            this.body.Position = new Vector2f(
                this.Position.X-45,
                this.Position.Y + ((this.Texture.Size.Y * (1 - ratio) / 2)-43)
            );
        }
    }
}
