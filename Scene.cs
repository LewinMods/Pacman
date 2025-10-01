using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Pacman;

public sealed class Scene
{
    private List<Entity> entities;
    
    public readonly SceneLoader Loader;
    public readonly AssetManager Assets;
    public readonly EventManager Events;
    public readonly SaveFile saveFile;

    public Scene()
    {
        entities = new List<Entity>();
        
        Loader = new SceneLoader();
        Assets = new AssetManager();
        Events = new EventManager();
        saveFile = new SaveFile("SaveFile");
        
        Loader.Load("maze");
    }

    public void Spawn(Entity entity)
    {
        entities.Add(entity);
        entity.Create(this);
        
        entities = entities.OrderBy(e => e.ZIndex).ToList();
    }

    public void UpdateAll(float deltaTime)
    {
        Loader.HandleSceneLoad(this);
        
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            entities[i].Update(this, deltaTime);
        }

        Events.Update(this, deltaTime);
        
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            if (entities[i].Dead)
            {
                Entity entity = entities[i];
                entities.RemoveAt(i);
                entity.Destroy(this);
            }
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

    public void Clear()
    {
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            Entity entity = entities[i];

            if (!entity.DontDestroyOnLoad)
            {
                entities.RemoveAt(i);
                entity.Destroy(this);
            }
        }
    }

    public IEnumerable<Entity> FindIntersects(FloatRect bounds)
    {
        int lastEntity = entities.Count - 1;

        for (int i = lastEntity; i >= 0; i--)
        {
            Entity entity = entities[i];
            if (entity.Dead) continue;
            if (entity.Bounds.Intersects(bounds))
            {
                yield return entity;
            }
        }
    }
}