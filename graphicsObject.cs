using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    //This class can be used as a base class for any graphics object (In this case, a rock and a space ship)
    public class GraphicsObject : Game
    {
        public bool Active;
        public bool IsVisible=false;
        public Vector2 Position;
        public Vector2 Speed;
        public Vector2 Center;  
        public int Width, Height; 
        public Texture2D Texture; 
        public Color[] Color;
        public int Value;
        public bool Inbounds;

        //Returns the pixel color from the "Color" array given an index
        public Color GetPixelColor(int pixelNum)
        {
            try
            {
                if (pixelNum < 1 || pixelNum >= Width * Height) throw new IndexOutOfRangeException();
            }
            catch (IndexOutOfRangeException)
            {
                return Microsoft.Xna.Framework.Color.White;
            }
            return Color[pixelNum];
        }

    } //****************************************** End class graphicsObject    
}
