using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace World.Map.Field
{
    class Beach : World.Map.Field.iField
    {

        public Beach(Game1 Game)
            : base(Game)
        {
            
        }

        public override void LoadContent(ContentManager Content)
        {
            this.mSpriteTexture = Content.Load<Texture2D>("beach");
        }

        public override string ToString()
        {
            return "B";
        }
    }
}
