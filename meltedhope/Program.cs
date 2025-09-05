using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using meltedhope;
using StadnardGameLib;
using meltedhope.src;

namespace meltedhope
{
    class Program
    {
        public static readonly uint WindowWidth = 1920;
        public static readonly uint WindowHeight = 1080;
        static void Main()
        {
            RenderWindow window = new RenderWindow(new VideoMode(WindowWidth, WindowHeight), "MeltedHope"/*,Styles.Fullscreen*/);
            window.SetFramerateLimit(144);
            window.Closed += (sender, e) => window.Close();
            EnemySpawningSystem enemySpawningSystem = new EnemySpawningSystem();
            View view = new View(new FloatRect(0, 0, WindowWidth, WindowHeight));
            AbilityManager abilityManager = new AbilityManager();
            var gameScreen = new GameScreen(window);
            gameScreen.AddGameObject(new Player(new Vector2f(4000, 4000)));
            gameScreen.AddGameObject(new BasicZombie(new Vector2f(800, 900)));
            gameScreen.AddGameObject(enemySpawningSystem);
            Healthbar healthbar = new Healthbar();
            XpBar xpBar = new XpBar();
            Texture mapTex = new Texture("assets/art/map.png");
            Sprite map = new Sprite(mapTex);
            Vector2f mapSize = new Vector2f(mapTex.Size.X, mapTex.Size.Y);
            Font arial = new Font("assets/fonts/arial.ttf");
            var Clock = new Clock();
            while (window.IsOpen)
            {
                window.Clear(new Color(25, 50, 75));
                window.Draw(map);

                float deltaTime = Clock.Restart().AsSeconds();
                window.DispatchEvents();


                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                    window.Close();
                if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                    gameScreen.isPaused = false;
                gameScreen.LoadFromQueue();
                Vector2f halfView = view.Size / 2f;
                Player player = gameScreen.GetFirstByTag("Player") as Player;
                float clampedX = MathF.Max(halfView.X, MathF.Min(player.Position.X, mapSize.X - halfView.X));
                float clampedY = MathF.Max(halfView.Y, MathF.Min(player.Position.Y, mapSize.Y - halfView.Y));
                gameScreen.Update(deltaTime,clampedX,clampedY);
                healthbar.HandleUpdate(window, deltaTime, clampedX, clampedY);
                xpBar.HandleUpdate(window, deltaTime, clampedX, clampedY);
                view.Center = new Vector2f(clampedX, clampedY);
                
                if (gameScreen.isPaused)
                {
                    
                    RectangleShape overlay = new RectangleShape(new Vector2f(WindowWidth, WindowHeight));
                    overlay.Position = new Vector2f(clampedX - WindowWidth / 2, clampedY - WindowHeight / 2);
                    overlay.FillColor = new Color(0, 0, 0, 150);
                    window.Draw(overlay);
                    abilityManager.Update(window,deltaTime);

                    Text pausedText = new Text("Choose your new ability", arial, 50);
                    pausedText.FillColor = Color.White;
                    FloatRect textRect = pausedText.GetLocalBounds();
                    pausedText.Origin = new Vector2f(textRect.Left + textRect.Width / 2.0f, textRect.Top + textRect.Height / 2.0f);
                    pausedText.Position = new Vector2f(clampedX, clampedY);
                    window.Draw(pausedText);
                }
                window.SetView(view);
                window.Display();
            }
        }


    }
}