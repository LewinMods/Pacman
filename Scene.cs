using SFML.Graphics;
using SFML.System;

namespace Pacman;

public class Scene
{
    private List<Entity> entities;
    public readonly SceneLoader Loader;
    public readonly AssetManager Assets;

    public Scene()
    {
        entities = new List<Entity>();
        Loader = new SceneLoader();
        Assets = new AssetManager();

        Spawn(new PacMan() { Position = new Vector2f(100, 100) });
    }

    public void Spawn(Entity entity)
    {
        entities.Add(entity);
        entity.Create(this);
    }

    public void UpdateAll(float deltaTime)
    {
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            entities[i].Update(deltaTime);
        }
    }

    public void RenderAll(RenderTarget target)
    {
        foreach (Entity entity in entities)
        {
            entity.Render(target);
        }
    }

    public bool FindByType<T>(out T found) where T : Entity
    {
        foreach (Entity entity in entities)
        {
            if (!entity.Dead && entity is T typed)
            {
                found = typed;
                return true;
            }
        }
        
        found = default(T);
        return false;
    }
}