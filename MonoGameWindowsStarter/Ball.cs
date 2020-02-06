using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameWindowsStarter
{
    class Ball
    {
        Game1 game;
        Texture2D texture;
        public BoundingCircle Bounds;
        // public Vector2 Velocity;

        public double VelocityX;
        public double VelocityY;

        public Ball(Game1 game)
        {
            this.game = game;
        }

        public void Initialize()
        {
            Bounds.Radius = 25;
            Bounds.X = game.GraphicsDevice.Viewport.Width / 2;
            Bounds.Y = game.GraphicsDevice.Viewport.Height / 2;

            /* Velocity = new Vector2(
                (float)game.Random.NextDouble(),
                (float)game.Random.NextDouble()
            );
            Velocity.Normalize();
            */

            VelocityX = 2 * game.Random.NextDouble();
            if (VelocityX >= 1)
                VelocityX /= -2;

            VelocityY = 2 * game.Random.NextDouble();
            if (VelocityY >= 1)
                VelocityY /= -2;
        }


        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("ball");
        }

        public void Update(GameTime gameTime)
        {
            var viewport = game.GraphicsDevice.Viewport;

            Bounds.Y += 0.5f * (float)gameTime.ElapsedGameTime.TotalMilliseconds * (float)VelocityY;
            Bounds.X += 0.5f * (float)gameTime.ElapsedGameTime.TotalMilliseconds * (float)VelocityX;

            if (Bounds.Y < Bounds.Radius)
            {
                VelocityY *= -1;
                float delta = Bounds.Radius - Bounds.Y;
                Bounds.Y += 2 * delta;
            }

            if (Bounds.Y > viewport.Height - Bounds.Radius)
            {
                VelocityY *= -1;
                float delta = viewport.Height - Bounds.Radius - Bounds.Y;
                Bounds.Y += 2 * delta;
            }

            if (Bounds.X < 0 || Bounds.X > viewport.Width - Bounds.Radius)
            {
                VelocityY = 0;
                VelocityX = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Bounds, Color.White);
        }
    }
}
