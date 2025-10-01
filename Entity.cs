using SFML.Graphics;
using SFML.System;

namespace Pacman;

public abstract class Entity
{
    private string textureName;
    public Sprite sprite;
    public bool Dead = false;
    public int ZIndex = 0;

    public bool DontDestroyOnLoad;

    protected Entity(string textureName)
    {
        this.textureName = textureName;
        sprite = new Sprite();
    }

    public Vector2f Position
    {
        get => sprite.Position;
        set => sprite.Position = value;
    }

    public virtual FloatRect Bounds => sprite.GetGlobalBounds();

    public virtual bool Solid => false;

    public virtual void Create(Scene scene)
    {
        sprite.Texture = scene.Assets.LoadTexture(textureName);
    }

    public virtual void Update(Scene scene, float deltaTime)
    {
        foreach (Entity found in scene.FindIntersects(Bounds))
        {
            CollideWith(scene, found);
        }
    }

    public virtual void Render(RenderTarget target)
    {
        target.Draw(sprite);
    }

    protected virtual void CollideWith(Scene s, Entity other)
    {
        
    }

    public virtual void Destroy(Scene scene)
    {
        
    }
}