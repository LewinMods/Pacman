namespace Pacman;

public delegate void ValueChangedEvent(Scene scene, int value);

public class EventManager
{
    public event ValueChangedEvent GainScore;
    public event ValueChangedEvent LoseHealth;
    public event ValueChangedEvent CandyEaten;
    
    private int scoreGained;
    private int healthLost;
    private int candyEaten;
    
    public EventManager()
    {
        
    }

    public void Update(Scene scene, float deltaTime)
    {
        if (scoreGained != 0)
        {
            GainScore?.Invoke(scene, scoreGained);
            scoreGained = 0;
        }
        
        if (healthLost != 0)
        {
            LoseHealth?.Invoke(scene, healthLost);
            healthLost = 0;
        }
        
        if (candyEaten != 0)
        {
            CandyEaten?.Invoke(scene, candyEaten);
            candyEaten = 0;
        }
    }
    
    public void PublishGainScore(int amount) => scoreGained += amount;
    
    public void PublishLoseHealth(int amount) => healthLost -= amount;
    
    public void PublishCandyEaten(int amount) => candyEaten += amount;
}