using meltedhope.src;
using SFML.Graphics;

namespace StadnardGameLib
{
    public class GameScreen
    {
        public static GameScreen? Instance;
        public RenderWindow Window;
        public List<IGameObject> GameObjects = [];
        private List<IGameObject> CreationQueue = [];
        public bool isPaused = false;

        public GameScreen(RenderWindow window)
        {
            if (Instance != null)
                throw new Exception("Only one instance of GameScreen is allowed.");
            Instance = this;
            this.Window = window;
        }
        public void AddGameObject(IGameObject obj)
        {
            CreationQueue.Add(obj);
        }
        public void LoadFromQueue() {

            foreach (var gameObject in CreationQueue)
                GameObjects.Add(gameObject);
            CreationQueue.Clear();
        }
        public void Update(float deltaTime,float clampx,float clampy)
        {


            for (int i = 0; i < GameObjects.Count; i++)
            {
                if (GameObjects[i].ToDestroy)
                {
                    GameObjects[i].OnDeletion();
                    GameObjects.RemoveAt(i);
                    i--;
                }
            }
            foreach (var gameObject in GameObjects)
            {
                if (gameObject.IsActive)
                    gameObject.HandleUpdate(Window, deltaTime,clampx,clampy);
            }
        }

        public IGameObject? GetFirstByTag(string tag)
        {
            return GameObjects.Find(gameObject => gameObject.Tag == tag);
        }

        public IGameObject? CheckCollision(IGameObject obj)
        {
            foreach (var gameObject in GameObjects)
            {
                if (!gameObject.IsActive || !gameObject.IsCollidable || gameObject == obj)
                    continue;

                if (obj.GetGlobalBounds().Intersects(gameObject.GetGlobalBounds()))
                {
                    Console.WriteLine($"Collision detected between {obj.Tag} and {gameObject.Tag}");
                    return gameObject;
                }

            }
            return null;
        }

        public IGameObject? CheckCollisionWhitelist(IGameObject obj, List<string> tags)
        {
            foreach (var gameObject in GameObjects)
            {
                if (!gameObject.IsActive || !gameObject.IsCollidable || gameObject == obj || !tags.Contains(gameObject.Tag))
                    continue;
                var r1 = obj.GetGlobalBounds();
                var r2 = gameObject.GetGlobalBounds();


                if (obj.GetGlobalBounds().Intersects(gameObject.GetGlobalBounds()))
                    return gameObject;
            }
            return null;
        }

        public IGameObject? CheckCollisionBlacklist(IGameObject obj, List<string> tags)
        {
            foreach (var gameObject in GameObjects)
            {
                if (!gameObject.IsActive || !gameObject.IsCollidable || gameObject == obj || tags.Contains(gameObject.Tag))
                    continue;

                if (obj.GetGlobalBounds().Intersects(gameObject.GetGlobalBounds()))
                    return gameObject;
            }
            return null;
        }
    }
}
