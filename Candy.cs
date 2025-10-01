using SFML.Graphics;

namespace Pacman;

public class Candy : Entity
{
   public Candy() : base("pacman")
   {
      
   }

   public override void Create(Scene scene)
   {
      base.Create(scene);
      sprite.TextureRect = new IntRect(54, 36, 18, 18);
   }
   
   protected override void CollideWith(Scene scene, Entity entity)
   {
      if (entity is PacMan)
      {
         scene.Events.PublishCandyEaten(1);
         Dead = true;
      }
   }
}