using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace World.Map.Field
{
    class iField
    {
        public Game1 Game;
        /**
         * Pozycja "stala" na mapie
         */
        public Vector2 mPosition = new Vector2(0, 0);
        public Vector2 mPositionZoom = new Vector2(0, 0);
        /**
         * Pozycja podczas wyswietlania na ekranie ("widok z kamery")
         */
        public Vector2 position = new Vector2(0, 0);
        /**
         * Pozycja w tablicy
         */
        public int X = 0;
        public int Y = 0;

        bool passable = true;

        public Texture2D mSpriteTexture;

        public iField(Game1 Game)
        {
            this.Game = Game;
        }

        public const int N = 0;
        public const int NE = 1;
        public const int E = 2;
        public const int SE = 3;
        public const int S = 4;
        public const int SW = 5;
        public const int W = 6;
        public const int NW = 7;

        public iField[] adjacentFieldList;

        public void bindAdjacentField( WorldMap WorldMap )
        {
            this.adjacentFieldList = new iField[8];
            this.adjacentFieldList[N]  = WorldMap.getField(X-1,Y-1);
            this.adjacentFieldList[NE] = WorldMap.getField(X-1,Y);
            this.adjacentFieldList[E]  = WorldMap.getField(X-1,Y+1);
            this.adjacentFieldList[SE] = WorldMap.getField(X,Y+1);
            this.adjacentFieldList[S]  = WorldMap.getField(X+1,Y+1);
            this.adjacentFieldList[SW] = WorldMap.getField(X+1,Y);
            this.adjacentFieldList[W]  = WorldMap.getField(X+1,Y-1);
            this.adjacentFieldList[NW] = WorldMap.getField(X,Y-1);
        }

        public virtual void LoadContent(ContentManager Content)
        {
            this.mSpriteTexture = Content.Load<Texture2D>("grass");
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 vCamera, Rectangle Screen, float Zoom )
        {
            this.Draw(spriteBatch, vCamera, Screen, 1.0f, Zoom );
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 vCamera, Rectangle Screen, float alpha, float Zoom)
        {
            this.mPositionZoom.Y = this.X * (64 / (2 * Zoom)) - (this.X * 7 / Zoom);
            this.mPositionZoom.X = -this.X * (100 / (2 * Zoom)); // -(w);

            this.mPositionZoom.Y += this.Y * ((100 / (2 * Zoom)) / 2);
            this.mPositionZoom.X += this.Y * (100 / (2 * Zoom));

            // Punkt ma swoje wspolrzedne
            // a narysowanie ich na ekranie zalezy od pozycji 0.0 dla Ekranu (ma rysowac od lewego gornego rogu ekranu
            // w ktorym jest wyswietlany
            // oraz od pozycji kamery ktora mozna przesuwac
            position.X = this.mPositionZoom.X - vCamera.X + Screen.X;// +(this.X * (100 / Zoom));
            position.Y = this.mPositionZoom.Y - vCamera.Y + Screen.Y; // -(this.Y * (50 / Zoom));

            Rectangle DrawBox = new Rectangle((int)position.X, (int)position.Y, (int) (100 / Zoom), (int) (65 / Zoom) );

            spriteBatch.Draw(this.mSpriteTexture, DrawBox, Color.White * alpha);
            //spriteBatch.Draw(this.mSpriteTexture, position, Color.White);

            if (false)
            {
                spriteBatch.DrawString(this.Game._spr_font, string.Format("[{0},{1}]", X, Y),
                    new Vector2(position.X + (this.mSpriteTexture.Width / 2) - 25, position.Y + 10), Color.White);

                spriteBatch.DrawString(this.Game._spr_font, string.Format("{0} {1}", mPosition.X, mPosition.Y),
                   new Vector2(position.X + 20, position.Y + Game._spr_font.LineSpacing + 10), Color.White);
            }
        }

        public virtual string ToString()
        {
            return "G";
        }
    }
}
