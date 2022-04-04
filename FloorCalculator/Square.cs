using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorCalculator
{
    enum Position
    {
        Top,
        Bottom,
        Left,
        Right
    }
    class Square
    {
        public double Length { get; private set; }
        public double Width { get; private set; }
        public Position? InnerLane { get; private set; }

        public Square(double length, double width)
        {
            this.Length = length;
            this.Width = width;
        }

        public void SetInnerLine(char pos)
        {
            switch (pos)
            {
                case 'T':
                    this.InnerLane = Position.Top;
                    break;
                case 'L':
                    this.InnerLane = Position.Left;
                    break;
            }
        }

        public Square Change_sizes(double length, double width)
        {
            if (length > 0 && width > 0)
            {
                this.Length = length;
                this.Width = width;
            }
            return this;
        }
    }
}
