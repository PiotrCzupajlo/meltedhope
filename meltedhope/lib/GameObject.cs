using SFML.Graphics;
using SFML.System;

namespace StadnardGameLib
{
    public class GameObject : Sprite
    {
        public string Tag = "Untagged";
        public bool IsActive = true;
        public bool IsVisible = true;
        public bool IsCollidable = true;
        public bool ToDestroy = false;


        public GameObject(Texture texture) : base(texture)
        {
            this.Origin = new Vector2f(texture.Size.X / 2f, texture.Size.Y / 2f);
        }

        public GameObject(Texture texture, Vector2f position) : base(texture)
        {
            this.Position = position;
            this.Origin = new Vector2f(texture.Size.X / 2f, texture.Size.Y / 2f);
        }

        public void HandleUpdate(RenderWindow window, float deltaTime)
        {
            OnUpdate();
            OnUpdate(window);
            OnUpdate(deltaTime);
            OnUpdate(window, deltaTime);
            if (IsVisible)
                GameScreen.Instance?.Window.Draw(this);
        }

        public void Destroy()
        {
            ToDestroy = true;
        }

        public virtual void OnUpdate() { }
        public virtual void OnUpdate(RenderWindow window) { }
        public virtual void OnUpdate(float deltaTime) { }
        public virtual void OnUpdate(RenderWindow window, float deltaTime) { }
        public virtual void OnDeletion() { }
        public virtual List<GameObject> OnDeletionCreateNewObj() { return null; }
    }
}
