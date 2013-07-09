using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace WindowsGame1
{
    class BulletObject : GraphicsObject
    {
        public BulletObject(string tex)
        {
            Content.RootDirectory = "Content";
            Active = false;
            using (var fileStream = new FileStream(tex, FileMode.Open))
            {
                Texture = Texture2D.FromStream(Game1.Graphics.GraphicsDevice, fileStream);
            }
            Width = Texture.Width; Height = Texture.Height;
            Center = new Vector2(x: Width / 2, y: Height / 2);
            Color = new Color[Texture.Width * Texture.Height];
            Texture.GetData(Color);
        }
        public void Fire(Vector2 start)
        {
            start.Y -= 20;
            Position = start;
            Active = true;
            IsVisible = true;
        }
        public void update(GameTime gameTime)
        {
            const float scale = 60.0f;
            float speed = gameTime.ElapsedGameTime.Milliseconds / 75.0f;
            Position.Y -= speed * scale;
            if (Position.Y < 0)
            {
                Active = false;
                IsVisible = false;
            }


          
        }
        public void draw(SpriteBatch S)
        {
            //Adds a sprite to the batch of sprites to be rendered, specifying the texture, screen position, source rectangle, color tint, rotation, origin, scale, effects, and sort depth.  
            if (Active)
            {
                S.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);  //Start drawing 2D images
                S.Draw(Texture, Position, new Rectangle(0, 0, Texture.Width, Texture.Height), Microsoft.Xna.Framework.Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                S.End();  //Stop drawing 2D images
            }

        }
    }
}
