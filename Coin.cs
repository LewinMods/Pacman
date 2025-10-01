using SFML.Graphics;

namespace Pacman;

public sealed class Coin : Entity
{
    public Coin() : base("pacman")
    {
        
    }

    public override void Create(Scene scene)
    {
        base.Create(scene);
        sprite.TextureRect = new IntRect(36, 36, 18, 18);
    }
    
    public override FloatRect Bounds
    {
        get
        {
            var bounds = base.Bounds;
            bounds.Left += 3;
            bounds.Width -= 6;
            bounds.Top += 3;
            bounds.Height -= 6;
            return bounds;
        }
    }
    
    protected override void CollideWith(Scene scene, Entity entity)
    {
        if (entity is PacMan)
        {
            scene.Events.PublishGainScore(100);
            Dead = true;
        }
    }
}