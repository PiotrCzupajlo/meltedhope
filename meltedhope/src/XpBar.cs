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
    public class XpBar : GameObject
    {

        public static XpBar? Instance;
        private static readonly Texture baseTexture = new Texture("assets/art/border.png");
        private static readonly Texture bodyTexture = new Texture("assets/art/fill.png");
        private GameObject body;
        public XpBar():base(baseTexture) {
            Instance = this;
            this.Position = new Vector2f(900, 900);
            this.body = new GameObject(bodyTexture, new Vector2f(758,890));
            body.Origin = new Vector2f(0, 0);
            this.body.Scale = new Vector2f(0.5f, 1);
            GameScreen.Instance?.AddGameObject(body);
        }
        public override void OnUpdate()
        {
            var player = GameScreen.Instance?.GetFirst<Player>();
            if (player == null)
                return;

            float ratio = (player.CurrentXp / player.XpToNextLvL)/2;
            if (ratio < 0) ratio = 0;
            if (ratio > 1) ratio = 0.5f;

            this.body.Scale = new Vector2f(ratio, 1);

            //this.body.Position = new Vector2f(
            //    this.Position.X - 45,
            //    this.Position.Y + ((this.Texture.Size.Y * (1 - ratio) / 2) - 43)
            //);
        }
    }
}
