
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
                       new VideoMode(828, 828), "Pacman")) 
            {
                window.Closed += (o, e) => window.Close();
                
                window.SetFramerateLimit(60);
                
                window.SetView(new View(
                    new Vector2f(207, 207),
                    new Vector2f(450, 450)
                ));

                Scene scene = new Scene();

                Clock clock = new Clock();
                
                while (window.IsOpen) 
                {
                    window.DispatchEvents();
                    
                    float deltaTime = clock.Restart().AsSeconds();
                    deltaTime = MathF.Min(deltaTime, 0.01f);

                    scene.UpdateAll(deltaTime);

                    window.Clear(new Color(223, 246, 245));

                    scene.RenderAll(window);

                    window.Display();
                }
            }
        }
    }
}