#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace FirstGame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Dictionary<string, Texture2D> textureTable = new Dictionary<string, Texture2D>();

        public bool gamePaused = false;
        private double timerPause;

        private TextBox textbox1 = new TextBox(100, 100, 400,48);
        private Texture2D background;
        private SpriteFont font;
        private int score = 0;
        ParticleEngine particleEngine;
        
        //private AnimatedSprite animatedSprite;
        private Player player;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;

            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 720;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

          // Import game content
            background = Content.Load<Texture2D>("Backgrounds/grassMowed");
            font = Content.Load<SpriteFont>("Score");

            Texture2D texture = Content.Load<Texture2D>("SmileyWalk");
            //animatedSprite = new AnimatedSprite(texture, 4, 4);

            List<Texture2D> textures = new List<Texture2D>();
            textures.Add(Content.Load<Texture2D>("blood1"));
            textures.Add(Content.Load<Texture2D>("blood2"));
            textures.Add(Content.Load<Texture2D>("blood3"));

            particleEngine = new ParticleEngine(textures, new Vector2(400, 240));

            textureTable["textboxLeft"] = Content.Load<Texture2D>("UI/textboxLeft");
            textureTable["textboxMid"] = Content.Load<Texture2D>("UI/textboxMid");
            textureTable["textboxRight"] = Content.Load<Texture2D>("UI/textboxRight");
            textureTable["textboxDefault"] = Content.Load<Texture2D>("UI/textboxDefault");
            textureTable["cursorBlink"] = Content.Load<Texture2D>("UI/cursorBlink");
            textureTable["smileyWalk"] = Content.Load<Texture2D>("SmileyWalk");
            textureTable["playerBlue0"] = Content.Load<Texture2D>("Player/playerBlue0");
            textureTable["projectile1"] = Content.Load<Texture2D>("projectile1");

            player = new Player(textureTable["smileyWalk"], textureTable["projectile1"], new Vector2(500, 500));
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            if (gameTime.TotalGameTime.TotalMilliseconds - timerPause > 500 && GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                gamePaused = !gamePaused;
                timerPause = gameTime.TotalGameTime.TotalMilliseconds;
            }

            if (gamePaused == false)
            {
                var mouseState = Mouse.GetState();
                var mousePosition = new Point(mouseState.X, mouseState.Y);

                if (textbox1.area.Contains(mousePosition))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        //focus = textbox1;
                        textbox1.focused = true;
                    }
                }
                textbox1.Update(gameTime);

                //animatedSprite.Update();
                player.Update(gameTime);

                particleEngine.EmitterLocation = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                particleEngine.Update(Mouse.GetState().LeftButton == ButtonState.Pressed);
            }
            else
            {
                // stuff for when paused
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    //spriteBatch.Draw(background, new Rectangle(x* 160, y * 160, 160, 160), Color.White);
                }
            }

            spriteBatch.DrawString(font, "Score", new Vector2(100, 100), Color.Black);

            textbox1.Draw(spriteBatch, font, textureTable, gameTime);

            player.Draw(spriteBatch, font);

            spriteBatch.End();

            //animatedSprite.Draw(spriteBatch, new Vector2(200, 200));

            particleEngine.Draw(spriteBatch);
            base.Draw(gameTime);
        }

    }
}
