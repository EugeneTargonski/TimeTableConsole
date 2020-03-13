using System;
using System.Collections.Generic;
using System.Linq;

namespace TimeTable
{
    class Program
    {
        static readonly int maxTime = 240;
        static readonly int selected = 60;
        static void Show(IEnumerable<int> items)
        {
            foreach (var item in items)
                Console.Write(item.ToString() + " ");
            Console.WriteLine();
        }
        static void Calc(IEnumerable<int> items, int mustContain)
        {
            if (maxTime - items.Sum() <= 0 && items.Contains(mustContain))
            {

                List<int> itemsForPermutation = GenerateItemsForPermutations(items, mustContain);

                Show(items);
                Show(itemsForPermutation);

                Console.WriteLine();
                Console.WriteLine(items.Sum());

                
            }
        }
        public static List<int> GenerateItemsForPermutations(IEnumerable<int> items, int mustContain)
        {
            var containsCount = items.Where(x => x == mustContain).Count();
            List<int> itemsForPermutation = items.Where(x => x != mustContain).ToList();
            for (int i = containsCount - 1; i > 0; i--)
            {
                itemsForPermutation.Add(mustContain);
            }
            return itemsForPermutation;
        }
        public static IEnumerable<T[]> GenerateAllPermutations<T>(T[] source, int count)
        {
            if (source.Length < 0 || source.Length < count) throw new ArgumentOutOfRangeException();
            if (count <= 0) yield break;
            T[] result = new T[count];
            Stack<int> stackInd = new Stack<int>();
            stackInd.Push(0);
            while (stackInd.Count > 0)
            {
                int position = stackInd.Count - 1;
                int indexValue = stackInd.Pop();
                while (indexValue < source.Length)
                {
                    result[position++] = source[indexValue++];
                    stackInd.Push(indexValue);
                    if (position == count)
                    {
                        yield return result;
                        break;
                    }
                }
            }
        }

        static void Main()
        {
            int[] times = { 120, 90, 60, 45, 30 };
            Stack<int> intStack = new Stack<int>();

            intStack.Push(60*24+1);//maxmin*maxhour+1 // endworktime - beginworktime
            int numbers = intStack.Count();
            while (numbers>0)
            {
                int curr = intStack.Pop();
                numbers--;
                var lower = times.Where(x => x < curr).OrderByDescending(x=>x);
                if (lower.Count()>0)
                {
                    foreach (int insNumber in lower)
                        while (intStack.Sum() + insNumber <= maxTime)
                        {
                            intStack.Push(insNumber);
                            numbers++;
                        }
                    Show(intStack, selected);
                }
            }


        }
    }
}
