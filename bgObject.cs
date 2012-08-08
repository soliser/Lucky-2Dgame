using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace WindowsGame1
{
    class bgObject : Microsoft.Xna.Framework.Game
    {
        public Vector2 position;
        public int width, height; 
        Texture2D texture;

        public bgObject(Vector2 intPos, string tex)
        {
            Content.RootDirectory = "Content";

            position = new Vector2(intPos.X, intPos.Y);
            texture = Texture2D.FromFile(Game1.graphics.GraphicsDevice, tex);
            width = texture.Width; height = texture.Height;
        
        }
        public void draw(SpriteBatch S)
        {
            //Adds a sprite to the batch of sprites to be rendered, specifying the texture, screen position, source rectangle, color tint, rotation, origin, scale, effects, and sort depth.  
          
                S.Begin(SpriteBlendMode.AlphaBlend);  //Start drawing 2D images
                S.Draw(texture, position, new Rectangle(0, 0, texture.Width, texture.Height), Color.White, 0.0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0);
                S.End();  //Stop drawing 2D images
           

        }
    }
}
