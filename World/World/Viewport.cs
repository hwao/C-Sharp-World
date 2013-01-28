using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace World
{
    class Viewport
    {
        public Vector2 FieldSize;
        public WorldMap WorldMap;
        public Vector2 Camera;
        public Rectangle Screen;
        protected float myZoom = 1;

        public float Zoom
        {
            get
            {
                return this.myZoom;
            }
            set
            {
                if (value > 0.2f && value <= 10 )
                {
                    myZoom = value;
                }
            }
        }

        public Viewport(WorldMap WorldMap, Rectangle Screen, Vector2 FieldSize, Vector2 Camera)
        {
            this.WorldMap = WorldMap;
            this.Camera = Camera;
            this.FieldSize = FieldSize;
            this.Screen = Screen; // new Rectangle((int)ScreenPos.X, (int)ScreenPos.Y, Camera.Width, Camera.Height);
            this.Zoom = 1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //this.DrawAllMap(spriteBatch);

            this.Draw3(spriteBatch);
        }

        public void DrawAllMap(SpriteBatch spriteBatch)
        {
            Vector2 vCamera = new Vector2(this.Camera.X, this.Camera.Y);

            for (int x = 0; x < this.WorldMap.height; x++)
            {
                for (int y = 0; y < this.WorldMap.width; y++)
                {
                    this.WorldMap.fieldList[x, y].Draw(spriteBatch, vCamera, this.Screen, 0.4f, this.Zoom);
                }
            }
        }

        public void Draw3(SpriteBatch spriteBatch)
        {
            Vector2 vCamera = new Vector2(this.Camera.X, this.Camera.Y);
            Vector2 crop = new Vector2(vCamera.X, vCamera.Y);

            Vector2 FieldSizeZoom = new Vector2(this.FieldSize.X / Zoom, this.FieldSize.Y / Zoom);


            int iSize = this.WorldMap.height + this.WorldMap.width - 1; //liczba y-kubelkow
            int yStart = Math.Max(Convert.ToInt32(2 * crop.Y / FieldSizeZoom.Y) - 2, 0);
            int yEnd = Math.Min(1 + Convert.ToInt32(2 * (crop.Y + this.Screen.Height) / FieldSizeZoom.Y), iSize);

            int addRight = Convert.ToInt32(this.Screen.Width / FieldSizeZoom.X) + 1;

            for (; yStart < yEnd; yStart++)
            {
                int t = Convert.ToInt32((this.WorldMap.yKubelek[yStart][0].mPosition.X - (crop.X * Zoom)) / this.FieldSize.X) + 1; // +1 Zapas na lewo od granicy

                Console.WriteLine("{0}", t);

                int xStart = t - addRight;
                if (xStart <= 0) xStart = 0;

                if (t > this.WorldMap.yKubelek[yStart].Length - 1)
                    t = this.WorldMap.yKubelek[yStart].Length - 1;
                if (t <= 0)
                    t = 0;

                int xEnd = t;

                for (; xStart <= xEnd; xStart++)
                {
                    this.WorldMap.yKubelek[yStart][xStart].Draw(spriteBatch, vCamera, this.Screen, 1.0f, this.Zoom);
                }
            }
        }
    }
}
