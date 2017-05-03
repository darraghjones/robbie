using System;

namespace Lib.Domain
{
    public class RandomCleaner : ICleaner
    {
        public Move Move(Neighbourhood neighbourhood)
        {
            return MoveHelpers.GetRandomMove();
        }
    }
}