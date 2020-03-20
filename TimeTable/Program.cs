using System;
using System.Collections.Generic;
using System.Linq;

namespace TimeTable
{
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
