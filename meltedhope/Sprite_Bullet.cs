using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Window;
using System.Numerics;

namespace meltedhope
{
    public class Sprite_Bullet:Sprite
    {
        int direction = 0;
        int damage = 1;
        public Sprite_Bullet(Texture texture, Vector2f position, int direction, int damage)
        {
            this.Texture = texture;
            this.Position = position;
            this.direction = direction;
            this.Origin = new Vector2f(texture.Size.X / 2f, texture.Size.Y / 2f);
            this.damage = damage;
        }
        public ValueTuple<bool,short> Update(float deltaTime,List<Barrier> barriers,List<Enemy> enemies)
        {
            bool todestroy = false;
            float speed = 800f;
            Vector2f movement = new Vector2f(0, 0);
            switch (direction)
            {
                case 0:
                    movement.Y -= speed * deltaTime;
                    break;
                case 1:
                    movement.Y += speed * deltaTime;
                    break;
                case 2:
                    movement.X -= speed * deltaTime;
                    break;
                case 3:
                    movement.X += speed * deltaTime;
                    break;
            }
            this.Position += movement;
            foreach (Barrier barrier in barriers)
            {
                if (barrier.isColliding(this.Position.X + speed * deltaTime, this.Position.Y))
                {
                    todestroy = true; break;
                }
            }
            bool iskill = false;
            short enemyid = -1;
                foreach (Enemy enemy in enemies)
                {
                    if (enemy.isColliding(this.Position.X + speed * deltaTime, this.Position.Y))
                    {
                        todestroy = true;
                        iskill=enemy.decreasehealth(damage);
                        if(iskill)
                            enemyid = (short)enemies.IndexOf(enemy);
                    break;
                    }
                }
            
            return (todestroy,enemyid);
        }
        public bool checkcollision() {

            return true;
        }
    }
}
