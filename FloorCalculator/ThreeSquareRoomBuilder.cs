using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorCalculator
{
    class ThreeSquareRoomBuilder : IBuilder
    {
        BuilderHelper helper;

        public ThreeSquareRoomBuilder(Room r, Tile t)
        {
            helper = new BuilderHelper(r, t);
        }

        public string Algoritm()
        {
            //string res = "";
            helper.Build1Square();
            helper.InnerLineFillStart();
            helper.InnerLineFillEnd();
            helper.Build1Square();
            helper.InnerLineFillStart();
            helper.InnerLineFillEnd();
            helper.Build1Square();
            return helper.GetCountOfTiles().ToString();
        }
    }
}
