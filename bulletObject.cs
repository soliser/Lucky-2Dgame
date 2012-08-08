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
    class bulletObject : graphicsObject
    {
        
        

        public bulletObject(string tex)
        {
            Content.RootDirectory = "Content";
            active = false;
            texture = Texture2D.FromFile(Game1.graphics.GraphicsDevice, tex);
            width = texture.Width; height = texture.Height;
            center = new Vector2(width / 2, height / 2);
            color = new Color[texture.Width * texture.Height];
            texture.GetData(color);
        }
        public void fire(Vector2 start)
        {
            start.Y -= 20;
            position = start;
            active = true;
            isVisible = true;
        }
        public void update(GameTime gameTime)
        {
            const float scale = 60.0f;
            float speed = gameTime.ElapsedGameTime.Milliseconds / 75.0f;
            position.Y -= speed * scale;
            if (position.Y < 0)
            {
                active = false;
                isVisible = false;
            }


          
        }
        public void draw(SpriteBatch S)
        {
            //Adds a sprite to the batch of sprites to be rendered, specifying the texture, screen position, source rectangle, color tint, rotation, origin, scale, effects, and sort depth.  
            if (active == true)
            {
                S.Begin(SpriteBlendMode.AlphaBlend);  //Start drawing 2D images
                S.Draw(texture, position, new Rectangle(0, 0, texture.Width, texture.Height), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                S.End();  //Stop drawing 2D images
            }

        }
    }
}
