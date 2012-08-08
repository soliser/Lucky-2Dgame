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
    //This class can be used as a base class for any graphics object (In this case, a rock and a space ship)
    public class graphicsObject : Microsoft.Xna.Framework.Game
    {
        public bool active;
        public bool isVisible=false;
        public Vector2 position;
        public Vector2 speed;
        public Vector2 center;  
        public int width, height; 
        public Texture2D texture; 
        public Color[] color;
        public int value;
        public bool inbounds;

        // Constructor
        public graphicsObject()
        {
        }
        

        //Returns the pixel color from the "Color" array given an index
        public Color getPixelColor(int pixelNum)
        {
            try
            {
                if (pixelNum < 1 || pixelNum >= width * height) throw new IndexOutOfRangeException();
            }
            catch (IndexOutOfRangeException e)
            {
                return Color.White;
            }
            return color[pixelNum];
        }

    } //****************************************** End class graphicsObject    
}
