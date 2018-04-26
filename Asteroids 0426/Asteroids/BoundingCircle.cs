using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids
{
    public class BoundingCircle
    {
        public float radius;
        public float x;
        public float y;

        public BoundingCircle(Vector2 pos, Texture2D texture, float scale)
        {
            radius = (texture.Width / 2) * scale;
            x = pos.X + texture.Width / 2;
            y = pos.Y + texture.Height / 2;
        }

        public bool Intersects(BoundingCircle boundingCircle)
        {
            double distance = Math.Sqrt((Math.Pow(x - boundingCircle.x, 2) + (Math.Pow(y - boundingCircle.y, 2))));
            if (distance <= radius + boundingCircle.radius)
            {
                    return true;
            }
            else
                return false;
        }
    }
}
