using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace ConsoleAppSample.Study
{
    public static class Sample
    {

        public static IEnumerable<string> GenerateV1()
        {
            var list = new List<string>();
            var letter = 'a';
            Console.Write("Exec roop");
            while (letter <= 'z')
            {
                Console.Write("+");
                list.Add(letter.ToString());
                letter++;
            }
            return list;
        }
        public static IEnumerable<string> GenerateV2()
        {
            var letter = 'a';
            Console.Write("Exec roop");
            while (letter <= 'z')
            {
                Console.Write("+");
                yield return letter.ToString();
                letter++;
            }
        }

        public static IEnumerable<string> Zip(IEnumerable<string> first, IEnumerable<string> second)
        {
            using (var firstSequence = first.GetEnumerator())
            {
                using (var secondSequence = second.GetEnumerator())
                {
                    while (firstSequence.MoveNext() && secondSequence.MoveNext())
                    {
                        yield return string.Format("{ 0} {1}", firstSequence.Current, secondSequence.Current);
                    }
                }
            }
        }

        public static IEnumerable<TResult> Zip<T1, T2, TResult>(IEnumerable<T1> first, IEnumerable<T2> second, Func<T1, T2, TResult> zipper)
        {
            using (var firstSequence = first.GetEnumerator())
            {
                using (var secondSequence = second.GetEnumerator())
                {
                    while (firstSequence.MoveNext() && secondSequence.MoveNext())
                    {
                        yield return zipper(firstSequence.Current, secondSequence.Current);
                    }
                }
            }
        }

        public static IEnumerable<int> AllNumbers()
        {
            var number = 0;
            while (number <= int.MaxValue)
            {
                yield return number;
                number++;
            }
        }
    }
}