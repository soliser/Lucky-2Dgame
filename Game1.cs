using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        public static GraphicsDeviceManager Graphics;
        public static SpriteBatch SpriteBatch;
        private CollisionChecker _checker;
        BgObject _b1;
        BgObject _b2;
        PlayerObject _p;
        public Vector2 BgSpeed= new Vector2(0, 600);
        int _x; 
        private readonly List<BulletObject> _bulletList = new List<BulletObject>();
        private readonly List<EnemyObject> _enemyList = new List<EnemyObject>();
        private readonly List<ExplosionObject> _explosionList = new List<ExplosionObject>();
        KeyboardState _prevState;
        private const int NumBullets = 3;
        private const int NumPotOGold = 5;
        private const int NumHorseShoe = 5;
        private const int NumClover = 5;
        private const int NumStars = 5;
        private const int NumMoons = 5;
        private const int NumBalloons = 5;
        private const int NumRainbows = 5;
        private const int NumHearts = 5;
        int _score;
        int _health = 1000;
        bool _gameOver;
        bool _menu=true;
        int _seconds;
        int _hiScore;
        int _extemp;
        Texture2D _fail;
        Texture2D _menuTex;

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this) {PreferredBackBufferHeight = 600, PreferredBackBufferWidth = 800};
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>

        protected override void LoadContent()
        {

            // Create a new SpriteBatch, which can be used to draw textures.
            // initialize and fill list
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            _b1 = new BgObject(new Vector2(0, -1500), "scrollingbg.jpg");
            _b2 = new BgObject(new Vector2(0, -3550), "scrollingbg1.jpg");
            _p = new PlayerObject(new Vector2(300, 400), BgSpeed, "player.png");
            using (var fileStream = new FileStream("fail.jpg", FileMode.Open))
            {
                _fail = Texture2D.FromStream(Graphics.GraphicsDevice, fileStream);
            }
            using (var fileStream = new FileStream("menu.png", FileMode.Open))
            {
                _menuTex = Texture2D.FromStream(Graphics.GraphicsDevice, fileStream);
            }
            for (float i = 0; i < NumBullets; i++)
            {
                var bullet = new BulletObject("laser.png");
                _bulletList.Add(bullet);
            }
            for (float i = 0; i < NumBalloons; i++)
            {
                var balloon = new EnemyObject(new Vector2(150*i+200, 700), new Vector2(0,-1), 1, 100, "balloon.png");
                _enemyList.Add(balloon);
            }
            for (float i = 0; i < NumClover; i++)
            {
                var clover = new EnemyObject(new Vector2(i * 150 + 10, -100), new Vector2(1,1), 1, 150, "clover.png");
                _enemyList.Add(clover);

            }
            for (float i = 0; i < NumHearts; i++)
            {
                var heart = new EnemyObject(new Vector2(-100, 150 * i + 50), new Vector2(1, -.01f), 1, 200, "heart.png"); 
                _enemyList.Add(heart);
            }
            for (float i = 0; i < NumHorseShoe; i++)
            {
                var horseShoe = new EnemyObject(new Vector2(800, 150 * i + 50), new Vector2(-1, -.01f), 1, 250,"horseshoe.png");
                _enemyList.Add(horseShoe);

            }
            for (float i = 0; i < NumMoons; i++)
            {
                var moon = new EnemyObject(new Vector2(700 + i * 150, 700 + i * 150), new Vector2(-1, -1), 1, 300, "moon.png");
                _enemyList.Add(moon);

            }
            for (float i = 0; i < NumPotOGold; i++)
            {
                var potOGold = new EnemyObject(new Vector2(i * 100, 600 + i * 150), new Vector2(1, -1), 1, 350, "potOGold.png");
                _enemyList.Add(potOGold);
    
            }
            for (float i = 0; i < NumRainbows; i++)
            {
                var rainbow = new EnemyObject(new Vector2(500 + i * 150, -100), new Vector2(-1, 1), 1, 400, "rainbow.png");
                _enemyList.Add(rainbow);
     
            }
            for (float i = 0; i < NumStars; i++)
            {
                var star = new EnemyObject(new Vector2(-i * 40, 400 + i * 150), new Vector2(1, -1), 1, 450, "star.png");
                _enemyList.Add(star);
       
            }

            for (float i = 0; i < _enemyList.Count; i++)
            {
                var boom = new ExplosionObject(new Vector2(0, 0), "boom.png");
                _explosionList.Add(boom);
            }
            _checker = new CollisionChecker();

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
                Exit();
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Escape))
                Exit();
            if (keyboard.IsKeyDown(Keys.Enter))
            {
                if (_menu)
                {
                    _menu = false;
                }
                if (_gameOver)
                {
                    _gameOver = false;
                    _score = 0;
                    _health = 1000;
                    _p.Position = new Vector2(300, 400);
                }
            }
            if (_menu == false&&_gameOver == false)
            {
                Window.Title = "Lucky//////////HEALTH: " + _health + "\\\\\\\\\\\\\\\\\\Score: " + _score;

     
                //Moving Background and explosions
 
                if (_b1.Position.Y > 800)
                {
                    _b1.Position.Y = _b2.Position.Y - 2050;
                }
                if (_b2.Position.Y > 800)
                {
                    _b2.Position.Y = _b1.Position.Y - 2050;
                }
                var direction = new Vector2(0, 1);
                BgSpeed = _p.BgSpeed;
                _b1.Position += direction * BgSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                _b2.Position += direction * BgSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                foreach (ExplosionObject g in _explosionList)
                {
                    if (g.Active)
                    {
                        g.Position += direction * BgSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (g.Position.Y > 800)
                            g.Active = false;
                    }
                }

                //check if bullet and enemy collide
                foreach (var g in _bulletList)
                {
                    foreach (var h in _enemyList)
                    {
                        if (g.IsVisible&&h.Active)
                        {
                            if (_checker.CheckCollisions(g, h))
                            {
                                h.Active = false;
                                g.IsVisible = false;
                                g.Active = false;
                                _score += h.Value;
                                _explosionList.ElementAt(_extemp).Active=true;
                                _explosionList.ElementAt(_extemp).Position = h.Position;
                                _extemp++;
                                _extemp %= _explosionList.Count;
                            }
                        }
                    }
                }


                //Lose health when player collides with enemy and resets enemy
                foreach (var g in _enemyList)
                {
                    if (_checker.CheckCollisions(g, _p))
                    {
                       if (g.Active)
                        {
                            _health -= g.Value / 5;
                            g.Active = false;
                        }
                    }
                }


                //Health Drops under 0 then Game Over
                if (_health <= 0)
                {
                    _gameOver = true;
                    Window.Title = "GAME OVER  Score" + _score + "     HIScore: " + _hiScore;
                    
                }
                //spawn the enemys one at a time
                foreach (var g in _enemyList)
                {
                    if (!g.Active)
                    {
                        _seconds = gameTime.TotalGameTime.Seconds;
                        _seconds %= _enemyList.Count;
                        
                        _enemyList.ElementAt(_seconds).Spawn();
                    }
                }
                //fire bullets 
                if (keyboard.IsKeyDown(Keys.Space) && _prevState.IsKeyDown(Keys.Space) == false)
                {
                    if (!_bulletList.ElementAt(_x).Active)
                    {
                        _bulletList.ElementAt(_x).Fire(_p.Position + _p.Center);
                        _x++;
                        _x %= NumBullets;
                    }
                }
                _prevState = keyboard;
                _p.update(gameTime);
                foreach (BulletObject g in _bulletList) g.update(gameTime);
                foreach (EnemyObject g in _enemyList) g.update(gameTime);

            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend); 
            if (_menu)
            {
                SpriteBatch.Draw(_menuTex, new Vector2(0, 0), null, Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
                SpriteBatch.End();
            }
            else if (_gameOver)
            {
                foreach (EnemyObject g in _enemyList)
                {
                    g.Active = false;
                }
                if (_score >= _hiScore)
                    _hiScore = _score;
                SpriteBatch.Draw(_fail, new Vector2(0, 0), null, Color.White, 0.0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0);
                SpriteBatch.End();
            }

            else
            {
                SpriteBatch.End();
                _b1.draw(SpriteBatch);
                _b2.draw(SpriteBatch);
                _p.draw(SpriteBatch);
                foreach (var g in _bulletList) g.draw(SpriteBatch);

                foreach (var g in _enemyList) g.draw(SpriteBatch);
                foreach (var g in _explosionList) g.draw(SpriteBatch);
            }
            
            base.Draw(gameTime);
        }
    }
}
