using SFML.Graphics;
using SFML.System;

namespace Pacman;

public abstract class Entity
{
    private string textureName;
    protected Sprite sprite;
    public bool Dead = false;

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

    public FloatRect Bounds => sprite.GetGlobalBounds();

    public virtual bool Solid => false;

    public void Create(Scene scene)
    {
        sprite.Texture = scene.Assets.LoadTexture(textureName);
    }

    public virtual void Update(float deltaTime)
    {
        
    }

    public void Render(RenderTarget target)
    {
        target.Draw(sprite);
    }
}