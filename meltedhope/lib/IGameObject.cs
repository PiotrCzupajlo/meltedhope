using SFML.Graphics;
using SFML.System;

namespace StadnardGameLib
{
    public interface IGameObject
    {
        public Vector2f Position { get; set; }
        public Drawable? drawable { get; set; }
        public Transformable? transformable { get; set; }
        public string Tag { get; set; }
        public bool IsActive { get; set; }
        public bool IsVisible { get; set; }
        public bool IsCollidable { get; set; }
        public bool ToDestroy { get; set; }
        public void HandleUpdate(RenderWindow window, float deltaTime);
        public FloatRect GetLocalBounds();
        public FloatRect GetGlobalBounds();

        public void OnUpdate() { }
        public void OnUpdate(RenderWindow window) { }
        public void OnUpdate(float deltaTime) { }
        public void OnUpdate(RenderWindow window, float deltaTime) { }
        public void OnDeletion() { }
    }
}