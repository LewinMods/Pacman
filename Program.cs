
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Pacman 
{
    class Program 
    {
        static void Main(string[] args) 
        {
            using (var window = new RenderWindow(
                       new VideoMode(828, 900), "Pacman")) 
            {
                window.Closed += (o, e) => window.Close();
                
                window.SetFramerateLimit(60);
                
                window.SetView(new View(
                    new FloatRect(18,0,414,450)
                ));

                Scene scene = new Scene();

                Clock clock = new Clock();
                
                while (window.IsOpen) 
                {
                    window.DispatchEvents();
                    
                    float deltaTime = clock.Restart().AsSeconds();
                    deltaTime = MathF.Min(deltaTime, 0.1f);

                    scene.UpdateAll(deltaTime);

                    window.Clear(new Color(11, 75, 255));

                    scene.RenderAll(window);

                    window.Display();
                }
            }
        }
    }
}