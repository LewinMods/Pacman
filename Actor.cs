using SFML.Graphics;
using SFML.System;

namespace Pacman;

public abstract class Actor : Entity
{
    private bool wasAligned;
    public float speed = 60;
    protected int direction;
    protected bool moving;
    private Vector2f originalPosition;

    private Clock clock;
    
    protected Actor(string textureName) : base(textureName)
    {
        ZIndex = 1;
        clock = new Clock();
    }

    protected void Reset()
    {
        clock.Restart();
        
        speed = 0;
        
        moving = false;
        wasAligned = false;
        Position = originalPosition;
        direction = -1;
    }

    public override void Create(Scene scene)
    {
        originalPosition = Position;
        
        base.Create(scene);
        Reset();
        
        scene.Events.LoseHealth += OnLoseHealth;
    }
    
    protected bool IsAligned =>
    (int) MathF.Floor(Position.X) % 18 == 0 &&
    (int) MathF.Floor(Position.Y) % 18 == 0;

    protected bool IsFree(Scene scene, int dir)
    {
        Vector2f at = Position + new Vector2f(9, 9);
        at += 18 * ToVector(dir);
        FloatRect rect = new FloatRect(at.X, at.Y, 1, 1);
        return !scene.FindIntersects(rect).Any(e => e.Solid);
    }

    protected static Vector2f ToVector(int dir)
    {
        switch (dir)
        {
            case 0: return new Vector2f(1, 0);
            case 1: return new Vector2f(0, -1);
            case 2: return new Vector2f(-1, 0);
            default: return new Vector2f(0, 1);
        }
    }

    protected abstract int PickDirection(Scene scene);

    public override void Update(Scene scene, float deltaTime)
    {
        if (speed <= 0)
        {
            if (clock.ElapsedTime.AsSeconds() >= 1)
            {
                speed = 60;
                moving = true;
            }
        }
        
        base.Update(scene, deltaTime);

        if (IsAligned)
        {
            if (!wasAligned)
            {
                direction = PickDirection(scene);
            }

            if (moving)
            {
                wasAligned = true;
            }
        }

        else
        {
            wasAligned = false;
        }

        if (!moving) return;
        
        Position += ToVector(direction) * (speed * deltaTime);
        
        Position = MathF.Floor(Position.X) switch
        {
            < 0 => new Vector2f(414, Position.Y),
            > 414 => new Vector2f(0, Position.Y),
            _ => Position
        };
    }
    
    public void OnLoseHealth(Scene scene, int health)
    {
        Reset();
    }
    
    public override void Destroy(Scene scene)
    {
        base.Destroy(scene);
        scene.Events.LoseHealth -= OnLoseHealth;
    }
}