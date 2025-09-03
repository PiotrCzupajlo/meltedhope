using SFML.Graphics;
using SFML.System;

namespace StadnardGameLib
{
    public class GameObjectNew
    {
        public Vector2f Position = new Vector2f(0, 0);
        public Drawable? drawable;
        public Transformable? transformable;
        public string Tag = "Untagged";
        public bool IsActive = true;
        public bool IsVisible = true;
        public bool IsCollidable = true;
        public bool ToDestroy = false;

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
