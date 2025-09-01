using OpenTK.Graphics.OpenGL;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace meltedhope
{
    public class Enemy: Sprite
    {
        public int Health { get; set; }
        public int Damage { get; set; }
        public float Speed { get; set; }
        public short TopLeftX { get; private set; }
        public short TopRightX { get; private set; }
        public short TopLeftY { get; private set; }
        public short BottomLeftY { get; private set; }
        public short attackcooldown { get; set; }
        public List<Texture> animation { get; set; }
        public short tick { get; set; }
        public short current_texture_id { get; set; }

        public Enemy(int health, int damage, float speed)
        {
            this.Health = health;
            this.Damage = damage;
            this.Speed = speed;

            //this.Origin = new Vector2f(texture.ElementAt(0).Size.X / 2f, texture.ElementAt(0).Size.Y / 2f);
            //this.Texture = texture.ElementAt(0);
            attackcooldown = 0;

            tick = 0;
            current_texture_id = 0;
        }
        public bool decreasehealth(int amount)
        {
            bool isdead = false;
            if (Health > amount)
                Health -= amount;
            else
                isdead = true;
            return isdead;
        }
        public  bool Update(Character character)
        {
            tick++;
            if (tick == 50)
            {
                tick = 0;
                current_texture_id++;
                if (current_texture_id >= animation.Count)
                    current_texture_id = 0;
                this.Texture = animation.ElementAt(current_texture_id);

            }
            if(attackcooldown>0)
                attackcooldown--;
            bool ischaracterdead = false;
            UpdateCorners();
            float deltaX = character.Position.X - Position.X;
            float deltaY = character.Position.Y - Position.Y;
            float length = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            if (length >10)
            {
                deltaX /= length;
                if (deltaX > 0) this.Scale = new Vector2f(2, 2);
                else this.Scale = new Vector2f(-2, 2);  
                deltaY /= length;
                Position = new Vector2f(Position.X + deltaX * Speed, Position.Y + deltaY * Speed);

            }
            else
            {
                //atack
                if (attackcooldown == 0)
                {
                    ischaracterdead = character.healthdecrease(Damage);
                    attackcooldown = 50;
                }
            }
            return ischaracterdead;
        }

        private void UpdateCorners()
        {
            FloatRect local = GetLocalBounds();
            Transform transform = Transform;

            Vector2f tl = new Vector2f(local.Left, local.Top);
            Vector2f tr = new Vector2f(local.Left + local.Width, local.Top);
            Vector2f bl = new Vector2f(local.Left, local.Top + local.Height);

            tl = transform.TransformPoint(tl);
            tr = transform.TransformPoint(tr);
            bl = transform.TransformPoint(bl);

            TopLeftX = (short)tl.X;
            TopLeftY = (short)tl.Y;

            TopRightX = (short)tr.X;

            BottomLeftY = (short)bl.Y;
        }
        public bool isColliding(float x, float y)
        {
            float minX = Math.Min(TopLeftX, TopRightX);
            float maxX = Math.Max(TopLeftX, TopRightX);
            float minY = Math.Min(TopLeftY, BottomLeftY);
            float maxY = Math.Max(TopLeftY, BottomLeftY);

            return (x >= minX && x <= maxX && y >= minY && y <= maxY);
        }
        public virtual void iskilled(List<Enemy> enemies, List<Item> items) { 
        
        
        }

    }
}
