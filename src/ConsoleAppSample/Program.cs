using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        var firstLine = Console.ReadLine();
        var N = int.Parse(firstLine.Split(' ')[0]);
        var X = int.Parse(firstLine.Split(' ')[1]);

        var sweets = Program.GetSweetsPriceList(N);
        Console.WriteLine(Program.Caluculate(sweets, X));
    }

    static List<int> GetSweetsPriceList(int numberOfList)
    {
        var list = new List<int>();

        for (int i = 0; i <= numberOfList; i++)
        {
            int temp = 0;
            if (int.TryParse(Console.ReadLine(), out temp))
                list.Add(temp);
        }
        return list;
    }

    static int Caluculate(List<int> sweets, int budget)
    {
        sweets = sweets.ToList();
        var sum = sweets.Sum();
        var difference = sum - budget;

        while (difference > 0)
        {
            var bigItems = sweets.Where(item => item > difference);
            var smallItems = sweets.Where(item => item < difference);
            var equalItems = sweets.Where(item => item == difference);

            if (equalItems.Any())
            {
                var target = equalItems.Single();
                difference -= target;
                sweets.Remove(target);
            }
            else if (bigItems.Any())
            {
                var target = bigItems.Min();
                difference -= target;
                sweets.Remove(target);
            }
            else if (smallItems.Any())
            {
                var target = smallItems.Max();
                difference -= target;
                sweets.Remove(target);
            }

        }
        return budget - sweets.Sum();
    }
}
