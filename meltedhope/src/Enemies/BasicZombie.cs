using SFML.Graphics;
using SFML.System;

namespace meltedhope.Enemies
{
    public class BasicZombie : Enemy
    {
        static readonly List<Texture> walkTextures =
        [
            new Texture("assets/art/candle_enemy_1.png"),
            new Texture("assets/art/candle_enemy_2.png"),
        ];

        public BasicZombie(Vector2f position) : base(walkTextures, position, health: 5f, damage: 1f, speed: 100f)
        {
            this.Scale = new Vector2f(2f, 2f);
        }

        public override void OnDeletion()
        {
            //Drop the shit
        }
    }
}
