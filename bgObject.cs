﻿using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace WindowsGame1
{
    class BgObject : Game
    {
        public Vector2 Position;
        public int Width;
        public int Height;
        readonly Texture2D _texture;

        public BgObject(Vector2 intPos, string tex)
        {
            Content.RootDirectory = "Content";

            Position = new Vector2(intPos.X, intPos.Y);
            using (var fileStream = new FileStream(tex, FileMode.Open))
            {
                _texture = Texture2D.FromStream(Game1.Graphics.GraphicsDevice, fileStream);
            }
            Width = _texture.Width; Height = _texture.Height;
        
        }
        public void draw(SpriteBatch s)
        {
            //Adds a sprite to the batch of sprites to be rendered, specifying the texture, screen position, source rectangle, color tint, rotation, origin, scale, effects, and sort depth.  

            s.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);  //Start drawing 2D images
                s.Draw(_texture, Position, new Rectangle(0, 0, _texture.Width, _texture.Height), Color.White, 0.0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0);
                s.End();  //Stop drawing 2D images
           

        }
    }
}
