using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorCalculator
{
    class TwoSqaresRoomBuilder : IBuilder
    {
        BuilderHelper helper;
        public TwoSqaresRoomBuilder(Room r, Tile t)
        {
            helper = new BuilderHelper(r, t);
        }
        public string Algoritm()
        {
            helper.Build1Square();
            helper.InnerLineFillStart();
            helper.InnerLineFillEnd();
            helper.Build1Square();
            return helper.GetCountOfTiles().ToString();
        }
    }
}
