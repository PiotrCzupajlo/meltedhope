using meltedhope.src;
using OpenTK.Graphics.OpenGL;
using SFML.Graphics;
using SFML.System;
using StadnardGameLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace meltedhope
{
    public class Enemy : GameObject<Sprite>
    {
        public float health;
        public float damage;
        public float speed;
        private readonly List<Texture> walkTextures;
        private readonly List<Texture> walktextures_state1;
        private readonly List<Texture> taking_damage_textuers;
        public EllipseShape shadow;
        public float shadow_offset_x;
        public float shadow_offset_y;
        public float dynamic_mirrored_offset;
        public int damagestate = 0;
        public bool lock_animation_damage = false;
        public float animationcooldown = 0.05f;
        public float animation_counter = 0;
        public int current_texture = 0;
        public Vector2f knocback_direction=new Vector2f();
        private float walking_particle_cooldown = 0.1f;
        private float walking_particle_timer = 0f;
        private int ground_particle_x;
        private int ground_particle_y;
        public RectangleShape hitbox;
        public bool attacking_player=false;
        public float counter_after_attack = 0;
        public float animation_after_attack = 0.1f;
        public int attack_animation_frame = 0;
        public float hitbox_offset_x { get; set; }
        public float hitbox_offset_y { get; set; }
        public bool changed_hitbox_position = false;
        public Enemy(List<Texture> walkTextures,List<Texture> walk_damaged,List<Texture> taking_damage, Vector2f position , float health, float damage, float speed, float shadow_offset_x, float shadow_offset_y, float dynamic_mirrored_offset, float shadow_size, int ground_particle_x, int ground_particle_y,float hitbox_offsex_x, float hitbox_offset_y, float hitbox_width, float hitbox_height) : base(new Sprite(walkTextures[0]))
        {
            this.walkTextures = walkTextures;
            Position = position;
            Obj!.Origin = new Vector2f(walkTextures[0].Size.X / 2f, walkTextures[0].Size.Y / 2f);
            this.Tag = "Enemy";
            this.health = health;
            this.damage = damage;
            this.speed = speed;

            shadow = new EllipseShape(shadow_size, new Vector2f(2f, 0.5f));
            shadow.FillColor = new Color(0, 0, 0, 120);
            shadow.Origin = new Vector2f(shadow.Radius, shadow.Radius);
            this.shadow_offset_x = shadow_offset_x;
            this.shadow_offset_y = shadow_offset_y;
            this.dynamic_mirrored_offset = dynamic_mirrored_offset;
            this.walktextures_state1 = walk_damaged;
            taking_damage_textuers = taking_damage;
            this.ground_particle_x= ground_particle_x;
            this.ground_particle_y = ground_particle_y;
            this.hitbox = new RectangleShape(new Vector2f(hitbox_width , hitbox_height ));
            this.hitbox_offset_x = hitbox_offsex_x;
            this.hitbox_offset_y = hitbox_offset_y;
        }
        public Enemy() { }

        public override FloatRect GetLocalBounds()
        {
            return Obj!.GetLocalBounds();
        }
        public override FloatRect GetGlobalBounds()
        {
            return Obj!.GetGlobalBounds();
        }

        private float animationTimer = 0f;

        public override void OnUpdate(RenderWindow window,float deltaTime)
        {
            FloatRect bounds = this.Obj.GetGlobalBounds();
            if(changed_hitbox_position==false)
                hitbox.Position = new Vector2f(bounds.Left + hitbox_offset_x, bounds.Top + hitbox_offset_y);
            window.Draw(shadow);
            //window.Draw(hitbox);
            changed_hitbox_position = false;
            animationTimer += deltaTime;
            if (lock_animation_damage == false)
            {
                if (GameScreen.Instance?.GetFirstByTag("Player") is Player player)
                    GoToPlayer(player, deltaTime);
            }
            else
            { 
            this.Position += knocback_direction * (speed * deltaTime);
            }
            HandleAnimation(deltaTime);

            if (health <= 0)
                this.Destroy();
        }

        private void GoToPlayer(Player player, float deltaTime)
        {
            float deltaX = player.Position.X - Position.X;
            float deltaY = player.Position.Y - Position.Y;
            float length = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

            var direction = new Vector2f(deltaX / length, deltaY / length);

            if (direction.X > 0)
                Obj!.Scale = new Vector2f(Math.Abs(Obj!.Scale.X), Obj!.Scale.Y);
            if (direction.X < 0)
                Obj!.Scale = new Vector2f(-Math.Abs(Obj!.Scale.X), Obj!.Scale.Y);

            if (length > 10)
            {

                this.Position += direction * (speed * deltaTime);
                walking_particle_timer += deltaTime;
                if (walking_particle_timer > walking_particle_cooldown)
                {
                    walking_particle_timer = 0;
                    Random random = new Random();
                    for (int i = 0; i < 2; i++)
                    {
                        if (i < 1)
                            GameScreen.Instance.AddLessImportant(new Ground_Particle(new Vector2f(this.Position.X + random.Next(this.ground_particle_x, this.ground_particle_x + 51), this.Position.Y + this.ground_particle_y), new Vector2f(random.NextSingle() + 1, random.NextSingle()), 0.2f, 100f, new Vector2f(3, 3)));
                        else
                        {
                            GameScreen.Instance.AddLessImportant(new Ground_Particle(new Vector2f(this.Position.X - random.Next(this.ground_particle_x, this.ground_particle_x + 51), this.Position.Y + this.ground_particle_y), new Vector2f(random.NextSingle() - 1, random.NextSingle()), 0.2f, 100f, new Vector2f(-3, 3)));

                        }
                    }
                }
            }
            else
            {
                player.TakeDamage(damage);
                attacking_player = true;
            }
        }

        void HandleAnimation(float deltatime)
        {
            if (attacking_player == false)
            {
                if (lock_animation_damage == false)
                {
                    if (damagestate == 1)
                    {
                        int frame = (int)(animationTimer * 5) % walkTextures.Count;
                        Obj!.Texture = walkTextures[frame];
                    }
                    else
                    {
                        int frame = (int)(animationTimer * 3) % walkTextures.Count;
                        Obj!.Texture = walktextures_state1[frame];
                    }
                }
                else
                {

                    animation_counter += deltatime;
                    if (animation_counter > animationcooldown)
                    {
                        current_texture++;
                        animation_counter = 0;
                    }
                    int frame = current_texture;


                    if (frame == taking_damage_textuers.Count)
                    {
                        lock_animation_damage = false;
                        current_texture = 0;
                        frame--;
                    }
                    Obj!.Texture = taking_damage_textuers[frame];

                }
            }
        }

        public void TakeDamage(float damage,Vector2f knockback_direction)
        {
            this.knocback_direction = knockback_direction;
            lock_animation_damage = true;
            this.health -= damage;
        
        }
    }
}
