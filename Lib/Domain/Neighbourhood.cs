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
    }
}