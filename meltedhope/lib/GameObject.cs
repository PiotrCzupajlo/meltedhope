using SFML.Graphics;
using SFML.System;

namespace StadnardGameLib
{
    public class GameObject<T> : IGameObject
    {
        public T? Obj { get; set; }
        public Vector2f Position { get; set; } = new Vector2f(0, 0);
        public Drawable? Drawable { get; set; }
        public Transformable? Transformable { get; set; }
        public string Tag { get; set; } = "Untagged";
        public bool IsActive { get; set; } = true;
        public bool IsVisible { get; set; } = true;
        public bool IsCollidable { get; set; } = true;
        public bool ToDestroy { get; set; } = false;

        public GameObject() { }
        public GameObject(T obj)
        {
            Obj = obj;
            if (obj is Drawable drawable)
                Drawable = drawable;
            if (obj is Transformable transformable)
                Transformable = transformable;
        }

        public virtual FloatRect GetLocalBounds()
        {
            return new FloatRect(0, 0, 0, 0);
        }

        public virtual FloatRect GetGlobalBounds()
        {
            return new FloatRect(0, 0, 0, 0);
        }

        public void HandleUpdate(RenderWindow window, float deltaTime,float clampx, float clampy)
        {
            if (!IsActive) return;
            if (Transformable != null) Transformable.Position = Position;
            if (GameScreen.Instance?.isPaused == false)
            {
                OnUpdate();
                OnUpdate(window);
                OnUpdate(deltaTime);
                OnUpdate(window, deltaTime);
                OnUpdate(window, deltaTime, clampx,clampy);
            }
            if (IsVisible && Drawable != null)
                GameScreen.Instance?.Window.Draw(Drawable);
        }

        public void Destroy()
        {
            ToDestroy = true;
        }

        public virtual void OnUpdate() { }
        public virtual void OnUpdate(RenderWindow window) { }
        public virtual void OnUpdate(float deltaTime) { }
        public virtual void OnUpdate(RenderWindow window, float deltaTime) { }
        public virtual void OnUpdate(RenderWindow window, float deltaTime, float clampx, float clampy) { }
        public virtual void OnDeletion() { }
    }
}
