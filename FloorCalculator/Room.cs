using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorCalculator
{
    public enum Orientation
    {
        Horizontal,
        Vertical
    }

    class Room
    {
        public List<Square> Squares { private set; get; } = new List<Square>();
        public Orientation? _Orientation { private set; get; }
        public double RoomLength { get; private set; } = 0;
        public double RoomWidth { get; private set; } = 0;

        public Room(Orientation o)
        {
            this._Orientation = o;
        }

        public Room()
        {
            _Orientation = Program.ORIENTATION_ROOM;
        }

        public void SetSquare(double length, double width)
        {
            Squares.Add(new Square(length, width));
        }

        public void CalculateLengthMax()
        {
            if (this._Orientation == Orientation.Vertical)
            {
                RoomLength = FindMaxSlide('l');
            }
            else
            {
                foreach (Square s in Squares)
                    this.RoomLength += s.Length;
            }
        }

        public void CalculateWidthhMax()
        {
            if (this._Orientation == Orientation.Horizontal)
            {
                RoomWidth = FindMaxSlide('w');
            }
            else
            {
                foreach (Square s in Squares)
                    this.RoomWidth += s.Width;
            }
        }

        public void SetInnerLines()
        {
            foreach (Square s in FindLittleSquares())
            {
                if (this._Orientation == Orientation.Horizontal)
                    s.SetInnerLine('T');
                else
                    s.SetInnerLine('L');
            }
        }

        private List<Square> FindLittleSquares()
        {
            List<Square> littleSquares2 = new List<Square>(Squares);
            List<Square> result;
            if (this._Orientation == Orientation.Horizontal)
            {
                var maxW2 = (double)littleSquares2.Max(i => (i.Width));
                result = littleSquares2.Where(i => i.Width != maxW2).Select(i => i).ToList();
            }
            else
            {
                var maxL2 = (double)littleSquares2.Max(i => (i.Length));
                result = littleSquares2.Where(i => i.Length != maxL2).Select(i => i).ToList();
            }
            return result;
        }

        private double FindMaxSlide(char k)
        {
            switch (k)
            {
                case 'l':
                    {
                        List<double> lengths = new List<double>();
                        foreach (Square s in Squares)
                        {
                            lengths.Add(s.Length);
                        }
                        return lengths.Max();
                    }
                case 'w':
                    {
                        List<double> widthes = new List<double>();
                        foreach (Square s in Squares)
                        {
                            widthes.Add(s.Width);
                        }
                        return widthes.Max();
                    }
                default: return 1;
            }
        }

        public double GetSquareInM2()
        {
            double square = 0;
            foreach (Square s in Squares)
            {
                square += s.Length * s.Width;
            }
            return square;
        }
    }
}
