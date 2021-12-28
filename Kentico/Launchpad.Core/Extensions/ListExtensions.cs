using System.Collections.Generic;

namespace Launchpad.Core.Extensions
{
    public static class ListExtensions
    {
        public static void Move<T>(this IList<T> list, int iIndexToMove, int moveDirection)
        {
            if (moveDirection == -1) // move up
            {
                if (iIndexToMove == 0) return;
                var old = list[iIndexToMove - 1];
                list[iIndexToMove - 1] = list[iIndexToMove];
                list[iIndexToMove] = old;
            }
            else
            {
                if (iIndexToMove == list.Count - 1) return;
                var old = list[iIndexToMove + 1];
                list[iIndexToMove + 1] = list[iIndexToMove];
                list[iIndexToMove] = old;
            }
        }
    }
}
