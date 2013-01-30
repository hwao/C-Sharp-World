using System;

namespace World.Map.Field
{
    class Grass : World.Map.Field.iField
    {

        public Grass( Game1 Game ) : base (Game)
        {
            
        }

        public override string ToString()
        {
            return "G";
        }
    }
}
