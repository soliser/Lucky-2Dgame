using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace WindowsGame1
{
    class PlayerObject : GraphicsObject
    {

        public Vector2 BgSpeed;
        
        public PlayerObject(Vector2 intPos, Vector2 bgSpeed, string tex)
        {
            Content.RootDirectory = "Content";
            BgSpeed = bgSpeed;
            Position = new Vector2(intPos.X, intPos.Y);
            using (var fileStream = new FileStream(tex, FileMode.Open))
            {
                Texture = Texture2D.FromStream(Game1.Graphics.GraphicsDevice, fileStream);
            }
            Width = Texture.Width; Height = Texture.Height;
            Center = new Vector2(x: Width / 2, y: Height / 2);
            Color = new Color[Texture.Width * Texture.Height];
            Texture.GetData(Color);
            IsVisible = true;
        }
        public void update(GameTime gameTime)
        {
            const float scale = 50.0f;
            float speed = gameTime.ElapsedGameTime.Milliseconds / 100.0f;

            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Right))
            {
                if (Position.X < 720)
                {
                    Position.X += speed * scale;
                }


                
            }
            else if (keyboard.IsKeyDown(Keys.Left))
            {
                if (Position.X > 10)
                {
                    Position.X -= speed * scale;
                }

            }
            if (keyboard.IsKeyDown(Keys.Up))
            {
                if(Position.Y > 100) 
                {
                    Position.Y -= speed * scale;
                }
                else 
                {
                    if(BgSpeed.Y<1000)
                    BgSpeed.Y +=5;
                }
            }
            else if (keyboard.IsKeyDown(Keys.Down))
            {
                if (Position.Y < 550)
                {
                    Position.Y += speed * scale;
                }
                else
                {
                    if(BgSpeed.Y>100)
                      BgSpeed.Y -= 10;
                }
            }
            

        }
        public void draw(SpriteBatch S)
        {
            //Adds a sprite to the batch of sprites to be rendered, specifying the texture, screen position, source rectangle, color tint, rotation, origin, scale, effects, and sort depth.  

                S.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);  //Start drawing 2D images
                S.Draw(Texture, Position, Microsoft.Xna.Framework.Color.White);
                S.End();  //Stop drawing 2D images
           

        }
    }
}
