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
    class playerObject : graphicsObject
    {

        public Vector2 bgSpeed;
        
        public playerObject(Vector2 intPos, Vector2 bgSpeed, string tex)
        {
            Content.RootDirectory = "Content";
            this.bgSpeed = bgSpeed;
            position = new Vector2(intPos.X, intPos.Y);
            texture = Texture2D.FromFile(Game1.graphics.GraphicsDevice, tex);
            width = texture.Width; height = texture.Height;
            center = new Vector2(width / 2, height / 2);
            color = new Color[texture.Width * texture.Height];
            texture.GetData(color);
            isVisible = true;
        }
        public void update(GameTime gameTime)
        {
            const float scale = 50.0f;
            float speed = gameTime.ElapsedGameTime.Milliseconds / 100.0f;

            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Right))
            {
                if (position.X < 720)
                {
                    position.X += speed * scale;
                }


                
            }
            else if (keyboard.IsKeyDown(Keys.Left))
            {
                if (position.X > 10)
                {
                    position.X -= speed * scale;
                }

            }
            if (keyboard.IsKeyDown(Keys.Up))
            {
                if(position.Y > 100) 
                {
                    position.Y -= speed * scale;
                }
                else 
                {
                    if(bgSpeed.Y<1000)
                    bgSpeed.Y +=5;
                }
            }
            else if (keyboard.IsKeyDown(Keys.Down))
            {
                if (position.Y < 550)
                {
                    position.Y += speed * scale;
                }
                else
                {
                    if(bgSpeed.Y>100)
                      bgSpeed.Y -= 10;
                }
            }
            

        }
        public void draw(SpriteBatch S)
        {
            //Adds a sprite to the batch of sprites to be rendered, specifying the texture, screen position, source rectangle, color tint, rotation, origin, scale, effects, and sort depth.  
          
                S.Begin(SpriteBlendMode.AlphaBlend);  //Start drawing 2D images
                S.Draw(texture, position, Color.White);
                S.End();  //Stop drawing 2D images
           

        }
    }
}
