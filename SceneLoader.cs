using System.Text;
using SFML.System;

namespace Pacman;

public class SceneLoader
{
    private readonly Dictionary<char, Func<Entity>> loaders;
    private string currentScene = "", nextScene = "";

    public SceneLoader()
    {
        loaders = new Dictionary<char, Func<Entity>>()
        {
            {'#', () => new Wall()},
            {'.', () => new Coin()}
        };
    }

    private bool Create(char symbol, out Entity created)
    {
        if (loaders.TryGetValue(symbol, out Func<Entity> loader))
        {
            created = loader();
            return true;
        }
        
        created = null;
        return false;
    }

    public void HandleSceneLoad(Scene scene)
    {
        if (nextScene == "") return;
        
        scene.Clear();
        
        string file =  $"assets/{nextScene}.txt";
        
        int lineNumber = 0;

        foreach (var line in File.ReadLines(file, Encoding.UTF8))
        {
            lineNumber++;
            
            if (line.Length == 0) continue;

            char[] lineArray = line.ToCharArray();

            for (int i = 0; i <= lineArray.Length - 1; i++)
            {
                if (Create(lineArray[i], out Entity created))
                {
                    scene.Spawn(created);
                    created.Position = new Vector2f((i - 1) * 18, lineNumber * 18);
                }
            }
        }
        
        currentScene = nextScene;
        nextScene = "";
    }

    public void Load(string scene) => nextScene = scene;
    public void Reload() => nextScene = currentScene;
}