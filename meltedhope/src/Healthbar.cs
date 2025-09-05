using SFML.Graphics;
using SFML.System;
using StadnardGameLib;

namespace meltedhope
{
    public class Healthbar : GameObject<Sprite>
    {
        public static Healthbar? Instance;
        private static readonly Texture baseTexture = new Texture("assets/art/healthcandle.png");
        private static readonly Texture bodyTexture = new Texture("assets/art/body.png");
        private GameObject<Sprite> body;
        private Healthbar(Texture texture) : base(new Sprite(texture))
        {
            Obj!.Origin = new Vector2f(texture.Size.X / 2f, texture.Size.Y / 2f);
            this.body = new GameObject<Sprite>(new Sprite(bodyTexture));
            GameScreen.Instance?.AddGameObject(body);
            body.Obj!.Origin = new Vector2f(0, 0);
        }
        public Healthbar() : this(baseTexture)
        {
            Instance = this;
            this.Position = new Vector2f(1830, 500);
        }

        public override void OnUpdate(RenderWindow window, float deltatime, float clampx, float clampy)
        {

            if (GameScreen.Instance?.GetFirstByTag("Player") is not Player player)
                return;
            this.Position = new Vector2f(clampx+ 850, clampy- 50);

            this.body.HandleUpdate(window, deltatime, clampx, clampy);
            this.body.Position = new Vector2f(clampx+ 850, clampy- 50);

            float ratio = player.health / player.maxHealth;
            if (ratio < 0) ratio = 0;
            if (ratio > 1) ratio = 1;

            this.body.Obj!.Scale = new Vector2f(1, ratio);

            body.Position = new Vector2f(
                this.Position.X-45,
                this.Position.Y + ((Obj!.Texture.Size.Y * (1 - ratio) / 2)-43)
            );

        }
    }
}
