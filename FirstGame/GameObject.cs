using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame
{
    public class GameObject
    {
        public Texture2D texture;
        public Color color;
        public Vector2 Position;
        public Vector2 Velocity;

        private Sprite animatedSprite;

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    texture.Width,
                    texture.Height);
            }
        }

        public GameObject(Texture2D texture, int rows, int columns, Color color, Vector2 position)
        {
            this.texture = texture;
            this.Position = position;
            this.color = color;
            animatedSprite = new Sprite(texture,rows,columns);
        }

        public GameObject(Texture2D texture, Vector2 position, Vector2 velocity)
        {
            this.texture = texture;
            this.Position = position;
            this.Velocity = velocity;
        }

        public void Update(GameTime gameTime)
        {
            animatedSprite.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animatedSprite.Draw(spriteBatch, color, Position, 0f);
        }

    }
}
