using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace World
{
    class Player
    {
        public Vector2 Position;
        public int Radius = 10;

        public Rectangle Sprite;
        public Texture2D mSpriteTexture;

        public Player(Vector2 Position, int Radius)
        {
            this.Position = Position;
            this.Radius = Radius;

            this.Sprite = new Rectangle((int)this.Position.X, (int)this.Position.Y, 11, 32);
        }

        // treeShort.png

        public virtual void LoadContent(ContentManager Content)
        {
            this.mSpriteTexture = Content.Load<Texture2D>("treeShort");
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 vCamera, Rectangle Screen, float alpha)
        {
            this.Sprite.X = (int)(this.Position.X - vCamera.X + Screen.X);
            this.Sprite.Y = (int)(this.Position.Y - vCamera.Y + Screen.Y);

            spriteBatch.Draw(this.mSpriteTexture, this.Sprite, Color.White * alpha);
        }

        public Rectangle getRectangle(Vector2 vCamera, Rectangle Screen)
        {
            this.Sprite.X = (int)(this.Position.X - vCamera.X + Screen.X);
            this.Sprite.Y = (int)(this.Position.Y - vCamera.Y + Screen.Y);

            return this.Sprite;
        }
    }
}
