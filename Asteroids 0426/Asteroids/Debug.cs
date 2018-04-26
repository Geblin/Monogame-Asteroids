using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Asteroids
{
    class Debug
    {
        private SpriteFont font;
        List<Asteroid> asteroidList;
        Player player;

        public Debug(List<Asteroid> asteroidList, Player player)
        {
            this.asteroidList = asteroidList;
            this.player = player;
        }

        public void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Debug");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Prints each asteroid position and scale.
            int y = 20;
            foreach (Asteroid asteroid in asteroidList)
            {
                spriteBatch.DrawString(font, "Pos: " + asteroid.pos + ", Scale: (" + asteroid.scale + ")", new Vector2(20, y), Color.Red);
                y += 20;
            }

            //Prints player speed and angle
            spriteBatch.DrawString(font, "Speed: " + player.acceleration, new Vector2(1800, 20), Color.Blue);
            spriteBatch.DrawString(font, "Angle: " + player.angle, new Vector2(1800, 40), Color.Blue);
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
