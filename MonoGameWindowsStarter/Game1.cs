using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Ball ball;
        Paddle1 paddle1;
        Paddle2 paddle2;
        Texture2D p1win;
        Texture2D p2win;
        double newYVel;
        double newXVel;

        public Random Random = new Random();

        KeyboardState oldKeyboardState;
        KeyboardState newKeyboardState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            paddle1 = new Paddle1(this);
            paddle2 = new Paddle2(this);
            ball = new Ball(this);
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
            graphics.PreferredBackBufferWidth = 1042;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();

            ball.Initialize();
            paddle1.Initialize();
            paddle2.Initialize();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ball.LoadContent(Content);
            paddle1.LoadContent(Content);
            paddle2.LoadContent(Content);

            p1win = Content.Load<Texture2D>("p1win");
            p2win = Content.Load<Texture2D>("p2win");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            newKeyboardState = Keyboard.GetState();

            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (newKeyboardState.IsKeyDown(Keys.Escape))
                Exit();

            if (newKeyboardState.IsKeyDown(Keys.R))
                this.Reset();

            paddle1.Update(gameTime);
            paddle2.Update(gameTime);
            ball.Update(gameTime);

            if (paddle1.Bounds.CollidesWith(ball.Bounds))
            {
                double r = this.Random.NextDouble();

                ball.VelocityX *= -1;
                newXVel = ball.VelocityX + 0.5 * r;
                if (newXVel <= 12 && newXVel >= -12)
                    ball.VelocityX = newXVel;
                newYVel = ball.VelocityY + 0.5 * r;
                if (newYVel <= 12 && newYVel >= -12)
                    ball.VelocityY = newYVel;
                var delta = (paddle1.Bounds.X + paddle1.Bounds.Width) - (ball.Bounds.X - ball.Bounds.Radius);
                ball.Bounds.X += 2 * delta;
            }

            if (paddle2.Bounds.CollidesWith(ball.Bounds))
            {
                double r = this.Random.NextDouble();

                ball.VelocityX *= -1;
                newXVel = ball.VelocityX - 0.5 * r;
                if (newXVel <= 12 && newXVel >= -12)
                    ball.VelocityX = newXVel;
                newYVel = ball.VelocityY + 0.5 * r;
                if (newYVel <= 12 && newYVel >= -12)
                    ball.VelocityY = newYVel;
                var delta = (ball.Bounds.X + ball.Bounds.Radius) - (paddle2.Bounds.X);
                ball.Bounds.X -= 2 * delta;
            }

            oldKeyboardState = newKeyboardState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            ball.Draw(spriteBatch);
            paddle1.Draw(spriteBatch);
            paddle2.Draw(spriteBatch);

            if (ball.VelocityX == 0 && ball.VelocityY == 0)
            {
                DisplayWinner(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DisplayWinner(SpriteBatch spriteBatch)
        {
            if (ball.Bounds.X < 0)
                spriteBatch.Draw(p2win,
                    new Rectangle(350, 0, 350, 350),
                    Color.CornflowerBlue);

            else if (ball.Bounds.X > this.GraphicsDevice.Viewport.Width - ball.Bounds.Radius)
                spriteBatch.Draw(p1win,
                    new Rectangle(350, 0, 350, 350),
                    Color.CornflowerBlue);

            return;
        }

        private void Reset()
        {
            ball.Initialize();
            paddle1.Initialize();
            paddle2.Initialize();
        }
    }
}
