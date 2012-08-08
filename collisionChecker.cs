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
    class collisionChecker : Microsoft.Xna.Framework.Game
    {


        //Main method to check for a collision between two graphics objects
        public bool CheckCollisions(graphicsObject A, graphicsObject B)
        {
            Matrix ATransform, BTransform;
            Rectangle ARectangle, BRectangle;

            //transforms the rectangles which surrounds each sprite
            ATransform = Transform(A.center, 0.0f, A.position);
            ARectangle = TransformRectangle(ATransform, A.width, A.height);

            BTransform = Transform(B.center, 0.0f, B.position);
            BRectangle = TransformRectangle(BTransform, B.width, B.height);

            // collision checking
            if (ARectangle.Intersects(BRectangle))
            {// rough collision
                if (PixelCollision(A, B, ATransform, A.width, A.height,
                                   BTransform, B.width, B.height))
                    return true;  //What to do if there is a collision - in this case, stop the movement
            }
            return false;
        }  //End check collision



        //Checks to see if the pixels overlap in two graphicsObjects
        private bool PixelCollision(
          graphicsObject A, graphicsObject B,
          Matrix transformA, int pixelWidthA, int pixelHeightA,
          Matrix transformB, int pixelWidthB, int pixelHeightB)
        {

            //set A transformation relative to B.  B remains at (0,0)
            Matrix AtoB = transformA * Matrix.Invert(transformB);

            //Generate normal vectors to each rectangle's side
            Vector2 columnStep, rowStep, rowStartPosition;
            columnStep = Vector2.TransformNormal(Vector2.UnitX, AtoB);
            rowStep = Vector2.TransformNormal(Vector2.UnitY, AtoB);

            //Calculate the top left corner of A
            rowStartPosition = Vector2.Transform(Vector2.Zero, AtoB);

            // Search each row of pixels in A.  Start at the top and move down
            for (int rowA = 0; rowA < pixelHeightA; rowA++)
            {
                //begin at the left
                Vector2 pixelPositionA = rowStartPosition;
                // for each column in the row (move left to right)
                for (int colA = 0; colA < pixelWidthA; colA++)
                {
                    // get the pixel position
                    int X = (int)Math.Round(pixelPositionA.X);
                    int Y = (int)Math.Round(pixelPositionA.Y);

                    //if the pixel is within the bounds of B
                    if (X >= 0 && X < pixelWidthB && Y >= 0 && Y < pixelHeightB)
                    {
                        //Get colors of overlapping pixels
                        Color colorA = A.getPixelColor(colA + rowA * pixelWidthA);
                        Color colorB = B.getPixelColor(X + Y * pixelWidthB);

                        //If both pixels are not completely transparent
                        if (colorA.A != 0 && colorB.A != 0)
                            return true;  // Colision!
                    }
                    // move to the next pixel in the row of A
                    pixelPositionA += columnStep;
                }
                // move to the next row of A
                rowStartPosition += rowStep;
            }
            return false;
        } // End method PixelCollision 



        private static Rectangle TransformRectangle(Matrix transform, int width, int height)
        {

            //Get each corner of texture
            Vector2 leftTop = new Vector2(0.0f, 0.0f);
            Vector2 rightTop = new Vector2(width, 0.0f);
            Vector2 leftBottom = new Vector2(0.0f, height);
            Vector2 rightBottom = new Vector2(width, height);

            //Transform each corner
            Vector2.Transform(ref leftTop, ref transform, out leftTop);
            Vector2.Transform(ref rightTop, ref transform, out rightTop);
            Vector2.Transform(ref leftBottom, ref transform, out leftBottom);
            Vector2.Transform(ref rightBottom, ref transform, out rightBottom);

            //Find the minimum and maximum corners
            Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop), Vector2.Min(leftBottom, rightBottom));
            Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop), Vector2.Max(leftBottom, rightBottom));

            //Return the transformed rectangle
            return new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
        }//End Transform Rectangle

        //Creates a transformation matrix that rotates an object by "rotation" and moves it to "position"
        private Matrix Transform(Vector2 center, float rotation, Vector2 position)
        {
            // move to origin, scale (if desired), rotate, translate
            return Matrix.CreateTranslation(new Vector3(-center, 0.0f)) *
                // Add scaling here if you want
                          Matrix.CreateRotationZ(rotation) *
                          Matrix.CreateTranslation(new Vector3(position, 0.0f));
        }

           
    }
}
