using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FirstGame
{
    public class Player : GameObject
    {
        private ProjectileEngine projectileEngine = new ProjectileEngine();
        private Texture2D projectileTexture;

        private Vector2 projectileAttachment;

        // unadjusted velocity for thumb sticks
        private float Vx = 0;
        private float Vy = 0;

        // the actual velocity
        private float VelocityX = 0;
        private float VelocityY = 0;

        private double timer;

        public float MaxSpeed = 300f;
        public float Acceleration = 6f;
        public float TurnSpeed = 12f; // higher is slower
        public float Friction = 0f;

        public Player(Texture2D texture, int rows, int columns, Color color, Vector2 position)
            : base(texture, rows, columns, color, position)
        {
            this.texture = texture;
            this.Position = position;
            this.color = color;
        }

        public float CalculateFriction(float velocity, float friction)
        {
            if (velocity > 0)
            {
                if (velocity <= friction)
                    return velocity = 0;
                
                return velocity -= friction;
            }
            else if (velocity < 0)
            {
                if (velocity >= friction * -1)
                    return velocity = 0;

                return velocity += friction;
            }

            return velocity;
        }

        public float CalculateVelocity(float velocity, float direction, float maxSpeed, float acceleration, float friction)
        {
            if (direction > 0)
            {
                if (velocity < 0)
                {
                    //velocity = CalculateFriction(velocity, friction);
                }

                if (velocity < maxSpeed)
                {
                    velocity += acceleration;
                }
            }
            else if (direction < 0)
            {
                if (velocity > 0)
                {
                    //velocity = CalculateFriction(velocity, friction);
                }

                if (velocity > maxSpeed * -1)
                {
                    velocity += acceleration * -1;
                }
            }
            else
            {
                //velocity = CalculateFriction(velocity, friction);
            }

            return velocity;
        }


        public new void Update(GameTime gameTime)
        {
            // horizontal
            float direction = 0f;

            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X != 0)
            {
                direction = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X;
            }

            Vx = CalculateVelocity(Vx, direction, MaxSpeed, Acceleration, Friction);

            if(direction > 0.55f)
            {
                direction = 1f;
            }
            else if (direction < -0.55f)
            {
                direction = -1f;
            }

            VelocityX = Vx *  Math.Abs(direction);

            if (VelocityX == 0)
            {
                Vx = 0;
            }

            // vertical
            direction = 0f;

            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y != 0)
            {
                direction = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y * -1;
            }

            Vy = CalculateVelocity(Vy, direction, MaxSpeed, Acceleration, Friction);

            if (direction > 0.55f)
            {
                direction = 1f;
            }
            else if (direction < -0.55f)
            {
                direction = -1f;
            }

            VelocityY = Vy * Math.Abs(direction);

            if(VelocityY == 0)
            {
                Vy = 0;
            }

            Position.X += VelocityX * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position.Y += VelocityY * (float)gameTime.ElapsedGameTime.TotalSeconds;

            projectileAttachment = new Vector2(Position.X - 7, Position.Y - 10);

            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);


            // shooting
            if (gameTime.TotalGameTime.TotalMilliseconds - timer > 500)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed)
                {
                    Shoot(Position, new Vector2(5, 0));
                    timer = gameTime.TotalGameTime.TotalMilliseconds;
                }
                else if (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
                {
                    Shoot(Position, new Vector2(-5, 0));
                    timer = gameTime.TotalGameTime.TotalMilliseconds;
                }
                else if( GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed)
                {
                    Shoot(Position, new Vector2(0, -5));
                    timer = gameTime.TotalGameTime.TotalMilliseconds;
                }
                else if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
                {
                    Shoot(Position, new Vector2(0, 5));
                    timer = gameTime.TotalGameTime.TotalMilliseconds;
                }
            }

            projectileEngine.Update();
            base.Update(gameTime);
        }

        public void Shoot(Vector2 position, Vector2 direction)
        {
            projectileEngine.SpawnProjectile(projectileAttachment, direction, projectileTexture);
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            base.Draw(spriteBatch);
            projectileEngine.Draw(spriteBatch);

            spriteBatch.DrawString(font, "Sticks: " + GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X + "," + GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y, new Vector2(10, 50), Color.Black);
            spriteBatch.DrawString(font, "Player speed: " + Math.Floor(VelocityX) + "," + Math.Floor(VelocityY), new Vector2(10, 10), Color.Black);
        }
    }
}
