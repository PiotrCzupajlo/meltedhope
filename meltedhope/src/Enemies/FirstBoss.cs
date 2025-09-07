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
        public static Texture Texture = new SFML.Graphics.Texture("assets/art/first_boss_new_1.png");
        public readonly List<Texture> additional_elements
        = new List<Texture>()
        {
            new Texture("assets/art/boss_flame_1.png")
        };
        static readonly List<Texture> jumptextures = [
            new Texture("assets/art/first_boss_new_jump_1.png"),
            new Texture("assets/art/first_boss_new_jump_2.png"),
            new Texture("assets/art/first_boss_new_jump_3.png")


            ];
        static readonly List<Texture> walkTextures =
                [
                    new Texture("assets/art/first_boss_new_1.png"),
            new Texture("assets/art/first_boss_new_1.png"),
        ];
        static readonly List<Texture> walkTexture_damaged = [
            new Texture("assets/art/first_boss_new_1.png"),
            new Texture("assets/art/first_boss_new_1.png")
            ];
        static readonly List<Texture> takingdamage = [
        new Texture("assets/art/first_boss_new_1.png"),
            new Texture("assets/art/first_boss_new_1.png")
        ];
        public List<GameObject<Sprite>> bodys;
        public float jumpcooldown = 10f;
        public float jumpcounter = 0f;
        public bool lock_jump_animation = false;
        public float jump_animation_counter = 0;
        public float jumpframes = 0;
        public float target_y=0;
        public FirstBoss(Vector2f position) : base(walkTextures, walkTexture_damaged, takingdamage, position, health: 5f, damage: 5f, speed: 200f, shadow_offset_x: 15, shadow_offset_y: -3, dynamic_mirrored_offset: -2,100f)
        {
            Obj!.Scale = new Vector2f(2f, 2f);
            bodys= new List<GameObject<Sprite>>();
            bodys.Add(new GameObject<Sprite>(new Sprite(additional_elements[0])));
            
        }
        public override void OnUpdate(RenderWindow window, float deltatime)
        {

            FloatRect bounds = this.Obj.GetGlobalBounds();
            RectangleShape border = new RectangleShape(new Vector2f(bounds.Width, bounds.Height));
            border.Position = new Vector2f(bounds.Left, bounds.Top);
            border.FillColor = Color.Transparent;   // no fill, just border
            border.OutlineThickness = 2f;           // border thickness
            border.OutlineColor = Color.Red;        // border color
            window.Draw(border);
            jumpcounter += deltatime;
            if (jumpcounter > jumpcooldown)
            {
                jumpcounter = 0;
                lock_jump_animation = true;
            }

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
                bodys.ElementAt(0).Position = new Vector2f(this.Position.X + 40, this.Position.Y - 180);
            }
            else
            {
                shadow.Position = new Vector2f(
                this.Position.X - shadow_offset_x + dynamic_mirrored_offset,
                (this.Position.Y + this.GetGlobalBounds().Height / 2f) + shadow_offset_y);
                bodys.ElementAt(0).Position = new Vector2f(this.Position.X - 37, this.Position.Y - 180);
            }
            if (lock_jump_animation == false)
            {
                base.OnUpdate();
                base.OnUpdate(window, deltatime);
            }
            else {
                jump_animation_counter += deltatime*2;
                if (jump_animation_counter > 0.5 && jumpframes == 0)
                {
                    this.Obj.Texture = jumptextures.ElementAt(0);
                    jumpframes = 1;
                }
                else if (jump_animation_counter > 1 && jumpframes == 1)
                {
                    this.Obj.Texture = jumptextures.ElementAt(1);
                    jumpframes = 2;

                }
                else if (jump_animation_counter > 1.5 && jumpframes == 2)
                {
                    this.Obj.Texture = jumptextures.ElementAt(2);
                    jumpframes = 3;
                    this.damage = 999;
                    rise(deltatime);
                    Player gameObject = GameScreen.Instance?.CheckCollisionWhitelist(this, ["Player"]) as Player;
                    if (gameObject != null)
                        gameObject.TakeDamage(damage);
                }
                else if (jump_animation_counter > 4 && jumpframes == 3)
                {
                    if (GameScreen.Instance?.GetFirstByTag("Player") is Player player)
                        jump(player, deltatime);
                    fall(deltatime);
                    jumpframes = 4;
                    Player gameObject = GameScreen.Instance?.CheckCollisionWhitelist(this, ["Player"]) as Player;
                    if (gameObject != null)
                        gameObject.TakeDamage(damage);

                }
                else if (jumpframes == 3)
                {
                    rise(deltatime);
                    Player gameObject = GameScreen.Instance?.CheckCollisionWhitelist(this, ["Player"]) as Player;
                    if (gameObject != null)
                        gameObject.TakeDamage(damage);
                }
                else if (jump_animation_counter > 4.5f && jumpframes == 4)
                {
                    this.Obj.Texture = jumptextures.ElementAt(1);
                    jumpframes = 5;
                    fall(deltatime);
                    Player gameObject = GameScreen.Instance?.CheckCollisionWhitelist(this, ["Player"]) as Player;
                    if (gameObject != null)
                        gameObject.TakeDamage(damage);
                }
                else if(jumpframes==4)
                {
                    fall(deltatime);
                    Player gameObject = GameScreen.Instance?.CheckCollisionWhitelist(this, ["Player"]) as Player;
                    if (gameObject != null)
                        gameObject.TakeDamage(damage);
                }
                else if (jump_animation_counter > 7 && jumpframes == 5)
                {
                    this.Obj.Texture = jumptextures.ElementAt(0);
                    jumpframes = 0;
                    lock_jump_animation = false;
                    jump_animation_counter = 0;
                    target_y = 0;
                    this.damage = 5;
                }
                else if(jumpframes==5)
                {
                    fall(deltatime);
                    Player gameObject = GameScreen.Instance?.CheckCollisionWhitelist(this, ["Player"]) as Player;
                    if (gameObject != null)
                        gameObject.TakeDamage(damage);
                }
            }
            
            bodys.ElementAt(0).Obj!.Scale = this.Obj.Scale;

            foreach (GameObject<Sprite> body in bodys)
            { 
            GameScreen.Instance.AddGameObject(body);
            }
        }
        public void jump(Player player, float deltatime)
        {
            this.Position = new Vector2f(player.Position.X, this.Position.Y);
        }
        public void rise(float deltatime) {
        this.Position += new Vector2f(0, -1000 ) * deltatime;
        }
        public void fall(float deltatime) {
        this.Position += new Vector2f(0, 1000 ) * deltatime;
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
