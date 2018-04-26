using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Asteroids
{
    
    //TODO
    /* Fix the asteroid splitting. Currently splits into exactly the same direction and rotation.
     * Fix a wave system that spawns more asteroid each wave.
     * Setup collisions with the player and fix lives and gameOver state.
     * Setup particle engine. I want particles for asteroid explosion, gunfire, player death and for when the lasers hit maximum range and disappears.
     * Fix sounds. Sounds for shots fired, asteroids exploding, player death and a ding on new wave. 
     * Make random asteroid shapes.
     */

    public class Game1 : Game
    {
        Debug debug;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player;
        AsteroidSpawner asteroidSpawner;

        List<Laser> laserList;
        List<Asteroid> asteroidList;

        Random rand;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            Content.RootDirectory = "Content";
            rand = new Random();

            laserList = new List<Laser>();
            asteroidList = new List<Asteroid>();

            player = new Player(laserList);
            asteroidSpawner = new AsteroidSpawner(asteroidList);

            debug = new Debug(asteroidList, player);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            debug.LoadContent(Content);

            player.LoadContent(Content);
            asteroidSpawner.LoadContent(Content);

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);
            asteroidSpawner.Update(gameTime);

            CheckCollisions();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            player.Draw(spriteBatch);
            asteroidSpawner.Draw(spriteBatch);

            debug.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void CheckCollisions()
        {            
            for (int a = 0; a < laserList.Count; a++)
            {
                for (int b = 0; b < asteroidList.Count; b++)
                {
                    if (asteroidList[b].boundingCircle.Intersects(laserList[a].boundingCircle))
                    {
                        laserList[a].isVisible = false;
                        asteroidList[b].isVisible = false;
                        if (asteroidList[b].scale > 0.25f)
                        {
                            asteroidSpawner.SpawnAsteroid(asteroidList[b].pos, asteroidList[b].scale / 2, rand.Next(2,4));
                            b++;
                        }
                    }
                }
            }
        }
    }
}
