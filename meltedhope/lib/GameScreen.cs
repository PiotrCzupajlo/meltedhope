using SFML.Graphics;

namespace StadnardGameLib
{
    public class GameScreen
    {
        public static GameScreen? Instance;
        public RenderWindow Window;
        public List<GameObject> GameObjects = [];
        private List<GameObject> CreationQueue = [];

        public GameScreen(RenderWindow window)
        {
            if (Instance != null)
                throw new Exception("Only one instance of GameScreen is allowed.");
            Instance = this;
            this.Window = window;

        }
        public void AddGameObject(GameObject obj)
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
                    List<GameObject> obj = GameObjects[i].OnDeletionCreateNewObj();
                        if(obj!=null)
                            AddListOfGameObjects(obj);  
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
        public void AddListOfGameObjects(List<GameObject> obj)
        {
            foreach (GameObject g in obj)
            { 
            CreationQueue.Add(g);
            }
        }

        public T? GetFirst<T>() where T : GameObject
        {
            return GameObjects.Find(gameObject => gameObject is T) as T;
        }

        public GameObject? GetFirstByTag(string tag)
        {
            return GameObjects.Find(gameObject => gameObject.Tag == tag);
        }

        public GameObject? CheckCollision(GameObject obj)
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

        public GameObject? CheckCollisionWhitelist(GameObject obj, List<string> tags)
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

        public GameObject? CheckCollisionBlacklist(GameObject obj, List<string> tags)
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
