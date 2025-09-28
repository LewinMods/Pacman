using SFML.Graphics;
using System.Collections.Generic;

namespace Pacman;

public class AssetManager
{
    public static readonly string AssetPath = "assets";
    private readonly Dictionary<string, Texture> textures;
    private readonly Dictionary<string, Font> fonts;

    public AssetManager()
    {
        textures = new Dictionary<string, Texture>();
        fonts = new Dictionary<string, Font>();
    }

    public Texture LoadTexture(string name)
    {
        if (textures.TryGetValue(name, out Texture foundTexture))
        {
            return foundTexture;
        }
        
        Texture texture = new Texture($"{AssetPath}/{name}.png");
        textures[name] = texture;
        return texture;
    }

    public Font LoadFont(string name)
    {
        if (fonts.TryGetValue(name, out Font foundFont))
        {
            return foundFont;
        }
        
        Font font = new Font($"{AssetPath}/{name}.png");
        fonts[name] = font;
        return font;
    }
}