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
            // EnemySpawningSystem enemySpawningSystem = new EnemySpawningSystem();
            Sprite background = new Sprite(new Texture("assets/art/background.png"));

            AbilityManager abilityManager = new AbilityManager();
            var gameScreen = new GameScreen(window);
            List<Item> items = new List<Item>();
            gameScreen.AddGameObject(new PlayerNew(new Vector2f(400, 300)));
            // gameScreen.AddGameObject(new Barrier(new Vector2f(0, -1)));
            // gameScreen.AddGameObject(new Barrier(new Vector2f(1, 0)));
            // gameScreen.AddGameObject(new Barrier(new Vector2f(0, 1)));
            // gameScreen.AddGameObject(new Barrier(new Vector2f(-1, 0)));
            // gameScreen.AddGameObject(new BasicZombie(new Vector2f(800, 900)));
            // gameScreen.AddGameObject(new XpBar());
            // gameScreen.AddGameObject(new Healthbar());
            // gameScreen.AddListOfGameObjects(items.Cast<GameObject>().ToList());
            // gameScreen.AddGameObject(enemySpawningSystem);
            Font arial = new Font("assets/fonts/arial.ttf");
            var Clock = new Clock();
            while (window.IsOpen)
            {
                window.Clear(new Color(25, 50, 75));
                window.Draw(background);

                float deltaTime = Clock.Restart().AsSeconds();
                window.DispatchEvents();


                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                    window.Close();
                if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                    gameScreen.isPaused = false;
                gameScreen.Update(deltaTime);

                if (gameScreen.isPaused)
                {
                    
                    RectangleShape overlay = new RectangleShape(new Vector2f(WindowWidth, WindowHeight));
                    overlay.FillColor = new Color(0, 0, 0, 150);
                    window.Draw(overlay);
                    abilityManager.Update(window,deltaTime);


                    Text pausedText = new Text("Choose your new ability", arial, 50);
                    pausedText.FillColor = Color.White;
                    FloatRect textRect = pausedText.GetLocalBounds();
                    pausedText.Origin = new Vector2f(textRect.Left + textRect.Width / 2.0f, textRect.Top + textRect.Height / 2.0f);
                    pausedText.Position = new Vector2f(WindowWidth / 2.0f, WindowHeight / 2.0f);
                    window.Draw(pausedText);
                }
                Healthbar.Instance?.OnUpdate();
                XpBar.Instance?.OnUpdate();
                window.Display();
            }
        }


    }
}