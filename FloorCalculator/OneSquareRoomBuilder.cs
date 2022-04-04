using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorCalculator
{
    class OneSquareRoomBuilder : IBuilder
    {
        BuilderHelper helper;
        public OneSquareRoomBuilder(Room r, Tile t)
        {
            helper = new BuilderHelper(r, t);
        }
        public string Algoritm()
        {
            helper.Build1Square();
            helper.InnerLineFillStart();
            string res = helper.GetCountOfTiles().ToString() + " Cost is " + helper.CountOfFullTiles * Program.COST_TILE;
            return res;
        }
    }
}
