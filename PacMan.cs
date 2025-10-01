using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Pacman;

public sealed class PacMan : Actor
{
    private List<List<IntRect>> frameList;

    private Animation animation;
    
    public PacMan(): base("pacman")
    {
        frameList = new List<List<IntRect>>()
        {
            new List<IntRect>()
            {
                new IntRect(0,0,18,18),
                new IntRect(18,0,18,18),
                new IntRect(36,54,18,18),
            },
            new List<IntRect>()
            {
                new IntRect(0,18,18,18),
                new IntRect(18,18,18,18),
                new IntRect(36,54,18,18),
            },
            new List<IntRect>()
            {
                new IntRect(0,36,18,18),
                new IntRect(18,36,18,18),
                new IntRect(36,54,18,18),
            },
            new List<IntRect>()
            {
                new IntRect(0,54,18,18),
                new IntRect(18,54,18,18),
                new IntRect(36,54,18,18),
            }
        };
    }

    public override void Create(Scene scene)
    {
        base.Create(scene);
        
        animation = new Animation(frameList[0]);
    }

    protected override int PickDirection(Scene scene)
    {
        int dir = direction;

        if (Keyboard.IsKeyPressed(Keyboard.Key.D))
        {
            dir = 0;
        }
        else if (Keyboard.IsKeyPressed(Keyboard.Key.W))
        {
            dir = 1;
        }
        else if (Keyboard.IsKeyPressed(Keyboard.Key.A))
        {
            dir = 2;
        }
        else if (Keyboard.IsKeyPressed(Keyboard.Key.S))
        {
            dir = 3;
        }
        
        if ((dir + 2) % 4 == direction) return direction;
        
        moving = dir >= 0;

        if (IsFree(scene, dir))
        {
            animation.ChangeFrames(frameList[dir]);
            
            return dir;
        }

        if (!IsFree(scene, direction)) moving = false;
        return direction;
    }

    public override void Update(Scene scene, float deltaTime)
    {
        sprite.TextureRect = moving ? animation.UpdateAnimation() : frameList[0][2];
        
        base.Update(scene, deltaTime);
    }
}