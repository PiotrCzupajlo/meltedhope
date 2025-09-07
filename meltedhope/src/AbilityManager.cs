using StadnardGameLib;
using SFML.Window;
using SFML.System;
using meltedhope.src.Abilities;

namespace meltedhope.src
{
    public class AbilityManager
    {
        public static AbilityManager Instance;
        public List<Ability> abilities;
        public bool isChoosing = false;
        public int first = -1;
        public int second = -1;
        public AbilityManager()
        {
            Instance = this;
            abilities = new List<Ability>();

            abilities.Add(new Double_Shoot_Ability());
            abilities.Add(new WaxRegenerationAbility());
            abilities.Add(new Bigger_Flame_Ability());
            abilities.Add(new Bigger_Range_Ability());
            abilities.Add(new Faster_Reload_Ability());
            abilities.Add(new Speed_Ability());


        }
        public void Update(SFML.Graphics.RenderWindow window, float deltaTime)
        {
            Random random = new Random();
            if (isChoosing==false)
            {
                first = random.Next(0, abilities.Count - 1);
                second = 0;
                do
                {
                    second = random.Next(0, abilities.Count );

                } while (second == first);
                isChoosing = true;
            }
            Player player = GameScreen.Instance.GetFirstByTag("Player") as Player;
            abilities.ElementAt(first).Obj!.Position=new SFML.System.Vector2f(player.Position.X-150, player.Position.Y-180);
            abilities.ElementAt(second).Obj!.Position=new SFML.System.Vector2f(player.Position.X+150, player.Position.Y-180);
            window.Draw(abilities.ElementAt(first).Drawable);
            window.Draw(abilities.ElementAt(second).Drawable);
            window.MouseButtonPressed += (sender, e) =>
            {
                if (e.Button == Mouse.Button.Left)
                {
                    var mousePos = new Vector2i(e.X, e.Y);
                    var mouseWorldPos = window.MapPixelToCoords(mousePos);
                    
                    if (GameScreen.Instance.isPaused == true &&abilities.ElementAt(first).GetGlobalBounds().Contains(mouseWorldPos.X, mouseWorldPos.Y))
                    {
                        Console.WriteLine("Ability 1 chosen");
                        bool result = abilities.ElementAt(first).MakeAChange(player);
                        GameScreen.Instance.isPaused = false;
                        if(result)
                            abilities.RemoveAt(first);
                        isChoosing = false;

                    }
                    else if (GameScreen.Instance.isPaused == true &&abilities.ElementAt(second).GetGlobalBounds().Contains(mouseWorldPos.X, mouseWorldPos.Y) )
                    {
                        Console.WriteLine("Ability 2 chosen");
                        bool result = abilities.ElementAt(second).MakeAChange(player);
                        if(result)
                            abilities.RemoveAt(second);
                        GameScreen.Instance.isPaused=false;
                        isChoosing = false;
                    }


                }


            };

        }
    }
}
