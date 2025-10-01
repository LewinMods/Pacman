using SFML.Graphics;
using SFML.System;

namespace Pacman;

public class GUI : Entity
{
    private Text scoreText;
    private Text highScoreText;
    
    private const int maxHealth = 5;
    private int currentHealth;
    private int currentScore;
    
    private int highScore;
    
    public GUI() : base("pacman")
    {
        scoreText = new Text();
        highScoreText = new Text();
        
        currentHealth = maxHealth;
        currentScore = 0;

        DontDestroyOnLoad = true;
    }

    public override void Create(Scene scene)
    {
        highScore = scene.saveFile.Load();
        
        base.Create(scene);
        sprite.TextureRect = new IntRect(72, 36, 18, 18);
        
        scoreText.Font = scene.Assets.LoadFont("pixel-font");
        highScoreText.Font = scene.Assets.LoadFont("pixel-font");
        
        scoreText.DisplayedString = "Score";
        scoreText.Scale = new Vector2f(0.5f, 0.5f);
        
        highScoreText.DisplayedString = "HighScore";
        highScoreText.Scale = new Vector2f(0.5f, 0.5f);
        
        currentHealth = maxHealth;
        
        scene.Events.LoseHealth += OnLoseHealth;
        scene.Events.GainScore += OnScoreGain;
    }

    public override void Render(RenderTarget target)
    {
        sprite.Position = new Vector2f(36, 396);
        
        for (int i = 0; i < maxHealth; i++)
        {
            sprite.TextureRect = i < currentHealth
                ? new IntRect(72, 36, 18, 18)
                : new IntRect(72, 0, 18, 18);

            base.Render(target);

            sprite.Position += new Vector2f(18, 0);
        }

        scoreText.DisplayedString = $"Score: {currentScore}";
        scoreText.Position = new Vector2f(408 - scoreText.GetGlobalBounds().Width, 396);
        
        highScoreText.DisplayedString = $"HighScore: {highScore}";
        highScoreText.Position = new Vector2f(408 - highScoreText.GetGlobalBounds().Width, 416);
        
        target.Draw(scoreText);
        target.Draw(highScoreText);
    }

    private void OnLoseHealth(Scene scene, int amount)
    {
        currentHealth += amount;

        if (currentHealth <= 0)
        {
            if (currentScore > highScore)
            {
                highScore = currentScore;
                scene.saveFile.Save(highScore);
            }

            currentScore = 0;
            
            scene.Loader.Reload();
            
            currentHealth  = maxHealth;
        }
    }

    private void OnScoreGain(Scene scene, int amount)
    {
        currentScore += amount;

        if (!scene.FindByType<Coin>(out _))
        {
            scene.Loader.Reload();
        }
    }
}