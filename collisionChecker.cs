using System;
using Microsoft.Xna.Framework;


namespace WindowsGame1
{
    class CollisionChecker : Game
    {


        //Main method to check for a collision between two graphics objects
        public bool CheckCollisions(GraphicsObject a, GraphicsObject b)
        {
            //transforms the rectangles which surrounds each sprite
            Matrix aTransform = Transform(a.Center, 0.0f, a.Position);
            Rectangle aRectangle = TransformRectangle(aTransform, a.Width, a.Height);

            Matrix bTransform = Transform(b.Center, 0.0f, b.Position);
            Rectangle bRectangle = TransformRectangle(bTransform, b.Width, b.Height);

            // collision checking
            if (aRectangle.Intersects(bRectangle))
            {// rough collision
                if (PixelCollision(a, b, aTransform, a.Width, a.Height,
                                   bTransform, b.Width, b.Height))
                    return true;  //What to do if there is a collision - in this case, stop the movement
            }
            return false;
        }  //End check collision



        //Checks to see if the pixels overlap in two graphicsObjects
        private static bool PixelCollision(
          GraphicsObject a, GraphicsObject b,
          Matrix transformA, int pixelWidthA, int pixelHeightA,
          Matrix transformB, int pixelWidthB, int pixelHeightB)
        {

            //set A transformation relative to B.  B remains at (0,0)
            Matrix atoB = transformA * Matrix.Invert(transformB);

            //Generate normal vectors to each rectangle's side
            Vector2 columnStep = Vector2.TransformNormal(Vector2.UnitX, atoB);
            Vector2 rowStep = Vector2.TransformNormal(Vector2.UnitY, atoB);

            //Calculate the top left corner of A
            Vector2 rowStartPosition = Vector2.Transform(Vector2.Zero, atoB);

            // Search each row of pixels in A.  Start at the top and move down
            for (int rowA = 0; rowA < pixelHeightA; rowA++)
            {
                //begin at the left
                Vector2 pixelPositionA = rowStartPosition;
                // for each column in the row (move left to right)
                for (int colA = 0; colA < pixelWidthA; colA++)
                {
                    // get the pixel position
                    var x = (int)Math.Round(pixelPositionA.X);
                    var y = (int)Math.Round(pixelPositionA.Y);

                    //if the pixel is within the bounds of B
                    if (x >= 0 && x < pixelWidthB && y >= 0 && y < pixelHeightB)
                    {
                        //Get colors of overlapping pixels
                        Color colorA = a.GetPixelColor(colA + rowA * pixelWidthA);
                        Color colorB = b.GetPixelColor(x + y * pixelWidthB);

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
            var leftTop = new Vector2(0.0f, 0.0f);
            var rightTop = new Vector2(width, 0.0f);
            var leftBottom = new Vector2(0.0f, height);
            var rightBottom = new Vector2(width, height);

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
