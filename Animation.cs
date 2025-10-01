using SFML.Graphics;
using SFML.System;

namespace Pacman;

public class Animation
{
    private List<IntRect> frames;

    private IntRect chosen;

    private float time;

    private Clock clock;

    private int iteration = 0;
    
    public Animation(List<IntRect> frames)
    {
        clock = new Clock();
        
        this.frames = frames;

        chosen = frames[0];

        time = 125;
    }

    public IntRect UpdateAnimation()
    {
        time -= clock.ElapsedTime.AsMilliseconds();
        clock.Restart();
        
        if (time <= 0)
        {
            if (iteration == frames.Count)
            {
                iteration = 0;
            }
            
            chosen = frames[iteration];
            iteration++;
            
            time = 125;
        }
        return chosen;
    }

    public void ChangeFrames(List<IntRect> newFrames)
    {
        if (newFrames != null && newFrames != frames)
        {
            frames = newFrames;
            iteration = 0;
        }
    }
}