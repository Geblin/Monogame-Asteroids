using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Asteroids
{
    public class AsteroidSpawner
    {
        Texture2D texture;
        List<Asteroid> asteroidList;
        Random rand;

        public AsteroidSpawner(List<Asteroid> asteroidList)
        {
            rand = new Random();
            this.asteroidList = asteroidList;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Asteroid");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Asteroid asteroid in asteroidList)
                asteroid.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            UpdateAsteroids(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.L))
                SpawnAsteroid(SetRandomSpawn(), 1f, 1);
        }

        public void SpawnAsteroid(Vector2 pos, float scale, int amount)
        {
            for (int i = 1; i <= amount; i++)
            {
                Asteroid newAsteroid = new Asteroid(texture, scale, pos);
                asteroidList.Add(newAsteroid);
            }
        }

        public void UpdateAsteroids(GameTime gameTime)
        {
            foreach (Asteroid asteroid in asteroidList)
            {
                asteroid.Update(gameTime);
            }

            for (int i = 0; i < asteroidList.Count; i++)
            {
                if (!asteroidList[i].isVisible)
                {
                    asteroidList.RemoveAt(i);
                    i--;
                }
            }
        }

        //Sets a random spawn outside of screen bounds
        public Vector2 SetRandomSpawn()
        {
            int side = rand.Next(4);
            
            //Each number represents a side

            switch (side)
            {
                // Left
                case 0:
                    return new Vector2(2120, rand.Next(0, 1080));

                // Top
                case 1:
                    return new Vector2(rand.Next(0, 1920), 1280);

                // Right
                case 2:
                    return new Vector2(-200, rand.Next(0, 1080));

                //Bottom
                case 3:
                    return new Vector2(rand.Next(0, 1920), -200);

                default:
                    throw new ArgumentException("Incorrect CrystalTypeEnum");
            }
        }
    }
}
