using SFML.Graphics;
using StadnardGameLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meltedhope.src
{
    public class EnemySpawningSystem:GameObject<Sprite>
    {
        public float spawntime;
        public float spawncooldown;
        public EnemySpawningSystem() : base(new Sprite())
        {
            spawntime = 0;
            spawncooldown = 5;
        }

        public override void OnUpdate(RenderWindow window, float deltaTime)
        {
            spawntime += deltaTime;
            if (spawntime > spawncooldown)
            {
                SpawnEnemy(new BasicZombie(new SFML.System.Vector2f(900,900)),window);
                spawntime = 0;
            }
        }

        public void SpawnEnemy(IGameObject enemy, RenderWindow window)
        {
            float x, y;
            Random rand = new Random();
            int side = rand.Next(0, 4);
            switch (side)
            {
                case 0: // Top
                    x = (float)rand.NextDouble() * window.Size.X;
                    y = -50f;
                    break;
                case 1: // Right
                    x = window.Size.X + 50f;
                    y = (float)rand.NextDouble() * window.Size.Y;
                    break;
                case 2: // Bottom
                    x = (float)rand.NextDouble() * window.Size.X;
                    y = window.Size.Y + 50f;
                    break;
                case 3: // Left
                    x = -50f;
                    y = (float)rand.NextDouble() * window.Size.Y;
                    break;
                default:
                    x = 0f;
                    y = 0f;
                    break;
            }
            enemy.Position = new SFML.System.Vector2f(x, y);
            GameScreen.Instance?.AddGameObject(enemy);
        }
    }
}
