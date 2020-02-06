using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MonoGameWindowsStarter
{
    public class Paddle1
    {
        Game1 game;
        public BoundingRectangle Bounds;
        Texture2D texture;

        public Paddle1(Game1 game)
        {
            this.game = game;
        }

        public void Initialize()
        {
            Bounds.Width = 25;
            Bounds.Height = 200;
            Bounds.X = 100;
            Bounds.Y = game.GraphicsDevice.Viewport.Height / 2 - Bounds.Height / 2;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("pixel");
        }

        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W))
            {
                Bounds.Y -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                Bounds.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if (Bounds.Y < 0)
            {
                Bounds.Y = 0;
            }
            if (Bounds.Y > game.GraphicsDevice.Viewport.Height - Bounds.Height)
            {
                Bounds.Y = game.GraphicsDevice.Viewport.Height - Bounds.Height;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Bounds, Color.Red);
        }
    }
}
