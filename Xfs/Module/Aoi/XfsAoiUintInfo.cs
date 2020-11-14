using System;
using System.Collections.Generic;
using System.Text;

namespace Xfs
{
    public struct XfsAoiUintInfo
    {
        public HashSet<long> MovesSet;

        public HashSet<long> OldMovesSet;

        public HashSet<long> Enters;

        public HashSet<long> Leaves;

        public void Dispose()
        {
            MovesSet?.Clear();

            OldMovesSet?.Clear();

            Enters?.Clear();

            Leaves?.Clear();
        }

    }
    public struct AoiChangeInfo
    {
        public int movesSet;

        public int oldMovesSet;

        public int enters;

        public int leaves;

        public void Dispose()
        {
            movesSet = -1;

            oldMovesSet = -1;

            enters = -1;

            leaves = -1;
        }

    }

}
