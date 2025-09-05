using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StadnardGameLib;
using SFML.System;

namespace meltedhope
{
    public class XpBar : GameObject<Sprite>
    {

        public static XpBar? Instance;
        private static readonly Texture baseTexture = new Texture("assets/art/border.png");
        private static readonly Texture bodyTexture = new Texture("assets/art/fill.png");
        private GameObject<Sprite> body;

        public XpBar() : base(new Sprite(baseTexture))
        {
            Obj!.Origin = new Vector2f(baseTexture.Size.X / 2f, baseTexture.Size.Y / 2f);
            body = new GameObject<Sprite>(new Sprite(bodyTexture));
            this.Position = new Vector2f(900, 900);
            body.Position = new Vector2f(758, 890);
            body.Obj!.Origin = new Vector2f(0, 0);
            body.Obj!.Scale = new Vector2f(0.5f, 1);
            GameScreen.Instance?.AddGameObject(body);
            Instance = this;
        }


        public override void OnUpdate(RenderWindow window, float deltatime,float clampx, float clampy)
        {
            if (GameScreen.Instance?.GetFirstByTag("Player") is not Player player)
                return;
            this.Position = new Vector2f(clampx, clampy + 300);

            this.body.HandleUpdate(window, deltatime, clampx, clampy);
            this.body.Position = new Vector2f(clampx - 142, clampy + 290);
            float ratio = (player.CurrentXp / player.XpToNextLvL)/2;
            if (ratio < 0) ratio = 0;
            if (ratio > 1) ratio = 0.5f;

            this.body.Obj!.Scale = new Vector2f(ratio, 1);


        }
    }
}
