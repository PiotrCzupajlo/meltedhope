using StadnardGameLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;

namespace meltedhope.src
{
    public class AbilityManager
    {
        public static AbilityManager Instance;
        public List<Ability> abilities;
        public AbilityManager()
        {
            Instance = this;
            abilities = new List<Ability>();

            abilities.Add(new Ability(new Texture("assets/art/ds_card.png")));
            abilities.Add(new Ability(new Texture("assets/art/wr_card.png")));
        }
        public void Update(SFML.Graphics.RenderWindow window, float deltaTime)
        {
            Player player = GameScreen.Instance.GetFirstByTag("Player") as Player;
            abilities.ElementAt(0).Position=new SFML.System.Vector2f(700, 250);
            abilities.ElementAt(1).Position=new SFML.System.Vector2f(950, 250);
            window.Draw(abilities.ElementAt(0));
            window.Draw(abilities.ElementAt(1));
            window.MouseButtonPressed += (sender, e) =>
            {
                if (e.Button == Mouse.Button.Left)
                {
                    var mousePos = new Vector2i(e.X, e.Y);
                    var mouseWorldPos = window.MapPixelToCoords(mousePos);
                    if (abilities.ElementAt(0).GetGlobalBounds().Contains(mouseWorldPos.X, mouseWorldPos.Y)&& GameScreen.Instance.isPaused==true)
                    {
                        Console.WriteLine("Ability 1 chosen");
                        player.current_bullet_multiplyer++;
                        player.damagefromburn += 0.001f;
                        GameScreen.Instance.isPaused = false;

                    }
                    else if (abilities.ElementAt(1).GetGlobalBounds().Contains(mouseWorldPos.X, mouseWorldPos.Y)&& GameScreen.Instance.isPaused==true)
                    {
                        Console.WriteLine("Ability 2 chosen");
                        player.damagefromburn -= 0.001f;
                        GameScreen.Instance.isPaused=false;
                    }


                }


            };

        }
    }
}
