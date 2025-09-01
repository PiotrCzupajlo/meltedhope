using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using StadnardGameLib;

namespace meltedhope
{
    public class Barrier : GameObject
    {
        static readonly Texture baseTexture = new Texture("assets/art/blank.png");
        private Vector2f direction;
        public Barrier(Vector2f direction) : base(baseTexture)
        {
            this.Tag = "Barrier";
            this.IsVisible = false;

            this.direction = direction;
            this.Origin = new Vector2f(0, 0);
            //Top
            if (direction.Y == -1)
            {
                this.Scale = new Vector2f(Program.WindowWidth, -1);
                this.Position = new Vector2f(0, 0);
            }

            //Right
            else if (direction.X == 1)
            {
                this.Scale = new Vector2f(1, Program.WindowHeight);
                this.Position = new Vector2f(Program.WindowWidth, 0);
            }

            //Bottom
            else if (direction.Y == 1)
            {
                this.Scale = new Vector2f(Program.WindowWidth, 1);
                this.Position = new Vector2f(0, Program.WindowHeight);
            }

            //Left
            else if (direction.X == -1)
            {
                this.Scale = new Vector2f(-1, Program.WindowHeight);
                this.Position = new Vector2f(0, 0);
            }
        }

        public override void OnUpdate()
        {
            GameObject? gameObject = GameScreen.Instance?.CheckCollisionWhitelist(this, ["Player"]);
            if (gameObject == null)
                return;

            if (direction.X != 0)
                gameObject.Position = new Vector2f(this.Position.X + gameObject.Texture.Size.X / 2 * -direction.X, gameObject.Position.Y);

            else if (direction.Y != 0)
                gameObject.Position = new Vector2f(gameObject.Position.X, this.Position.Y + gameObject.Texture.Size.Y / 2 * -direction.Y);
        }
    }
}
