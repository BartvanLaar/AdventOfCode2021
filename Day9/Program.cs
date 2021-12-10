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

            var lowPoints = FindLowPoints(intData);
            var sum = lowPoints.Sum(c => intData[c.Y][c.X]) + lowPoints.Count();

            sw.Stop();
            Console.WriteLine($"Sum of all low point values + 1 is {sum}");
            Console.WriteLine($"Took {sw.ElapsedMilliseconds} ms after reading in the start data.");
        }

        private static List<Coordinate> FindLowPoints(int?[][] data)
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
            var basins = new List<List<Coordinate>>();
            var lowPoints = FindLowPoints(intData);
            foreach(var point in lowPoints)
            {
                var basin = new List<Coordinate>();
                FillBasin(intData, basin, point);
                basins.Add(basin);
            }
          
            var result = basins.OrderByDescending(x => x.Count()).Take(topN).ToArray();
            var sum = 1;
            foreach (var r in result)
            {
                sum *= r.Count();
            }

            sw.Stop();
            Console.WriteLine($"Answer is {string.Join(", ", result.Select(r => r.Count()))} resulting in sum of {sum}");
            Console.WriteLine($"Took {sw.ElapsedMilliseconds} ms after reading in the start data.");
        }

        private static bool IsLowPoint(int?[][] data, Coordinate coordinate)
        {
            var x = coordinate.X;
            var y = coordinate.Y;
            var currVal = data[y][x];
            if (currVal == 9)
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


        private static void FillBasin(int?[][] intData, List<Coordinate> visitedNodes, Coordinate coord)
        {
            var x = coord.X;
            var y = coord.Y;
            if (visitedNodes.Any(xy => xy.Y == y && xy.X == x) || intData[y][x].Value == 9)
            {
                return;
            }
           
            visitedNodes.Add(coord);

            // fix neighbour being previous x y
            var neighbour1 = intData.ElementAtOrDefault(y + 1)?.ElementAtOrDefault(x);
            var neighbour2 = intData.ElementAtOrDefault(y - 1)?.ElementAtOrDefault(x); 
            var neighbour3 = intData.ElementAtOrDefault(y)?.ElementAtOrDefault(x + 1); 
            var neighbour4 = intData.ElementAtOrDefault(y)?.ElementAtOrDefault(x - 1); 
           
            
            if (neighbour1.HasValue)
            {
                FillBasin(intData, visitedNodes,new Coordinate(x, y + 1));
            }

            if (neighbour2.HasValue)
            {
                FillBasin(intData, visitedNodes, new Coordinate(x, y - 1));
            }

            if (neighbour3.HasValue)
            {
                FillBasin(intData, visitedNodes, new Coordinate(x + 1, y));
            }

            if (neighbour4.HasValue)
            {
                FillBasin(intData, visitedNodes, new Coordinate(x - 1, y));
            }
        }
    }
}