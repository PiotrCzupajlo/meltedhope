using SFML.Graphics;
using SFML.System;
using StadnardGameLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meltedhope
{
    public class Item :GameObject<Sprite>
    {
        public EllipseShape shadow;
        public float offset= 0;
        public bool direction=false;
        public float realy = 0;
        public Item(Texture texture,float x, float y,float shadow_offset_x, float shadow_offset_y) : base(new Sprite(texture))
        { 
            this.Position = new Vector2f(x, y);
            shadow = new EllipseShape(10f, new Vector2f(2f, 0.5f));
            shadow.FillColor = new Color(0, 0, 0, 120);
            shadow.Origin = new Vector2f(shadow.Radius, shadow.Radius);
            shadow.Position = new Vector2f(
            this.Position.X+shadow_offset_x,
            (this.Position.Y + this.GetGlobalBounds().Height / 2f)+shadow_offset_y);
            this.IsCollidable = true;
            this.IsActive = true;

            realy = y;
        }
        public override void OnUpdate(RenderWindow window ,float deltatime)
        {
            window.Draw(shadow);
            if (!direction)
            {
                offset += 0.1f;
                if (offset >= 15)
                    direction = true;
            }
            else
            {
                offset -= 0.1f;
                if (offset <= -15)
                    direction = false;
            }
            this.Position = new SFML.System.Vector2f(this.Position.X, realy + offset);
            this.shadow.Scale = new Vector2f(2f - (offset / 100), 0.5f - (offset / 100));
            IGameObject? gameObject = GameScreen.Instance?.CheckCollisionWhitelist(this, ["Player"]);
            if (gameObject != null)
                OnCollision(gameObject);
        }
        public bool Update(Player character)
        {

            FloatRect itemBounds = this.GetGlobalBounds();
            FloatRect playerBounds = character.GetGlobalBounds();

            bool result = itemBounds.Intersects(playerBounds);

            if (result)
            {
                collectitem(character);
            }

            return result;
        }
        private void OnCollision(IGameObject gameObject)
        {
            if (gameObject is Player player)
            {
                this.collectitem(player);
                this.Destroy();
            }
        }

        public virtual void collectitem(Player character) { }
    }
}
