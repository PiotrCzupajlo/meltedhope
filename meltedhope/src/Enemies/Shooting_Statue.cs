using meltedhope.Items;
using SFML.Graphics;
using SFML.System;
using StadnardGameLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace meltedhope.src.Enemies
{
    public class Shooting_Statue:Enemy
    {
        public static readonly List<Texture> additional_elements
= new List<Texture>()
{
            new Texture("assets/art/enemy_standing/fire.png")
};
        static readonly List<Texture> walkTextures =
        [
            new Texture("assets/art/enemy_standing/standing_enemy1.png"),
            new Texture("assets/art/enemy_standing/standing_enemy2.png"),
            new Texture("assets/art/enemy_standing/standing_enemy3.png"),
            new Texture("assets/art/enemy_standing/standing_enemy4.png"),
            new Texture("assets/art/enemy_standing/standing_enemy5.png"),
            new Texture("assets/art/enemy_standing/standing_enemy6.png"),
            new Texture("assets/art/enemy_standing/standing_enemy7.png"),
            new Texture("assets/art/enemy_standing/standing_enemy8.png"),
            new Texture("assets/art/enemy_standing/standing_enemy9.png"),
            new Texture("assets/art/enemy_standing/standing_enemy10.png"),
            new Texture("assets/art/enemy_standing/standing_enemy11.png"),
            new Texture("assets/art/enemy_standing/standing_enemy12.png")
        ];
        static readonly List<Texture> walkTexture_damaged = [
            new Texture("assets/art/enemy_new_1.png"),
            new Texture("assets/art/enemy_new_7.png"),
            new Texture("assets/art/enemy_new_8.png"),
            new Texture("assets/art/enemy_new_5.png"),
            new Texture("assets/art/enemy_new_6.png"),
            ];
        static readonly List<Texture> attack_textures = [
            new Texture("assets/art/enemy_new_4.png"),
            new Texture("assets/art/enemy_new_2.png"),
            new Texture("assets/art/enemy_new_3.png"),


            ];
        static readonly List<Texture> takingdamage = [
            new Texture("assets/art/enemy_standing/standing_enemy15.png"),
            new Texture("assets/art/enemy_standing/standing_enemy14.png"),
            new Texture("assets/art/enemy_standing/standing_enemy13.png")


    ];
        public float attack_cooldown = 2f;
        public float attack_counter = 0;
        public List<GameObject<Sprite>> bodys;
        public bool lock_attack_animation = false;
        public Shooting_Statue(Vector2f position) : base(walkTextures, walkTexture_damaged, takingdamage, position, health: 10f, damage: 1f, speed: 50f, shadow_offset_x: 15, shadow_offset_y: -3, dynamic_mirrored_offset: -20, 25f, 10, 80, 50, -100, 100, 170)
        {
            this.canmove = false;
            Obj!.Scale = new Vector2f(3f, 3f);
            bodys = new List<GameObject<Sprite>>();
            bodys.Add(new GameObject<Sprite>(new Sprite(additional_elements[0])));
        }
        public override void OnUpdate(RenderWindow window, float deltatime)
        {

            if (health != 5f)
            {
                if (health < 3)
                    damagestate = 2;
                else
                    damagestate = 1;
            }

            if (Obj!.Scale == new Vector2f(-3, 3))
            {
                shadow.Position = new Vector2f(
                this.Position.X + shadow_offset_x,
                (this.Position.Y + this.GetGlobalBounds().Height / 2f) + shadow_offset_y);
                this.hitbox.Position = new Vector2f(this.Position.X - hitbox_offset_x + dynamic_mirrored_offset, this.Position.Y + hitbox_offset_y);
                bodys.ElementAt(0).Position = new Vector2f(this.Position.X - 30, this.Position.Y - 120);
            }
            else
            {
                shadow.Position = new Vector2f(
                this.Position.X - shadow_offset_x + dynamic_mirrored_offset,
                (this.Position.Y + this.GetGlobalBounds().Height / 2f) + shadow_offset_y);
                this.hitbox.Position = new Vector2f(this.Position.X - 100+hitbox_offset_x, this.Position.Y + hitbox_offset_y);
                bodys.ElementAt(0).Position = new Vector2f(this.Position.X - 30, this.Position.Y - 120);
            }
            bodys.ElementAt(0).Obj!.Scale = this.Obj.Scale;
            changed_hitbox_position = true;
            base.OnUpdate(window, deltatime);
            attack(deltatime);


            foreach (GameObject<Sprite> body in bodys)
            {
                GameScreen.Instance.AddGameObject(body);
            }
        }
        public void attack(float deltatime)
        {
            attack_counter += deltatime;
            if (attack_counter > attack_cooldown)
            {
                attack_counter = 0;
                Player player = GameScreen.Instance?.GetFirstByTag("Player") as Player;
                float deltaX = player.Position.X - this.Position.X;
                if (deltaX < -50)
                    deltaX = -1;
                else if (deltaX > 50)
                    deltaX = 1;
                else
                    deltaX = 0;
                float deltaY = player.Position.Y - this.Position.Y;
                if(deltaY<-50)
                    deltaY = -1;
                else if(deltaY>50)
                    deltaY = 1;
                else
                    deltaY = 0;
                GameScreen.Instance?.AddGameObject(new Enemy_bullet(new Vector2f(this.Position.X, this.Position.Y), new Vector2f(deltaX, deltaY), damage, 500));
            }

        }
        public override void OnDeletion()
        {
            foreach(GameObject<Sprite> body in bodys )
            {
                body.ToDestroy = true;
            }
            GameScreen.Instance?.AddLessImportant(new Old_Wax(this.Position.X + 50, this.Position.Y));

            GameScreen.Instance?.AddLessImportant(new YellowXpStar(this.Position.X - 50, this.Position.Y));

        }
    }
}
