using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace World.Map.Field.Hill
{
    class Hill : World.Map.Field.iField
    {
        public Hill(Game1 Game)
            : base(Game)
        {
            this.vSize.Y = 80;
            this.vFix.Y = -15;
        }

        public override void LoadContent(ContentManager Content)
        {
            this.mSpriteTexture = Content.Load<Texture2D>("Field/Hill/" + this.GetType().Name);
        }
    }
}

