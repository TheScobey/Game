using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FirstGame
{
    class Projectile
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Angle { get; set; }
        public Color Color { get; set; }
        public float Size { get; set; }
        public int TTL { get; set; }

        public Projectile(Vector2 position, Vector2 velocity, Texture2D texture, int TTL)
        {
            this.Position = position;
            this.Velocity = velocity;
            this.Texture = texture;
            this.TTL = TTL;
            Color = Color.White;
        }

        public void Update()
        {
            TTL--;
            Position += Velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Console.WriteLine("Drawing proj");
            spriteBatch.Draw(Texture, Position, Color);
        }
    }
}
