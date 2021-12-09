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
            int?[][] intData = stringData.Select(s => s.Select(n => (int?)n - ZERO).ToArray()).ToArray();
            sw.Start();

            var sum = 0;
            var topN = 3;
            var sizes = Enumerable.Range(0, topN).Select(x => 0).ToList();
            for (var y = 0; y < intData.Length; y++)
            {
                for (var x = 0; x < intData[y].Length; x++)
                {
                    var size = GetBasinSize(intData, new List<(int, int)>(), y, x);
                    sizes.Add(size);
                    sizes = sizes.OrderBy(s => s).TakeLast(3).ToList();
                }
            }
            sw.Stop();
            Console.WriteLine($"Answer is {string.Join(", ", sizes)}");
            Console.WriteLine($"Took {sw.ElapsedMilliseconds} ms after reading in the start data.");
        }

        private static int GetBasinSize(int?[][] intData, List<(int X, int Y)> visitedNodes, int y, int x)
        {
            if (visitedNodes.Any(xy => xy.Y == y && xy.X == x))
            {
                return 0;
            }

            visitedNodes.Add((x, y));

            var currVal = intData[y][x].Value;

            // fix neighbour being previous x y
            var neighbour1 = intData.ElementAtOrDefault(y + 1)?.ElementAtOrDefault(x) ?? int.MaxValue;
            var neighbour2 = intData.ElementAtOrDefault(y - 1)?.ElementAtOrDefault(x) ?? int.MaxValue;
            var neighbour3 = intData.ElementAtOrDefault(y)?.ElementAtOrDefault(x + 1) ?? int.MaxValue;
            var neighbour4 = intData.ElementAtOrDefault(y)?.ElementAtOrDefault(x - 1) ?? int.MaxValue;
            //visitedNodes.Add((x, y + 1));
            //visitedNodes.Add((x, y - 1));
            //visitedNodes.Add((x + 1, y));
            //visitedNodes.Add((x - 1, y));
            var sum = 0;

            if (Math.Abs(neighbour1 - currVal) == 1)
            {
                sum += GetBasinSize(intData, visitedNodes, y + 1, x);
            }

            if (Math.Abs(neighbour2 - currVal) == 1)
            {
                sum += GetBasinSize(intData, visitedNodes, y - 1, x);
            }

            if (Math.Abs(neighbour3 - currVal) == 1)
            {
                sum += GetBasinSize(intData, visitedNodes, y, x + 1);
            }

            if (Math.Abs(neighbour4 - currVal) == 1)
            {
                sum += GetBasinSize(intData, visitedNodes, y, x - 1);
            }

            return 1 + sum;
        }
    }
}