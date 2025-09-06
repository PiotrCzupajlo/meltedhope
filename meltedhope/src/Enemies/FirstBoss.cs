using meltedhope.Items;
using SFML.Graphics;
using SFML.System;
using StadnardGameLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meltedhope.src.Enemies
{
    public class FirstBoss:Enemy
    {
        public static Texture Texture = new SFML.Graphics.Texture("assets/art/first_boss_1.png");
        public readonly List<Texture> additional_elements
        = new List<Texture>()
        {
            new Texture("assets/art/boss_flame_1.png")
        };
        static readonly List<Texture> walkTextures =
                [
                    new Texture("assets/art/first_boss_1.png"),
            new Texture("assets/art/first_boss_1.png"),
        ];
        static readonly List<Texture> walkTexture_damaged = [
            new Texture("assets/art/first_boss_1.png"),
            new Texture("assets/art/first_boss_1.png")
            ];
        static readonly List<Texture> takingdamage = [
        new Texture("assets/art/first_boss_1.png"),
            new Texture("assets/art/first_boss_1.png")
        ];
        public List<GameObject<Sprite>> bodys;


        public FirstBoss(Vector2f position) : base(walkTextures, walkTexture_damaged, takingdamage, position, health: 5f, damage: 5f, speed: 200f, shadow_offset_x: 15, shadow_offset_y: -3, dynamic_mirrored_offset: -2)
        {
            Obj!.Scale = new Vector2f(2f, 2f);
            bodys= new List<GameObject<Sprite>>();
            bodys.Add(new GameObject<Sprite>(new Sprite(additional_elements[0])));
            

        }
        public override void OnUpdate(RenderWindow window,float deltatime)
        {
            

            if (health != 5f)
            {
                if (health < 3)
                    damagestate = 2;
                else
                    damagestate = 1;
            }

            if (Obj!.Scale == new Vector2f(-2, 2))
            {
                shadow.Position = new Vector2f(
                this.Position.X + shadow_offset_x,
                (this.Position.Y + this.GetGlobalBounds().Height / 2f) + shadow_offset_y);
                bodys.ElementAt(0).Position = new Vector2f(this.Position.X + 40, this.Position.Y - 280);
            }
            else
            {
                shadow.Position = new Vector2f(
                this.Position.X - shadow_offset_x + dynamic_mirrored_offset,
                (this.Position.Y + this.GetGlobalBounds().Height / 2f) + shadow_offset_y);
                bodys.ElementAt(0).Position = new Vector2f(this.Position.X - 37, this.Position.Y - 280);
            }
            base.OnUpdate();
            base.OnUpdate(window, deltatime);
            
            bodys.ElementAt(0).Obj!.Scale = this.Obj.Scale;

            foreach (GameObject<Sprite> body in bodys)
            { 
            GameScreen.Instance.AddGameObject(body);
            }
        }
        public override void OnDeletion()
        {
            foreach (GameObject<Sprite> body in bodys)
            {
                body.ToDestroy = true;
            }
            GameScreen.Instance?.AddGameObject(new Old_Wax(this.Position.X + 50, this.Position.Y));

            GameScreen.Instance?.AddGameObject(new YellowXpStar(this.Position.X - 50, this.Position.Y));

        }
    }
}
