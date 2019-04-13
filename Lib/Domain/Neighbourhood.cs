namespace Lib.Domain
{
    public struct Neighbourhood
    {
        public Cell North { get; }
        public Cell South { get; }
        public Cell East { get; }
        public Cell West { get; }
        public Cell Current { get; }

        public Neighbourhood(Cell north, Cell south, Cell east, Cell west, Cell current)
        {
            North = north;
            South = south;
            East = east;
            West = west;
            Current = current;
        }

        public override string ToString()
        {
            return $"\t\t {North.ToString().Substring(0,1)}\n" +
                   $"\t\t{West.ToString().Substring(0, 1)}" +
                   $"{Current.ToString().Substring(0, 1)}" +
                   $"{East.ToString().Substring(0, 1)}\n" +
                   $"\t\t {South.ToString().Substring(0, 1)}\n\n";
        }
    }
}