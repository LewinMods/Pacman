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
            {'.', () => new Coin()},
            {'g', () => new Ghost()},
            {'p', () => new PacMan()},
            {'c', () => new Candy()}
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

        List<string> line = File.ReadLines(file, Encoding.UTF8).ToList();

        for (int y = 0; y < line.Count; y++)
        {
            if (line.Count == 0) continue;

            char[] lineArray = line[y].ToCharArray();

            for (int x = 0; x <= lineArray.Length - 1; x++)
            {
                if (Create(lineArray[x], out Entity created))
                {
                    created.Position = new Vector2f((x - 1) * 18, y * 18);
                    scene.Spawn(created);
                }
            }
        }
        
        currentScene = nextScene;
        nextScene = "";

        if (!scene.FindByType<GUI>(out _))
        {
            scene.Spawn(new GUI());
        }
    }

    public void Load(string scene) => nextScene = scene;
    public void Reload() => nextScene = currentScene;
}