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

            var lowPoints = FindlowPoints(intData);
            var sum = lowPoints.Sum(c => intData[c.Y][c.X]) + lowPoints.Count();

            sw.Stop();
            Console.WriteLine($"Sum of all low point values + 1 is {sum}");
            Console.WriteLine($"Took {sw.ElapsedMilliseconds} ms after reading in the start data.");
        }

        private static List<Coordinate> FindlowPoints(int?[][] data)
        {
            var result = new List<Coordinate>();
            for (var y = 0; y < data.Length; y++)
            {
                for (var x = 0; x < data[y].Length; x++)
                {
                    var coord = new Coordinate(x, y);
                    if (IsLowPoint(data, coord))
                    {
                        result.Add(coord);
                    }

                }
            }

            return result;
        }

        private record Coordinate(int X, int Y);
        private static void ChallengeTwo()
        {
            var sw = new Stopwatch();
            const string INPUT_FILE_NAME = "InputDataChallenge2.txt";
            var inputData = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), INPUT_FILE_NAME));
            IEnumerable<string> stringData = inputData.Split(Environment.NewLine, StringSplitOptions.TrimEntries);
            const char ZERO = '0';
            int?[][] intData = stringData.Select(s => s.Select(n => (int?)n - ZERO).ToArray()).ToArray();
            sw.Start();

            var topN = 3;
            var sizes = new List<int>();
            for (var y = 0; y < intData.Length; y++)
            {
                for (var x = 0; x < intData[y].Length; x++)
                {
                    if (intData[y][x] == 9)
                    {
                        continue;
                    }

                    var size = GetBasinSize(intData, new List<Coordinate>(), x, y);
                    sizes.Add(size);
                }
            }
            var result = sizes.OrderBy(x => x).TakeLast(topN).ToArray();
            var sum = 1;
            foreach (var r in result)
            {
                sum *= r;
            }

            sw.Stop();
            Console.WriteLine($"Answer is {string.Join(", ", result)} resulting in sum of {sum}");
            Console.WriteLine($"Took {sw.ElapsedMilliseconds} ms after reading in the start data.");
        }

        private static bool IsLowPoint(int?[][] data, Coordinate coordinate)
        {
            var x = coordinate.X;
            var y = coordinate.Y;
            var currVal = data[y][x];
            if(currVal == 9)
            {
                return false;
            }
            var neighbour1 = data.ElementAtOrDefault(y + 1)?.ElementAtOrDefault(x);
            var neighbour2 = data.ElementAtOrDefault(y - 1)?.ElementAtOrDefault(x);
            var neighbour3 = data.ElementAtOrDefault(y)?.ElementAtOrDefault(x + 1);
            var neighbour4 = data.ElementAtOrDefault(y)?.ElementAtOrDefault(x - 1);

            var isLow = (!neighbour1.HasValue || currVal < neighbour1) &&
                   (!neighbour2.HasValue || currVal < neighbour2) &&
                   (!neighbour3.HasValue || currVal < neighbour3) &&
                   (!neighbour4.HasValue || currVal < neighbour4);

            return isLow;
        }


        private static int GetBasinSize(int?[][] intData, List<Coordinate> visitedNodes, int x, int y)
        {
            if (visitedNodes.Any(xy => xy.Y == y && xy.X == x) || intData[y][x].Value == 9)
            {
                return 0;
            }
            visitedNodes.Add(new Coordinate(x, y));

            var currVal = intData[y][x].Value;

            // fix neighbour being previous x y
            var neighbour1 = intData.ElementAtOrDefault(y + 1)?.ElementAtOrDefault(x) ?? int.MaxValue;
            var neighbour2 = intData.ElementAtOrDefault(y - 1)?.ElementAtOrDefault(x) ?? int.MaxValue;
            var neighbour3 = intData.ElementAtOrDefault(y)?.ElementAtOrDefault(x + 1) ?? int.MaxValue;
            var neighbour4 = intData.ElementAtOrDefault(y)?.ElementAtOrDefault(x - 1) ?? int.MaxValue;
            var sum = 0;

            if (neighbour1 - currVal == 1)
            {
                sum += GetBasinSize(intData, visitedNodes, y + 1, x);
            }

            if (neighbour2 - currVal == 1)
            {
                sum += GetBasinSize(intData, visitedNodes, y - 1, x);
            }

            if (neighbour3 - currVal == 1)
            {
                sum += GetBasinSize(intData, visitedNodes, y, x + 1);
            }

            if (neighbour4 - currVal == 1)
            {
                sum += GetBasinSize(intData, visitedNodes, y, x - 1);
            }

            return 1 + sum;
        }
    }
}