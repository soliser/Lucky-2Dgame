using System;
using System.Collections.Generic;
using System.Linq;
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

    public class enemyObject : graphicsObject
    {
  
        
        public float scale;
        public Vector2 direction;
        Vector2 intPos;

        
        
        public enemyObject(Vector2 position, Vector2 direction, float scale, int value, string tex)
        {
            Content.RootDirectory = "Content";
            active = false;
            this.value = value;
            this.direction = direction;
            this.position = position;
            this.scale = scale;
            intPos = position;
            texture = Texture2D.FromFile(Game1.graphics.GraphicsDevice, tex);
            width = texture.Width; height = texture.Height;
            center = new Vector2(width / 2, height / 2);
            color = new Color[texture.Width * texture.Height];
            texture.GetData(color);
        }
        public void spawn()
        {
            active = true;
            inbounds = true;
            position = intPos;
        }

        public void update(GameTime gameTime)
        {
            Vector2 scales = new Vector2(5f, 5f);
            Vector2 speed  = new Vector2(gameTime.ElapsedGameTime.Milliseconds / 5.0f, gameTime.ElapsedGameTime.Milliseconds / 5.0f);
            if (active == true)
            {
                position += (speed * scale * direction);
                if (inbounds == true)
                {
                    if (position.Y < -1000 || position.X < -1000 || position.Y > 1600 || position.X > 1600)
                    {
                        position = intPos;
                    }
                }
                if (inbounds == false)
                {
                    position = intPos;
                    active = true;
                    inbounds = true;
                }
            }
            
            
        }
        public void draw(SpriteBatch S)
        {

            //Adds a sprite to the batch of sprites to be rendered, specifying the texture, screen position, source rectangle, color tint, rotation, origin, scale, effects, and sort depth.  
            if (active == true)
            {
                S.Begin(SpriteBlendMode.AlphaBlend);  //Start drawing 2D images
                S.Draw(texture, position, new Rectangle(0, 0, texture.Width, texture.Height), Color.White, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 0);
                S.End();  //Stop drawing 2D images
            }
        }


    }
}