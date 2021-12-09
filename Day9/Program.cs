using System.Diagnostics;

namespace Day9
{
    public class Program
    {
        public static void Main(params string[] args)
        {
            Console.WriteLine("Hello world, today there are 2 challenges!");

            ChallengeOne();
            ChallengeTwo();
        }

        public static void ChallengeOne()
        {
            var sw = new Stopwatch();
            const string INPUT_FILE_NAME = "InputDataChallenge1.txt";
            var inputData = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), INPUT_FILE_NAME));
            IEnumerable<string> stringData = inputData.Split(Environment.NewLine, StringSplitOptions.TrimEntries);
            const char ZERO = '0';
            int?[][] intData = stringData.Select(s => s.Select(n => (int?)n - ZERO).ToArray()).ToArray();
            sw.Start();

            var sum = 0;
            for (var y = 0; y < intData.Length; y++)
            {
                for (var x = 0; x < intData[y].Length; x++)
                {
                    var currVal = intData[y][x].Value;

                    var neighbour1 = intData.ElementAtOrDefault(y + 1)?.ElementAtOrDefault(x) ?? int.MaxValue;
                    var neighbour2 = intData.ElementAtOrDefault(y - 1)?.ElementAtOrDefault(x) ?? int.MaxValue;
                    var neighbour3 = intData.ElementAtOrDefault(y)?.ElementAtOrDefault(x + 1) ?? int.MaxValue;
                    var neighbour4 = intData.ElementAtOrDefault(y)?.ElementAtOrDefault(x - 1) ?? int.MaxValue;

                    if (currVal < neighbour1 && 
                        currVal < neighbour2 &&
                        currVal < neighbour3 && currVal < neighbour4)
                    {
                        sum += (currVal + 1);
                    }

                }
            }
            sw.Stop();
            Console.WriteLine($"Sum of all low point values + 1 is {sum}");
            Console.WriteLine($"Took {sw.ElapsedMilliseconds} ms after reading in the start data.");
        }

        private static void ChallengeTwo()
        {
            var sw = new Stopwatch();
            const string INPUT_FILE_NAME = "InputDataChallenge2.txt";
            var inputData = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), INPUT_FILE_NAME));
            IEnumerable<string> stringData = inputData.Split(Environment.NewLine, StringSplitOptions.TrimEntries);
            const char ZERO = '0';
            int[][] intData = stringData.Select(s => s.Select(n => n - ZERO).ToArray()).ToArray();
            sw.Start();

            var sum = 0;
            for (var y = 0; y < intData.Length; y++)
            {
                for (var x = 0; x < intData[y].Length; x++)
                {
                    var currVal = intData[y][x];

                    var neighbour1 = intData.ElementAtOrDefault(y + 1)?.ElementAtOrDefault(x) ?? int.MaxValue;
                    var neighbour2 = intData.ElementAtOrDefault(y - 1)?.ElementAtOrDefault(x) ?? int.MaxValue;
                    var neighbour3 = intData.ElementAtOrDefault(y)?.ElementAtOrDefault(x + 1) ?? int.MaxValue;
                    var neighbour4 = intData.ElementAtOrDefault(y)?.ElementAtOrDefault(x - 1) ?? int.MaxValue;

                    if (currVal < neighbour1 && currVal < neighbour2 && currVal < neighbour3 && currVal < neighbour4)
                    {
                        sum += (currVal + 1);
                    }

                }
            }
            sw.Stop();
            Console.WriteLine($"Sum of all low point values + 1 is {sum}");
            Console.WriteLine($"Took {sw.ElapsedMilliseconds} ms after reading in the start data.");
        }



    }
}