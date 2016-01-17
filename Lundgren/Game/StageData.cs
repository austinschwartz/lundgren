using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lundgren.Game.Helpers.Formats;

namespace Lundgren.Game
{
    public class StageData
    {
        public Coord LeftEdge;
        public Coord RightEdge;
    }

    public class YoshisIsland : StageData
    {
        public YoshisIsland()
        {
            LeftEdge = new Coord(-57.92, -17.9);
            RightEdge = new Coord(57.92, -17.9);
        }
    }

    public class PokemonStadium : StageData
    {
        public PokemonStadium()
        {
            LeftEdge = new Coord(-89.67, -14.4);
            RightEdge = new Coord(89.67, -14.4);
        }
    }

    public class FinalDestination : StageData
    {
        public FinalDestination()
        {
            LeftEdge = new Coord(-87.48569, -14.4);
            RightEdge = new Coord(87.48569, -14.4);
        }
    }

    public class Battlefield : StageData
    {
        public Battlefield()
        {
            LeftEdge = new Coord(-70.32, -14.4);
            RightEdge = new Coord(70.32, -14.4);
        }
    }

    public class Dreamland : StageData
    {
        public Dreamland()
        {
            LeftEdge = new Coord(-79.1913, -14.3912);
            RightEdge = new Coord(79.1913, -14.3912);
        }
    }

    public class FountainOfDreams : StageData
    {
        public FountainOfDreams()
        {
            LeftEdge = new Coord(-65.26755, -13.77622);
            RightEdge = new Coord(65.26755, -13.77622);
        }
    }
}
