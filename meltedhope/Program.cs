using SFML.Graphics;
using SFML.System;
using SFML.Window;
namespace meltedhope
{
    class Program
    {

        static void Main()
        {
            List<Sprite_Bullet> bullets = new List<Sprite_Bullet>();
            List<Barrier> barriers = new List<Barrier>();
            List<Enemy> enemies = new List<Enemy>();
            barriers.Add(new Barrier(0,0,1920,10)); //top
            barriers.Add(new Barrier(0,1070,1920,1080));//bottom
            barriers.Add(new Barrier(0,0,10,1080));//left
            barriers.Add(new Barrier(1910,0,1920,1080));//right
            var window = new RenderWindow(new VideoMode(1920, 1080), "MeltedHope",Styles.Fullscreen);
            Enemy enemy = new Enemy(new Texture("candle_enemy.png"), 100, 10, 2f);
            enemy.Position = new Vector2f(1800, 200);
            enemy.Scale= new Vector2f(2,2);
            enemies.Add(enemy);

            window.SetFramerateLimit(144);

            var texture = new Texture("candle_idle.png");
            Character player = new Character(texture,100);
            var texture2 = new Texture("candle_idle2.png");
            var texture3 = new Texture("candle_move1.png");
            var texture4 = new Texture("candle_move2.png");
            var texture_fireball = new Texture("fireball.png");
            var texture_background = new Texture("background.png");
            var texture_gameover = new Texture("gameover.png");
            Sprite background = new Sprite(texture_background);
            Sprite gameover = new Sprite(texture_gameover);
            gameover.Position = new Vector2f(760, 240);
            player.Position = new Vector2f(400, 300);

            float speed = 400f;
            Clock clock = new Clock();

            window.Closed += (sender, e) => window.Close();
            short tick = 0;
            short opt = 0;
            bool ismoving = false;
            short bullet_cooldown = 0;
            bool game = false;
            while (window.IsOpen)
            {
                if (!game)
                {
                    if (bullet_cooldown > 0)
                        bullet_cooldown--;
                    if (tick < 30)
                    {
                        tick++;
                    }
                    else
                    {
                        tick = 0;
                        if (opt == 0)
                        {
                            if (ismoving == false)
                                player.Texture = texture2;
                            else
                                player.Texture = texture3;
                            opt = 1;
                        }
                        else
                        {
                            opt = 0;
                            if (ismoving == false)
                                player.Texture = texture;
                            else
                                player.Texture = texture4;
                        }
                    }
                    ismoving = false;
                    window.DispatchEvents();

                    float delta = clock.Restart().AsSeconds();

                    if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                    {
                        bool skip = false;
                        foreach (var barrier in barriers)
                        {
                            if (barrier.isColliding(player.Position.X, player.Position.Y - speed * delta))
                            {
                                skip = true; break;
                            }
                        }
                        if (skip) continue;
                        player.Position += new Vector2f(0, -speed * delta);
                        ismoving = true;

                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                    {
                        bool skip = false;
                        foreach (var barrier in barriers)
                        {
                            if (barrier.isColliding(player.Position.X, player.Position.Y + speed * delta))
                            {
                                skip = true; break;
                            }
                        }
                        if (skip) continue;
                        player.Position += new Vector2f(0, speed * delta);
                        ismoving = true;
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                    {
                        bool skip = false;
                        foreach (var barrier in barriers)
                        {
                            if (barrier.isColliding(player.Position.X - speed * delta, player.Position.Y))
                            {
                                skip = true; break;
                            }
                        }
                        if (skip) continue;
                        player.Position += new Vector2f(-speed * delta, 0);
                        player.Scale = new Vector2f(-1, 1);
                        ismoving = true;
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                    {
                        bool skip = false;
                        foreach (var barrier in barriers)
                        {
                            if (barrier.isColliding(player.Position.X + speed * delta, player.Position.Y))
                            {
                                skip = true; break;
                            }
                        }
                        if (skip) continue;
                        player.Position += new Vector2f(speed * delta, 0);
                        player.Scale = new Vector2f(1, 1);
                        ismoving = true;
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                    {
                        if (bullet_cooldown == 0)
                        {
                            float x = player.Position.X;
                            float y = player.Position.Y;
                            bullets.Add(new Sprite_Bullet(texture_fireball, new Vector2f(x, y - 10), 0, 10));
                            bullet_cooldown = 50;
                        }

                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
                    {
                        if (bullet_cooldown == 0)
                        {
                            float x = player.Position.X;
                            float y = player.Position.Y;
                            bullets.Add(new Sprite_Bullet(texture_fireball, new Vector2f(x, y + 10), 1, 10));
                            bullet_cooldown = 50;
                        }
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                    {
                        if (bullet_cooldown == 0)
                        {
                            float x = player.Position.X;
                            float y = player.Position.Y;
                            bullets.Add(new Sprite_Bullet(texture_fireball, new Vector2f(x - 10, y), 2, 10));
                            bullet_cooldown = 50;
                        }
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                    {
                        if (bullet_cooldown == 0)
                        {
                            float x = player.Position.X;
                            float y = player.Position.Y;
                            bullets.Add(new Sprite_Bullet(texture_fireball, new Vector2f(x + 10, y), 3, 10));
                            bullet_cooldown = 50;
                        }
                    }
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                        window.Close();
                    window.Clear(new SFML.Graphics.Color(25, 50, 75));
                    window.Draw(background);
                    window.Draw(player);
                    bool ischaracterdead = false;
                    foreach (Enemy e in enemies)
                    {
                        ischaracterdead = e.Update(player);
                        window.Draw(e);
                        if (ischaracterdead)
                        {
                            game = true;
                            break;
                        }
                    }
                    for (int i = bullets.Count - 1; i >= 0; i--)
                    {
                        var toDestroy = bullets[i].Update(delta, barriers, enemies);

                        if (!toDestroy.Item1)
                        {
                            window.Draw(bullets[i]);

                        }
                        else
                        {
                            bullets.RemoveAt(i);
                            if (toDestroy.Item2 != -1)
                            {
                                enemies.RemoveAt(toDestroy.Item2);
                            }
                        }

                    }


                    window.Display();

                }
                else
                {
                    window.Clear(new SFML.Graphics.Color(39, 23, 17));
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                        window.Close();
                    window.Draw(gameover);
                    window.Display();
                }
            }
        }
    }
}