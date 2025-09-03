using SFML.Graphics;
using SFML.System;

namespace StadnardGameLib
{
    public class GameObjectNew<T> : IGameObject
    {
        public Vector2f Position { get; set; } = new Vector2f(0, 0);
        public Drawable? drawable { get; set; }
        public Transformable? transformable { get; set; }
        public string Tag { get; set; } = "Untagged";
        public bool IsActive { get; set; } = true;
        public bool IsVisible { get; set; } = true;
        public bool IsCollidable { get; set; } = true;
        public bool ToDestroy { get; set; } = false;

        public virtual FloatRect GetLocalBounds()
        {
            return new FloatRect(0, 0, 0, 0);
        }

        public virtual FloatRect GetGlobalBounds()
        {
            return new FloatRect(0, 0, 0, 0);
        }

        public void HandleUpdate(RenderWindow window, float deltaTime)
        {
            if (!IsActive) return;
            if (transformable != null) transformable.Position = Position;
            if (GameScreenNew.Instance?.isPaused == false)
            {
                OnUpdate();
                OnUpdate(window);
                OnUpdate(deltaTime);
                OnUpdate(window, deltaTime);
            }
            if (IsVisible && drawable != null)
                GameScreenNew.Instance?.Window.Draw(drawable);
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
    }
}
