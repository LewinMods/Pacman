using SFML.Graphics;

namespace Pacman;

public sealed class Ghost : Actor
{
    private List<IntRect> redFrames;
    private List<IntRect> blueFrames;

    private float frozenTimer = 0;
    
    private Animation animation;
    
    public Ghost() : base("pacman")
    {
        redFrames = new List<IntRect>()
        {
            new IntRect(36, 0, 18, 18),
            new IntRect(54, 0, 18, 18)
        };

        blueFrames = new List<IntRect>()
        {
            new IntRect(36, 18, 18, 18),
            new IntRect(54, 18, 18, 18)
        };

    }

    public override void Create(Scene scene)
    {
        base.Create(scene);
        
        moving = true;
        
        scene.Events.CandyEaten += StartTimer;
        
        animation = new Animation(redFrames);
    }

    protected override int PickDirection(Scene scene)
    {
        List<int> validMoves = new List<int>();
        
        for (int i = 0; i < 4; i++)
        {
            if ((i + 2) % 4 == direction) continue;
            
            if (IsFree(scene, i)) validMoves.Add(i);
        }
        
        int r = new Random().Next(0, validMoves.Count);
        return validMoves[r];
    }

    protected override void CollideWith(Scene scene, Entity entity)
    {
        if (entity is PacMan)
        {
            if (frozenTimer <= 0)
            {
                scene.Events.PublishLoseHealth(1);
            }
            else if (speed != 0)
            {
                Reset();
                scene.Events.PublishGainScore(2000);
            }
        }
    }

    private void StartTimer(Scene scene, int frozen)
    {
        frozenTimer = 5;
        animation.ChangeFrames(blueFrames);
    }
    
    public override void Destroy(Scene scene)
    {
        base.Destroy(scene);
        scene.Events.LoseHealth -= StartTimer;
    }
    
    public override void Update(Scene scene, float deltaTime) 
    {
        sprite.TextureRect = animation.UpdateAnimation();

        if (speed != 0)
        {
            speed = frozenTimer > 0 ? 40 : 60;
        }
        
        base.Update(scene, deltaTime);
        
        frozenTimer = MathF.Max(frozenTimer - deltaTime, 0.0f);
    }

    public override void Render(RenderTarget target)
    {
        if (frozenTimer <= 0)
        {
            animation.ChangeFrames(redFrames);
        }
        
        base.Render(target);
    }
}