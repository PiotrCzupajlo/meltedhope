using SFML.Graphics;
using SFML.System;

namespace StadnardGameLib
{
    public interface IGameObject
    {
        public Vector2f Position { get; set; }
        public Drawable? Drawable { get; set; }
        public Transformable? Transformable { get; set; }
        public string Tag { get; set; }
        public bool IsActive { get; set; }
        public bool IsVisible { get; set; }
        public bool IsCollidable { get; set; }
        public bool ToDestroy { get; set; }
        public void HandleUpdate(RenderWindow window, float deltaTime,float clampx,float clampy);
        public FloatRect GetLocalBounds();
        public FloatRect GetGlobalBounds();

        public void OnUpdate() { }
        public void OnUpdate(RenderWindow window) { }
        public void OnUpdate(float deltaTime) { }
        public void OnUpdate(RenderWindow window, float deltaTime) { }
        public void OnUpdate(RenderWindow window, float deltaTime,float clampx, float clampy) { }
        public void OnDeletion() { }
    }
}