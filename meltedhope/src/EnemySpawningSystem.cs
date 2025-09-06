using meltedhope.src.Enemies;
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
        public float bossspawntime;
        public float bosscooldown;

        public EnemySpawningSystem() : base(new Sprite())
        {
            spawntime = 0;
            spawncooldown = 2;
            bosscooldown = 2;
        }

        public override void OnUpdate(RenderWindow window, float deltaTime,float clampx, float clampy)
        {
            spawntime += deltaTime;
            bossspawntime+=deltaTime;
            if (spawntime > spawncooldown)
            {
                SpawnEnemy(new BasicZombie(new SFML.System.Vector2f(900,900)),window,clampx,clampy);
                spawntime = 0;
            }
            if (bosscooldown < bossspawntime)
            {
                SpawnEnemy(new FirstBoss(new SFML.System.Vector2f(900, 900)), window, clampx, clampy);
                bossspawntime = 0;
            }
        }

        public void SpawnEnemy(IGameObject enemy, RenderWindow window,float clampx, float clampy)
        {
            float x, y;
            Random rand = new Random();
            int side = rand.Next(0, 4);
            switch (side)
            {
                case 0: // Top
                    x = clampx;
                    y = -1000f+clampy;
                    break;
                case 1: // Right
                    x = 1000f+clampx;
                    y = clampy;
                    break;
                case 2: // Bottom
                    x = clampx;
                    y =  1000f+clampy;
                    break;
                case 3: // Left
                    x = -1000f+clampx;
                    y = clampy;
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
