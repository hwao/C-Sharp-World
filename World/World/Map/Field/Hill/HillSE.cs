using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace World.Map.Field.Hill
{
    class HillSE : World.Map.Field.iField
    {
        public HillSE(Game1 Game)
            : base(Game)
        {

        }

        public override void LoadContent(ContentManager Content)
        {
            this.mSpriteTexture = Content.Load<Texture2D>("Field/Hill/hillSE");
        }

        public override string ToString()
        {
            return "HSE";
        }
    }
}

