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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        private collisionChecker Checker;
        bgObject b1, b2;
        playerObject p;
        public Vector2 bgSpeed= new Vector2(0, 600);
        int x = 0; 
        private List<bulletObject> bulletList = new List<bulletObject>();
        private List<enemyObject> enemyList = new List<enemyObject>();
        private List<explosionObject> explosionList = new List<explosionObject>();
        KeyboardState prevState;
        int numBullets = 3;
        int numPotOGold=5;
        int numHorseShoe= 5;
        int numClover=5; 
        int numStars=5;
        int numMoons = 5;
        int numBalloons=5;
        int numRainbows = 5;
        int numHearts = 5;
        int score = 0;
        int health = 1000;
        bool gameOver=false;
        bool menu=true;
        int seconds = 0;
        int hiScore;
        int extemp;
        Texture2D fail, menuTex;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            // initialize and fill list
            spriteBatch = new SpriteBatch(GraphicsDevice);
            b1 = new bgObject(new Vector2(0, -1500), "scrollingbg.jpg");
            b2 = new bgObject(new Vector2(0, -3550), "scrollingbg1.jpg");
            p = new playerObject(new Vector2(300, 400), bgSpeed, "player.png");
            fail = Texture2D.FromFile(Game1.graphics.GraphicsDevice, "fail.jpg");
            menuTex = Texture2D.FromFile(Game1.graphics.GraphicsDevice, "menu.png");
            for (float i = 0; i < numBullets; i++)
            {
                bulletObject bullet = new bulletObject("laser.png");
                bulletList.Add(bullet);
            }
            for (float i = 0; i < numBalloons; i++)
            {
                enemyObject balloon = new enemyObject(new Vector2(150*i+200, 700), new Vector2(0,-1), 1, 100, "balloon.png");
                enemyList.Add(balloon);
            }
            for (float i = 0; i < numClover; i++)
            {
                enemyObject clover = new enemyObject(new Vector2(i * 150 + 10, -100), new Vector2(1,1), 1, 150, "clover.png");
                enemyList.Add(clover);

            }
            for (float i = 0; i < numHearts; i++)
            {
                enemyObject heart = new enemyObject(new Vector2(-100, 150 * i + 50), new Vector2(1, -.01f), 1, 200, "heart.png"); 
                enemyList.Add(heart);
            }
            for (float i = 0; i < numHorseShoe; i++)
            {
                enemyObject horseShoe = new enemyObject(new Vector2(800, 150 * i + 50), new Vector2(-1, -.01f), 1, 250,"horseshoe.png");
                enemyList.Add(horseShoe);

            }
            for (float i = 0; i < numMoons; i++)
            {
                enemyObject moon = new enemyObject(new Vector2(700 + i * 150, 700 + i * 150), new Vector2(-1, -1), 1, 300, "moon.png");
                enemyList.Add(moon);

            }
            for (float i = 0; i < numPotOGold; i++)
            {
                enemyObject potOGold = new enemyObject(new Vector2(i * 100, 600 + i * 150), new Vector2(1, -1), 1, 350, "potOGold.png"); ;
                enemyList.Add(potOGold);
    
            }
            for (float i = 0; i < numRainbows; i++)
            {
                enemyObject rainbow = new enemyObject(new Vector2(500 + i * 150, -100), new Vector2(-1, 1), 1, 400, "rainbow.png");
                enemyList.Add(rainbow);
     
            }
            for (float i = 0; i < numStars; i++)
            {
                enemyObject star = new enemyObject(new Vector2(-i * 40, 400 + i * 150), new Vector2(1, -1), 1, 450, "star.png");
                enemyList.Add(star);
       
            }

            for (float i = 0; i < enemyList.Count; i++)
            {
                explosionObject boom = new explosionObject(new Vector2(0, 0), "boom.png");
                explosionList.Add(boom);
            }
            Checker = new collisionChecker();

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Escape))
                this.Exit();
            if (keyboard.IsKeyDown(Keys.Enter))
            {
                if (menu)
                {
                    menu = false;
                }
                if (gameOver)
                {
                    gameOver = false;
                    score = 0;
                    health = 1000;
                    p.position = new Vector2(300, 400);
                }
            }
            if (menu == false&&gameOver == false)
            {
                Window.Title = "Lucky//////////HEALTH: " + health + "\\\\\\\\\\\\\\\\\\Score: " + score;

     
                //Moving Background and explosions
 
                if (b1.position.Y > 800)
                {
                    b1.position.Y = b2.position.Y - 2050;
                }
                if (b2.position.Y > 800)
                {
                    b2.position.Y = b1.position.Y - 2050;
                }
                Vector2 direction = new Vector2(0, 1);
                bgSpeed = p.bgSpeed;
                b1.position += direction * bgSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                b2.position += direction * bgSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                foreach (explosionObject g in explosionList)
                {
                    if (g.active)
                    {
                        g.position += direction * bgSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (g.position.Y > 800)
                            g.active = false;
                    }
                }

                //check if bullet and enemy collide
                foreach (graphicsObject g in bulletList)
                {
                    foreach (graphicsObject h in enemyList)
                    {
                        if (g.isVisible&&h.active)
                        {
                            if (Checker.CheckCollisions(g, h))
                            {
                                h.active = false;
                                g.isVisible = false;
                                g.active = false;
                                score += h.value;
                                explosionList.ElementAt(extemp).active=true;
                                explosionList.ElementAt(extemp).position = h.position;
                                extemp++;
                                extemp %= explosionList.Count;
                            }
                        }
                    }
                }


                //Lose health when player collides with enemy and resets enemy
                foreach (graphicsObject g in enemyList)
                {
                    if (Checker.CheckCollisions(g, p))
                    {
                       if (g.active)
                        {
                            health -= g.value / 5;
                            g.active = false;
                        }
                    }
                }


                //Health Drops under 0 then Game Over
                if (health <= 0)
                {
                    gameOver = true;
                    Window.Title = "GAME OVER  Score" + score + "     HIScore: " + hiScore;
                    
                }
                //spawn the enemys one at a time
                foreach (graphicsObject g in enemyList)
                {
                    if (!g.active)
                    {
                        seconds = (int)gameTime.TotalRealTime.Seconds;
                        seconds %= enemyList.Count;
                        
                        enemyList.ElementAt(seconds).spawn();
                    }
                }
                //fire bullets 
                if (keyboard.IsKeyDown(Keys.Space) == true && prevState.IsKeyDown(Keys.Space) == false)
                {
                    if (!bulletList.ElementAt(x).active)
                    {
                        bulletList.ElementAt(x).fire(p.position + p.center);
                        x++;
                        x %= numBullets;
                    }
                }
                prevState = keyboard;
                p.update(gameTime);
                foreach (bulletObject g in bulletList) g.update(gameTime);
                foreach (enemyObject g in enemyList) g.update(gameTime);

            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend); 
            if (menu)
            {
                spriteBatch.Draw(menuTex, new Vector2(0, 0), null, Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
                spriteBatch.End();
            }
            else if (gameOver)
            {
                foreach (enemyObject g in enemyList)
                {
                    g.active = false;
                }
                if (score >= hiScore)
                    hiScore = score;
                spriteBatch.Draw(fail, new Vector2(0, 0), null, Color.White, 0.0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0);
                spriteBatch.End();
            }

            else
            {
                spriteBatch.End();
                b1.draw(spriteBatch);
                b2.draw(spriteBatch);
                p.draw(spriteBatch);
                foreach (bulletObject g in bulletList) g.draw(spriteBatch);

                foreach (enemyObject g in enemyList) g.draw(spriteBatch);
                foreach (explosionObject g in explosionList) g.draw(spriteBatch);
            }
            
            base.Draw(gameTime);
        }
    }
}
