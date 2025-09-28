using SFML.Graphics;
using SFML.System;

namespace Pacman;

public class PacMan : Entity
{
    public PacMan(): base("pacman")
    {
        sprite.TextureRect = new IntRect(0, 0, 18, 18);
        sprite.Origin = new Vector2f(9, 9);
    }

    public override void Update(float deltaTime)
    {
        Position += new Vector2f(100, 0) * deltaTime;
    }
}