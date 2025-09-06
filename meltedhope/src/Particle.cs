using OpenTK.Windowing.GraphicsLibraryFramework;
using SFML.Graphics;
using SFML.System;
using StadnardGameLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meltedhope.src
{
    public class Particle:GameObject<Sprite>
    {
        private static readonly Texture baseTexture = new Texture("assets/art/particle.png");
        Vector2f direction;
        float speed = 70f;
        float lifetime = 0.5f;
        float life_counter = 0f;
        double offset;

        private Particle(Texture texture) : base(new Sprite(texture))
        {
            Random random = new Random();
            this.offset = random.NextDouble() * 0.1;
            Obj!.Origin = new Vector2f(texture.Size.X / 2f, texture.Size.Y / 2f);
        }
        public Particle(Vector2f position, Vector2f direction,float lifetime, float speed) : this(baseTexture)
        {
            Position = position;
            this.direction = direction;
            this.speed = speed;
            Obj.Texture = baseTexture;
            Obj.Scale = new Vector2f(3, 3);
            this.lifetime = lifetime;
        }

        public override FloatRect GetLocalBounds()
        {
            return Obj!.GetLocalBounds();
        }
        public override FloatRect GetGlobalBounds()
        {
            return Obj!.GetGlobalBounds();
        }

        public override void OnUpdate(float deltaTime)
        {
            
            life_counter += deltaTime;
            if (this.speed <= 0)
                this.Destroy();
            if (life_counter < lifetime)
            {
                if (life_counter> lifetime * 0.7f)
                {
                    speed -= 8f;
                }
                this.Position += direction * (speed * deltaTime);
                
            }
            else
            {
                this.Destroy();
            }
            

        }


    }
}

