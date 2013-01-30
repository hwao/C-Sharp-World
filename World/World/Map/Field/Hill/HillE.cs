using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace World.Map.Field.Hill
{
    class HillE : World.Map.Field.Hill.Hill
    {
        public HillE(Game1 Game)
            : base(Game)
        {
            this.vSize.Y = 65;
            this.vFix.Y = 0;
        }
    }
}

