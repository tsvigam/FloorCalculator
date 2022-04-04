using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorCalculator
{
    public struct Result
    {
        double Cost;
        int TilesCount;
        public Result(double cost, int count)
        {
            Cost = cost;
            TilesCount = count;
        }
    }
    class BuilderHelper
    {
        private List<Tile> scrap = new List<Tile>();
        //Square Max;
        private Square CurrentSq { get; set; }
        private int count;
        public int CountOfFullTiles { get; private set; } = 0;
        private List<Tile> installTiles = new List<Tile>();
        private List<Tile> innerLineScope = new List<Tile>();
        private Tile TemplateTile { get; set; }
        private Room Room { get; set; }
        public BuilderHelper(Room r, Tile t)
        {
            this.TemplateTile = t;
            this.Room = r;
            CurrentSq = GetMaxSquare();
        }
        public void Build1Square()
        {
            /*if (CurrentSq != GetMaxSquare())
                { CurrentSq = sq; }*/
            count = 0;
            if (Room._Orientation == Orientation.Horizontal)
            {
                for (int j = 0; j < (int)(CurrentSq.Length * 1000 / TemplateTile.Width); j++)
                {
                    count = (int)(CurrentSq.Width * 1000 / TemplateTile.Length);
                    for (int i = 0; i < count; i++)
                    {
                        installTiles.Add(new Tile());
                        CountOfFullTiles += 1;
                    }
                    SortScrap();
                    if (scrap.Count != 0)
                    {
                        Tile? foundedScrapTile = SearchInScrap(); // Error with search po width
                        if (foundedScrapTile != null)
                        {
                            scrap.Remove(foundedScrapTile);
                            installTiles.Add(MakeCut(foundedScrapTile, CurrentSq.Width * 1000 % TemplateTile.Length, 0, 's'));
                        }
                    }
                    else
                    {
                        Tile LastTileOnRow = new Tile();
                        installTiles.Add(MakeCut(LastTileOnRow, CurrentSq.Width * 1000 % TemplateTile.Length, 0, 's'));

                    }
                    if (scrap.Count == 0 && installTiles.Count != 0) // странное условие
                        CountOfFullTiles += 1;
                }
            }

        }
        public void InnerLineFillStart()
        {
            //count = (int)(CurrentSq.Width * 1000 / TemplateTile.Length); 
            if (Room._Orientation == Orientation.Horizontal)
            {
                innerLineScope = new List<Tile>();

                for (int i = 0; i < count; i++)
                {
                    installTiles.Add(MakeCut(new Tile(), 0, CurrentSq.Length * 1000 % TemplateTile.Width, 'i'));
                    /*  if (scrap.Last().Current_Width != TemplateTile.Width)
                      {
                          innerLineScope.Add(scrap.Last());
                          scrap.Remove(scrap.Last());
                      }*/

                }
                if (scrap.Count != 0)
                //search on scrap added) ////fill last piece of inner Line from scrap
                {
                    Tile foundedScrapTile = SearchInScrap(CurrentSq.Width * 1000 % TemplateTile.Length, TemplateTile.Width);
                    if (foundedScrapTile != null)
                    {
                        scrap.Remove(foundedScrapTile);
                        installTiles.Add(MakeCut(foundedScrapTile, CurrentSq.Width * 1000 % TemplateTile.Length, CurrentSq.Length * 1000 % TemplateTile.Width, 'i'));
                    }
                    else
                        installTiles.Add(MakeCut(new Tile(), CurrentSq.Width * 1000 % TemplateTile.Length, CurrentSq.Length * 1000 % TemplateTile.Width, 'i'));

                }
                else //fill last piece of inner Line 
                {
                    installTiles.Add(MakeCut(new Tile(), CurrentSq.Width * 1000 % TemplateTile.Length, CurrentSq.Length * 1000 % TemplateTile.Width, 'i'));

                }
            }
        }
        public void InnerLineFillEnd()
        {
            IEnumerator<Square> SquareEnumerator = GetNextSquare(1);
            SquareEnumerator.MoveNext();
            CurrentSq = SquareEnumerator.Current;
            //CurrentSq.Change_sizes(CurrentSq.Length, CurrentSq.Width - (installTiles.Last().Current_Width * 0.001));
            if (CurrentSq != null && Room._Orientation == Orientation.Horizontal)
            {
                count = (int)(CurrentSq.Width * 1000 / TemplateTile.Length);
                // first fill next square tiles from inner line scope
                // this case for bottom and right sum square
                double sumLengthTilesFromInnerLineScope = 0;
                while (CurrentSq.Width * 1000 >= sumLengthTilesFromInnerLineScope && innerLineScope.Count > 0)
                {
                    if (sumLengthTilesFromInnerLineScope + innerLineScope.Last().Current_Length > CurrentSq.Width * 1000)
                        break;
                    else
                        sumLengthTilesFromInnerLineScope += innerLineScope.Last().Current_Length;
                    installTiles.Add(innerLineScope.Last());
                    innerLineScope.Remove(innerLineScope.Last());
                }
                if (CurrentSq.Width * 1000 != sumLengthTilesFromInnerLineScope && innerLineScope.Count > 0)
                {
                    installTiles.Add(MakeCut(innerLineScope.Last(), CurrentSq.Width * 1000 - sumLengthTilesFromInnerLineScope, 0, 's'));
                    innerLineScope.Remove(innerLineScope.Last());
                    CountOfFullTiles++;
                }

                else
                {
                    // installTiles.Add(innerLineScope.Last());
                    //     installTiles.Add(innerLineScope.Last());
                    // uze dobavleno     innerLineScope.Remove(innerLineScope.Last());
                }

                if (innerLineScope.Count != 0)
                {
                    scrap.AddRange(innerLineScope);
                    innerLineScope.Clear();

                }
                CurrentSq = CurrentSq.Change_sizes(CurrentSq.Length - (installTiles.Last().Current_Width * 0.001), CurrentSq.Width);

            }
        }

        public string Calculate(Square? sq)
        {

            installTiles = new List<Tile>();
            //Result result = new Result();
            if (CurrentSq != GetMaxSquare())
            { CurrentSq = sq; }
            count = 0;
            //Search on Horizontal on Length of Tiles
            if (Room._Orientation == Orientation.Horizontal)
            {

                for (int j = 0; j < (int)(CurrentSq.Length * 1000 / TemplateTile.Width); j++)
                {
                    count = (int)(CurrentSq.Width * 1000 / TemplateTile.Length);
                    for (int i = 0; i < count; i++)
                    {
                        installTiles.Add(new Tile());
                    }
                    SortScrap();
                    if (scrap.Count != 0)
                    {
                        Tile foundedScrapTile = SearchInScrap();
                        if (foundedScrapTile != null)
                        {
                            scrap.Remove(foundedScrapTile);
                            installTiles.Add(MakeCut(foundedScrapTile, CurrentSq.Width * 1000 % TemplateTile.Length, 0, 's'));
                        }
                    }
                    else
                    {
                        Tile LastTileOnRow = new Tile();
                        installTiles.Add(MakeCut(LastTileOnRow, CurrentSq.Width * 1000 % TemplateTile.Length, 0, 's'));
                    }
                }
                //Before well done!!!

                innerLineScope = new List<Tile>();
                for (int i = 0; i < count; i++)
                {
                    installTiles.Add(MakeCut(new Tile(), TemplateTile.Length, CurrentSq.Length * 1000 % TemplateTile.Width, 's'));
                    if (scrap.Last().Current_Width != TemplateTile.Width)
                    {
                        innerLineScope.Add(scrap.Last());
                        scrap.Remove(scrap.Last());
                    }

                }
                installTiles.Add(MakeCut(new Tile(), CurrentSq.Width * 1000 % TemplateTile.Length, 0, 's'));
                installTiles.Last().ChangeSizes(installTiles.Last().Current_Length, CurrentSq.Length * 1000 % TemplateTile.Width);
                innerLineScope.Add(new Tile(installTiles.Last().Current_Length, TemplateTile.Width - installTiles.Last().Current_Width));
            }
            //result = new Result((double)((double)installTiles.Count * TemplateTile.Cost), installTiles.Count);
            string s = ((double)installTiles.Count * TemplateTile.Cost).ToString() + "$, count of tiles - " + installTiles.Count.ToString();
            return s;
        }

        private Tile? SearchInScrap() //неправильно выбирает тайлы по ширине, когда в скрапе иинер лайн остатки
        {
            //return scrap.SkipWhile(i => (i.Current_Length < CurrentSq.Width * 1000 % TemplateTile.Length) && (i.Current_Width < TemplateTile.Width)).First();

            return scrap.SkipWhile(i => (i.Current_Length < CurrentSq.Width * 1000 % TemplateTile.Length) && (i.Current_Width != TemplateTile.Width)).FirstOrDefault();
        }
        private Tile SearchInScrap(double l, double w)
        {
            if (l == 0)
                l = TemplateTile.Length;
            if (w == 0)
                w = TemplateTile.Width;
            SortScrap();
            return scrap.SkipWhile(i => (i.Current_Length < l) && (i.Current_Width < w)).First();//check!
        }
        private void SortScrap()
        {
            scrap = scrap.OrderBy(x => x.Current_Length)
                  .ThenBy(x => x.Current_Width).ToList();
        }
        private Tile MakeCut(Tile t, double l, double w, char? placeForCut)
        {
            double cutPieceL, cutPieceW;
            double changedW = w, changedL = l;
            if (w == 0)
            {
                changedW = t.Current_Width;
                //cutPieceW = t.Current_Width;
            }
            if (l == 0)
            {
                changedL = t.Current_Length;
                // cutPieceL = t.Current_Length;
            }

            //if (t.Current_Length == changedL)
            //    cutPieceL = t.Current_Length;
            //else
            if (t.Current_Length - changedL == 0)
                cutPieceL = 0;
            else
                cutPieceL = t.Current_Length - changedL;
            if (t.Current_Width == changedW)
                cutPieceW = t.Current_Width;
            else
                cutPieceW = t.Current_Width - changedW;
            if (l == 0)
                cutPieceL = t.Current_Length;
            if (w == 0)
                cutPieceW = t.Current_Width;
            t.ChangeSizes(changedL, changedW);
            //if ((cutPieceW == 0)||(cutPieceL == 0))//error on inner line loop.
            //    return t;
            if (((cutPieceL != 0) && (cutPieceW != 0)) || w != 0)
                switch (placeForCut)
                {
                    case 'i':
                        if ((l != 0 && cutPieceL == 0))
                            innerLineScope.Add(new Tile(l, cutPieceW));
                        else
                            innerLineScope.Add(new Tile(cutPieceL, cutPieceW));
                        break;
                    case 's':
                        scrap.Add(new Tile(cutPieceL, cutPieceW));
                        break;
                    default:
                        scrap.Add(new Tile(cutPieceL, cutPieceW));
                        break;
                }


            return t;
        }
        private IEnumerator<Square> GetNextSquare(int n)
        {//??? нужно, чтоб вернулись разные кусочки комнаты кроме максимального
            if (Room.Squares.Where(i => i.InnerLane != null).ToList().Count != 0)
                yield return Room.Squares.Where(i => i.InnerLane != null).ElementAt(n - 1);
        }

        private List<Square> GetSquaresOfRoom()
        {
            return Room.Squares;
        }
        public int GetCountOfTiles()
        {
            if (this.Room.Squares.Count == 1)
                if (scrap.Count != 0)
                    CountOfFullTiles += scrap.Count;
            if (innerLineScope.Count != 0)
                CountOfFullTiles += innerLineScope.Count;
            return CountOfFullTiles;
        }
        //Became build with this piece
        private Square GetMaxSquare()
        {
            return Room.Squares.Find(i => (i.InnerLane == null));
        }
    }
}
