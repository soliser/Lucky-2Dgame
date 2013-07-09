using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{

    public class EnemyObject : GraphicsObject
    {
        public float Scale;
        public Vector2 Direction;
        readonly Vector2 _intPos;
   
        public EnemyObject(Vector2 position, Vector2 direction, float scale, int value, string tex)
        {
            Content.RootDirectory = "Content";
            Active = false;
            this.Value = value;
            Direction = direction;
            Position = position;
            Scale = scale;
            _intPos = position;
            using (var fileStream = new FileStream(tex, FileMode.Open))
            {
                Texture = Texture2D.FromStream(Game1.Graphics.GraphicsDevice, fileStream);
            } 
            Width = Texture.Width; Height = Texture.Height;
            Center = new Vector2(x: Width / 2, y: Height / 2);
            Color = new Color[Texture.Width * Texture.Height];
            Texture.GetData(Color);
        }
        public void Spawn()
        {
            Active = true;
            Inbounds = true;
            Position = _intPos;
        }

        public void update(GameTime gameTime)
        {
            var speed  = new Vector2(gameTime.ElapsedGameTime.Milliseconds / 5.0f, gameTime.ElapsedGameTime.Milliseconds / 5.0f);
            if (Active)
            {
                Position += (speed*Scale*Direction);
                if (Inbounds)
                {
                    if (Position.Y < -1000 || Position.X < -1000 || Position.Y > 1600 || Position.X > 1600)
                    {
                        Position = _intPos;
                    }
                }
                if (Inbounds == false)
                {
                    Position = _intPos;
                    Active = true;
                    Inbounds = true;
                }
            }
            
            
        }
        public void draw(SpriteBatch s)
        {

            //Adds a sprite to the batch of sprites to be rendered, specifying the texture, screen position, source rectangle, color tint, rotation, origin, scale, effects, and sort depth.  
            if (Active)
            {
                s.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend); //Start drawing 2D images
                s.Draw(Texture, Position, new Rectangle(0, 0, Texture.Width, Texture.Height), Microsoft.Xna.Framework.Color.White, 0.0f,
                       Vector2.Zero, Scale, SpriteEffects.None, 0);
                s.End(); //Stop drawing 2D images
            }
        }
    }
}