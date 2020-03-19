using System;
using System.Collections.Generic;
using System.Linq;

namespace TimeTable
{
    public static class Mutations
    {
        #region Private
        private static void Swap<T>(ref T a, ref T b) where T : IComparable
        {
            var tmp = a;
            a = b;
            b = tmp;
        }
        private static int RightSearchNonSorted<T>(T[] source) where T : IComparable
        {
            for (int i = source.Length - 2; i >= 0; i--)
                if (source[i + 1].CompareTo(source[i]) > 0)
                    return i;
            return 0;
        }
        private static int FindRightBiggerThanValue<T>(T[] source, T value) where T : IComparable
        {
            for (int i = source.Length - 1; i >= 0; i--)
                if (source[i].CompareTo(value) > 0)
                    return i;
            return 0;
        }
        private static T[] SortRigthPart<T>(T[] source, int startPosition) where T : IComparable
        {
            return source.Take(startPosition).Concat(source.Skip(startPosition).OrderBy(x => x)).ToArray();
        }
        #endregion Private
        public static IEnumerable<T[]> GetPermutations<T>(T[] source) where T : IComparable
        {
            source = source.OrderBy(x => x).ToArray();
            yield return source;
            int i = RightSearchNonSorted(source);
            while (i >= 0)
            {
                var j = FindRightBiggerThanValue(source, source[i]);
                if (j == 0) yield break;
                Swap(ref source[i], ref source[j]);
                source = SortRigthPart(source, i + 1);
                yield return source;
                i = RightSearchNonSorted(source);
            }
        }
        public static IEnumerable<IEnumerable<int>> GetCombinations(int[] source, int mustContain, int maxSum)
        {
            Stack<int> intStack = new Stack<int>();

            intStack.Push(60 * 24 + 1);//maxmin*maxhour+1 // endworktime - beginworktime
            int numbers = intStack.Count();
            while (numbers > 0)
            {
                int curr = intStack.Pop();
                numbers--;
                var lower = source.Where(x => x < curr).OrderByDescending(x => x);
                if (lower.Count() > 0)
                {
                    foreach (int insNumber in lower)
                        while (intStack.Sum() + insNumber <= maxSum)
                        {
                            intStack.Push(insNumber);
                            numbers++;
                        }
                    if (maxSum - intStack.Sum() <= 0 && intStack.Contains(mustContain))
                    {
                        yield return intStack;
                    }
                }
            }
        }
    }

    class Program
    {
        static readonly int maxTime = 180;
        static readonly int selected = 60;
        static void Show(IEnumerable<int> items)
        {
            foreach (var item in items)
                Console.Write(item.ToString() + " ");
            Console.WriteLine();
        }
        static void ShowTab(IEnumerable<int> items)
        {
            foreach (var item in items)
                Console.Write("    "+item.ToString() + " ");
            Console.WriteLine();
        }

        static void Main()
        {
            int[] times = { 120, 90, 60, 45 };
            foreach (var item in Mutations.GetCombinations(times, selected, maxTime))
            {
                Show(item);
                foreach (var subitem in Mutations.GetPermutations(item.ToArray()))
                {
                    ShowTab(subitem);
                }
                Console.WriteLine(item.Sum());
            }
        }
    }
}
