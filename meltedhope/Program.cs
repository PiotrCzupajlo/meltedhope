using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using meltedhope.Enemies;
using StadnardGameLib;

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
            Sprite background = new Sprite(new Texture("assets/art/background.png"));
            var gameScreen = new GameScreen(window);

            gameScreen.AddGameObject(new Player(new Vector2f(400, 300)));
            gameScreen.AddGameObject(new Barrier(new Vector2f(0, -1)));
            gameScreen.AddGameObject(new Barrier(new Vector2f(1, 0)));
            gameScreen.AddGameObject(new Barrier(new Vector2f(0, 1)));
            gameScreen.AddGameObject(new Barrier(new Vector2f(-1, 0)));
            gameScreen.AddGameObject(new BasicZombie(new Vector2f(800, 900)));

            var Clock = new Clock();
            while (window.IsOpen)
            {
                window.Clear(new Color(25, 50, 75));
                window.Draw(background);

                float deltaTime = Clock.Restart().AsSeconds();
                window.DispatchEvents();

                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                    window.Close();

                gameScreen.Update(deltaTime);

                window.Display();
            }
        }
    }
}