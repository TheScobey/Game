using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame
{
    public class NPC
    {
        private AnimatedSprite animatedSprite;
        private Vector2 position;

        public NPC(Texture2D texture, Vector2 position)
        {
            this.position = position;
            animatedSprite = new AnimatedSprite(texture, 4, 4);
        }

        public void Update(Vector2 position)
        {
            this.position = position;
            animatedSprite.Update();
        }

        public void Update()
        {
            animatedSprite.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animatedSprite.Draw(spriteBatch, position, 0f);
        }
    }
}
