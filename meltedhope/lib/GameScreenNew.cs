using meltedhope.src;
using SFML.Graphics;

namespace StadnardGameLib
{
    public class GameScreenNew
    {
        public static GameScreenNew? Instance;
        public RenderWindow Window;
        public List<GameObjectNew> GameObjects = [];
        private List<GameObjectNew> CreationQueue = [];
        public bool isPaused = false;

        public GameScreenNew(RenderWindow window)
        {
            if (Instance != null)
                throw new Exception("Only one instance of GameScreen is allowed.");
            Instance = this;
            this.Window = window;
        }
        public void AddGameObject(GameObjectNew obj)
        {
            CreationQueue.Add(obj);
        }

        public void Update(float deltaTime)
        {
            foreach (var gameObject in CreationQueue)
                GameObjects.Add(gameObject);
            CreationQueue.Clear();

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
                    gameObject.HandleUpdate(Window, deltaTime);
            }
        }

        public T? GetFirst<T>() where T : GameObjectNew
        {
            return GameObjects.Find(gameObject => gameObject is T) as T;
        }

        public GameObjectNew? GetFirstByTag(string tag)
        {
            return GameObjects.Find(gameObject => gameObject.Tag == tag);
        }

        public GameObjectNew? CheckCollision(GameObjectNew obj)
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

        public GameObjectNew? CheckCollisionWhitelist(GameObjectNew obj, List<string> tags)
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

        public GameObjectNew? CheckCollisionBlacklist(GameObjectNew obj, List<string> tags)
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
