using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorCalculator
{
    class Tile
    {
        public readonly double Length;
        public readonly double Width;
        public double Current_Length { get; set; }
        public double Current_Width { get; set; }
        public double? Cost { get; set; }

        public Tile(double length, double width)
        {
            if (length != 0 || width != 0)
            {
                this.Length = Program.LEN_TILE;
                this.Width = Program.WID_TILE;
                this.Current_Length = length;
                this.Current_Width = width;
                this.Cost = Program.COST_TILE;
            }

        }

        public void ChangeSizes(double l, double w)
        {
            this.Current_Length = l;
            this.Current_Width = w;
        }

        public Tile()
        {
            this.Length = Program.LEN_TILE;
            this.Width = Program.WID_TILE;
            this.Cost = Program.COST_TILE;
            this.Current_Length = Program.LEN_TILE;
            this.Current_Width = Program.WID_TILE;
        }
    }
}
