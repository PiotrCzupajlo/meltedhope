using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using meltedhope.Enemies;
namespace meltedhope
{
    class Program
    {

        static void Main()
        {
            #region lists
            List<Sprite_Bullet> bullets = new List<Sprite_Bullet>();
            List<Barrier> barriers = new List<Barrier>();
            List<Enemy> enemies = new List<Enemy>();
            List<Item> items_ground = new List<Item>();
            #endregion
            #region initializing stuff
            barriers.Add(new Barrier(0,0,1920,10)); //top
            barriers.Add(new Barrier(0,1070,1920,1080));//bottom
            barriers.Add(new Barrier(0,0,10,1080));//left
            barriers.Add(new Barrier(1910,0,1920,1080));//right
            var texture = new Texture("assets/candle_idle.png");
            Character player = new Character(texture, new Vector2f(400, 300));
            var texture2 = new Texture("assets/candle_idle2.png");
            var texture3 = new Texture("assets/candle_move1.png");
            var texture4 = new Texture("assets/candle_move2.png");
            var texture_fireball = new Texture("assets/fireball.png");
            var texture_background = new Texture("assets/background.png");
            var texture_gameover = new Texture("assets/gameover.png");
            RenderWindow window = new RenderWindow(new VideoMode(1920, 1080), "MeltedHope"/*,Styles.Fullscreen*/);

            BasicZombie basicZombie = new BasicZombie();
            basicZombie.Position = new Vector2f(1800, 200);
            enemies.Add(basicZombie);
            
            window.SetFramerateLimit(144);


            Sprite background = new Sprite(texture_background);
            Sprite gameover = new Sprite(texture_gameover);
            gameover.Position = new Vector2f(760, 240);
            Music music = new Music("assets/track.ogg");
            music.Loop = true;
            music.Volume = 5;
            music.Play();
            Texture healthcandle = new Texture("assets/healthcandle.png");
            Texture bodyt = new Texture("assets/body.png");
            Sprite body = new Sprite(bodyt);
            Healthbar healthbar = new Healthbar(healthcandle,body);
            healthbar.Position = new Vector2f(1830, 500);
            float speed = 400f;
            Clock clock = new Clock();
            #endregion
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
                    #region walking and idle animation
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
                    #endregion
                    window.DispatchEvents();

                    float delta = clock.Restart().AsSeconds();
                    #region moving character
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
                        if (!skip)
                        {
                            player.changeposition(new Vector2f(0, -speed * delta));
                            ismoving = true;
                        }

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
                        if (!skip)
                        {
                            player.changeposition(new Vector2f(0, speed * delta));
                            ismoving = true;
                        }
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
                        if (!skip)
                        {
                            player.changeposition(new Vector2f(-speed * delta, 0));
                            player.Scale = new Vector2f(-1, 1);
                            ismoving = true;
                        }
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
                        if (!skip)
                        {
                            player.changeposition(new Vector2f(speed * delta, 0));
                            player.Scale = new Vector2f(1, 1);
                            ismoving = true;
                        }
                    }
                    #endregion
                    #region making bullets
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
                    #endregion
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                        window.Close();
                    window.Clear(new SFML.Graphics.Color(25, 50, 75));
                    window.Draw(background);
                    window.Draw(player.shadow);
                    window.Draw(player);

                    bool ischaracterdead = false;
                    #region iterating through the enemies and bullets
                    foreach (Enemy e in enemies)
                    {
                        ischaracterdead = e.Update(player);
                        window.Draw(e.shadow);
                        window.Draw(e);
                        if (ischaracterdead)
                        {
                            game = true;
                            break;
                        }
                    }
                    for (int i = bullets.Count - 1; i >= 0; i--)
                    {
                        var toDestroy = bullets[i].Update(delta, barriers, enemies, items_ground);

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
                    for (int i = items_ground.Count - 1; i >= 0; i--)
                    { 
                    bool result = items_ground[i].Update(player);
                        if (result)
                        {
                            items_ground.RemoveAt(i);
                        }
                        else
                        {

                            window.Draw(items_ground[i].shadow);
                            window.Draw(items_ground[i]);
                        }
                    }

                    #endregion
                    healthbar.Update(player.health,100);
                    window.Draw(healthbar);
                    window.Draw(healthbar.body);

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