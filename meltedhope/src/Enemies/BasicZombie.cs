using meltedhope.Items;
using SFML.Graphics;
using SFML.System;
using StadnardGameLib;

namespace meltedhope
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

        public override List<GameObject> OnDeletionCreateNewObj()
        {
            List<GameObject> gameObjects = new List<GameObject>();
            Item old_wax = new Old_Wax(this.Position.X + 50, this.Position.Y);
            gameObjects.Add(old_wax);
            return gameObjects;
            //Drop the shit
        }
    }
}
