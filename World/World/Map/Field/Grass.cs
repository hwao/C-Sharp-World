using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
