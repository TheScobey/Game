using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FirstGame
{
    class ProjectileEngine
    {
        public List<Projectile> Projectiles = new List<Projectile>();
        public int projectileTTL = 200;

        public ProjectileEngine()
        {
        
        }

        public void SpawnProjectile(Vector2 position, Vector2 velocity, Texture2D texture)
        {
            Projectiles.Add(GenerateNewProjectile(position, velocity, texture, projectileTTL));
        }

        private Projectile GenerateNewProjectile(Vector2 position, Vector2 velocity, Texture2D texture, int TTL)
        {
            return new Projectile(position, velocity, texture, TTL);
        }


        public void Update()
        {
            for(int i = 0; i < Projectiles.Count; i++)
            {
                Projectiles[i].Update();
                if (Projectiles[i].TTL <= 0)
                {
                    Projectiles.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Projectiles.Count; i++)
            {
                Projectiles[i].Draw(spriteBatch);
            }
        }
    }
}
