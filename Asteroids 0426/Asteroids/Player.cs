using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Asteroids
{
    public class Player
    {
        Texture2D texture, laserTexture;
        Vector2 pos, origin, direction;
        Rectangle sourceRectangle;
        BoundingCircle boundingCircle;
        public float angle, acceleration, rateOfFire, rateOfFireCounter, turnSpeed, maxSpeed, speedUpRate, slowDownRate, range;
        bool isDriving;
        List<Laser> laserList;

        public Player(List<Laser> laserList)
        {
            pos = new Vector2(200, 200);           
            angle = 0f;
            rateOfFire = 20;        //Default = 20 
            turnSpeed = 0.08f;      //Default = 0.08
            maxSpeed = 5;           //Default = 6
            speedUpRate = 0.15f;    //Default = 0.15
            slowDownRate = 0.05f;    //Default = 0.1
            range = 1000;
            rateOfFireCounter = rateOfFire;
            this.laserList = laserList;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("playerShip");
            laserTexture = content.Load<Texture2D>("laser");

            //Setting source and origin. Can't do in constructor because texture isn't loaded yet.
            sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
            boundingCircle = new BoundingCircle(pos, texture, 1f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {          
            foreach (Laser laser in laserList)
                laser.Draw(spriteBatch);

            spriteBatch.Draw(texture, pos, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            boundingCircle.x = pos.X;
            boundingCircle.y = pos.Y;

            //Updates the lasers
            UpdateLasers(gameTime);

            //Makes the ship fly through the border into the other side
            if (pos.X > 2020)
                pos.X = -100;
            else if (pos.X < -100)
                pos.X = 2020;
            if (pos.Y > 1180)
                pos.Y = -100;
            else if (pos.Y < -100)
                pos.Y = 1180;

            //Rotate ship when pressing A and D
            if (keyState.IsKeyDown(Keys.A))
                angle -= turnSpeed;
            if (keyState.IsKeyDown(Keys.D))
                angle += turnSpeed;

            //Accelerate if W is pressed
            if (keyState.IsKeyDown(Keys.W))
            {
                if (isDriving == false)
                {
                    isDriving = true;
                    acceleration = 1;
                }
                direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                direction.Normalize();
                pos += direction * acceleration;
                //Accelerates the ship
                acceleration += speedUpRate;
                //Sets max speed
                if (acceleration > maxSpeed)
                    acceleration = maxSpeed;
            }
            //else slows down the ship
            else
            {
                isDriving = false;
                acceleration -= slowDownRate;
            }
            
            if (acceleration < 0)
                acceleration = 0;

            pos += direction * acceleration;

            //Press space to shoot
            if (keyState.IsKeyDown(Keys.Space))
            {
                Shoot();
            }
            if (rateOfFireCounter < rateOfFire)
                rateOfFireCounter++;
        }
       
        public void Shoot()
        {
            if (rateOfFireCounter == rateOfFire)
            {
                Laser newLaser = new Laser(pos, angle, laserTexture);
                newLaser.isVisible = true;
                laserList.Add(newLaser);
            }

            if (rateOfFireCounter == rateOfFire)
                rateOfFireCounter = 0;
        }

        public void UpdateLasers(GameTime gameTime)
        {
            //Checks if a laser is outside of range and then makes it not visible
            foreach (Laser laser in laserList)
            {
                laser.Update(gameTime);
                if (laser.checkRange(range))
                {
                    laser.isVisible = false;
                }

            }

            //Remove every bullet that is not visible
            for (int i = 0; i < laserList.Count; i++)
            {
                if (!laserList[i].isVisible)
                {
                    laserList.RemoveAt(i);
                    i--;
                }
            }
            
        }
    }
}
