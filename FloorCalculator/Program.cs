using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace FloorCalculator
{
    class Program
    {
        public const int LEN_TILE = 1500;
        public const int WID_TILE = 180;
        public const double COST_TILE = 9.45;
        public const double COST_WITH_WORK = 75.5;
        public const Orientation ORIENTATION_ROOM = Orientation.Horizontal;

        static void Main(string[] args)
        {
            List<Room> rooms = new List<Room>();
            Console.WriteLine("We are start input sizes");
            Thread.Sleep(1000);
            Console.WriteLine("if you need input room, pls press Enter");
            while (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                Console.WriteLine("Input sizes of Squares of next room ");
                string sizes_string = Console.ReadLine();
                List<double> sizes = new List<double>();
                Regex regex = new Regex(@"\d+\.?\d*");
                MatchCollection matches = regex.Matches(sizes_string);
                if (matches.Count > 0)
                {
                    for (int i = 0; i < matches.Count; i++)
                    {
                        sizes.Add(Double.Parse(matches[i].Value));
                    }
                }
                Room r = new Room(ORIENTATION_ROOM);
                int n = sizes.Count / 2;
                for (int i = 0; i < n; i++)
                {
                    r.SetSquare(sizes[0], sizes[1]);
                    sizes.RemoveAt(0); sizes.RemoveAt(0);
                }
                rooms.Add(r);
                Console.WriteLine("if you need input room, pls press Enter");
            }
            rooms[0].CalculateLengthMax();
            rooms[0].CalculateWidthhMax();
            rooms[0].SetInnerLines();
            double squareHouse = 0;
            foreach (Room r in rooms)
            {
                squareHouse += r.GetSquareInM2();
                Calculate_Room(r);
            }
            Console.WriteLine(((double)squareHouse));
        }

        public static void Calculate_Room(Room room)
        {
            Tile tile = new Tile();
            IBuilder builder;
            switch (room.Squares.Count)
            {
                case 1:
                    builder = new OneSquareRoomBuilder(room, tile);
                    break;
                case 2:
                    builder = new TwoSqaresRoomBuilder(room, tile);
                    break;
                case 3:
                    builder = new ThreeSquareRoomBuilder(room, tile);
                    break;
                default:
                    builder = new OneSquareRoomBuilder(room, tile);
                    break;
            }
            Console.WriteLine(builder.Algoritm());
        }
    }
}
