using meltedhope.src;
using SFML.Graphics;

namespace StadnardGameLib
{
    public class GameScreen
    {
        public static GameScreen? Instance;
        public RenderWindow Window;
        public List<IGameObject> GameObjects = [];
        public List<IGameObject> lessimportant = [];
        public List<IGameObject> moreimportant = [];
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
        public void AddLessImportant(IGameObject obj)
        {
            lessimportant.Add(obj);
        }
        public void AddMoreImportant(IGameObject obj)
        {
            moreimportant.Add(obj);
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
            for(int i=0;i<lessimportant.Count;i++)
            {
                if (lessimportant[i].ToDestroy)
                {
                    lessimportant[i].OnDeletion();
                    lessimportant.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < moreimportant.Count; i++)
            {
                if (moreimportant[i].ToDestroy)
                {
                    moreimportant[i].OnDeletion();
                    moreimportant.RemoveAt(i);
                    i--;
                }
            }
            foreach (var gameobject in lessimportant)
            { 
            if(gameobject.IsActive)
                    gameobject.HandleUpdate(Window, deltaTime, clampx, clampy);
            }
            foreach (var gameObject in GameObjects)
            {
                if (gameObject.IsActive)
                    gameObject.HandleUpdate(Window, deltaTime,clampx,clampy);
            }
            foreach (var gameobject in moreimportant)
            {
                if (gameobject.IsActive)
                    gameobject.HandleUpdate(Window, deltaTime, clampx, clampy);

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
