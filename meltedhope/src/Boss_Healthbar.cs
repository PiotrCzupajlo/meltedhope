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
    public class Boss_Healthbar: GameObject<Sprite>
    {

        public static Healthbar? Instance;
        private static readonly Texture baseTexture = new Texture("assets/art/boss_healthbar/boss_healthbar1.png");
        private static readonly Texture bodyTexture = new Texture("assets/art/boss_healthbar/body.png");
        private GameObject<Sprite> body;
        private Boss_Healthbar(Texture texture) : base(new Sprite(texture))
        {
            Obj!.Origin = new Vector2f(texture.Size.X / 2f, texture.Size.Y / 2f);
            this.body = new GameObject<Sprite>(new Sprite(bodyTexture));
            GameScreen.Instance?.AddGameObject(body);
            body.Obj.Origin = new Vector2f(0, Obj.Texture.Size.Y / 2f);

            this.Obj.Scale = new Vector2f(3, 3);
            this.body.Obj.Scale = new Vector2f(3, 3);
        }
        public Boss_Healthbar() : this(baseTexture)
        {
        }

        public override void OnUpdate(RenderWindow window, float deltatime, float clampx, float clampy)
        {

            if (GameScreen.Instance?.GetFirstByTag("Boss") is not Enemy enemy)
                return;
            this.Position = new Vector2f(clampx , clampy - 500);

            this.body.HandleUpdate(window, deltatime, clampx, clampy);
            float ratio = enemy.health / enemy.maxHealth;
            ratio *= 3;
            if (ratio < 0) ratio = 0;
            if (ratio > 3) ratio = 3;

            this.body.Obj!.Scale = new Vector2f( ratio,3);

            body.Position = new Vector2f(this.Position.X-120 , this.Position.Y+32);

        }
    }
}
