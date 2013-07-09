using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace WindowsGame1
{
    class ExplosionObject : Game
    {
        public Vector2 Position;
        public int Width, Height;
        readonly Texture2D _texture;
        public bool Active;

        public ExplosionObject(Vector2 intPos, string tex)
        {
            Content.RootDirectory = "Content";
            Active = false;
            Position = new Vector2(intPos.X, intPos.Y);
            using (var fileStream = new FileStream(tex, FileMode.Open))
            {
                _texture = Texture2D.FromStream(Game1.Graphics.GraphicsDevice, fileStream);
            }
            Width = _texture.Width; Height = _texture.Height;
        
        }
        public void draw(SpriteBatch S)
        {
            //Adds a sprite to the batch of sprites to be rendered, specifying the texture, screen position, source rectangle, color tint, rotation, origin, scale, effects, and sort depth.  
            if (Active)
            {
                S.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);  //Start drawing 2D images
                S.Draw(_texture, Position, new Rectangle(0, 0, _texture.Width, _texture.Height), Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, -1);
                S.End();  //Stop drawing 2D images
            }

        }
    }
}
