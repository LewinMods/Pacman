using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Pacman;

public sealed class PacMan : Actor
{
    public PacMan(): base("pacman")
    {
        ZIndex = 1;
    }

    public override void Create(Scene scene)
    {
        speed = 100.0f;
        base.Create(scene);
        sprite.TextureRect = new IntRect(0, 0, 18, 18);
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
        
        if (IsFree(scene, dir)) return dir;

        if (!IsFree(scene, direction)) moving = false;
        return direction;
    }
}