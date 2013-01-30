using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace World.Map.Field.Hill
{
    class HillW : World.Map.Field.Hill.Hill
    {
        public HillW(Game1 Game)
            : base(Game)
        {
            this.vSize.Y = 65;
            this.vFix.Y = 0;
        }
    }
}

