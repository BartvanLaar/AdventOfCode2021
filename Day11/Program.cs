using System.Diagnostics;

namespace Day11
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
            int[][] intData = stringData.Select(s => s.Select(n => n - ZERO).ToArray()).ToArray();
            var n = 100;

            var amountOfFlashes = 0;
            for (var i = 1; i <= n; i++)
            {
                amountOfFlashes += Simulate(intData);
            }

            sw.Stop();
            Console.WriteLine($"Amount of octopus flashes was {amountOfFlashes}.");
            Console.WriteLine($"Took {sw.ElapsedMilliseconds} ms after reading in the start data.");
        }

        private static void IncreaseEnergy(int[][] intData)
        {
            for (var y = 0; y < intData.Length; y++)
            {
                for (var x = 0; x < intData.Length; x++)
                {
                    intData[y][x]++;
                }
            }
        }
        private record Coordinate(int X, int Y);
        private static int Simulate(int[][] intData)
        {
            IncreaseEnergy(intData);
            Coordinate[] flashes = HandleFlashingOctopuses(intData);
            ResetFlashedOctopuses(flashes, intData);

            return flashes.Count();
        }

        private static Coordinate[] HandleFlashingOctopuses(int[][] intData)
        {
            var flashedOctopuses = new List<Coordinate>();
            for (var y = 0; y < intData.Length; y++)
            {
                for (var x = 0; x < intData.Length; x++)
                {
                    var coord = new Coordinate(x, y);
                    if (intData[y][x] > 9 && !flashedOctopuses.Any(x1 => x1.X == x && x1.Y == y))
                    {

                        // the code below does not work for the given requirements as the adjactent octopuses arent checked fater update if theyre eligible to flash or not..
                        // this code needs some recursive function to check neighbours of neighbours etc. until none update..

                        flashedOctopuses.Add(coord);
                        var neighboursToCheck = GetNeighbours(intData, x, y);
                        foreach(var neighbor in neighboursToCheck)
                        {
                            intData[neighbor.Y][neighbor.X]++;
                        }

                    }
                }
            }

            return flashedOctopuses.ToArray();
        }

        private static IEnumerable<Coordinate> GetNeighbours(int[][] intData, int x, int y)
        {

            if (y - 1 >= 0 && x - 1 >= 0)
            {
                yield return new Coordinate(x - 1, y - 1);
            }

            if (y - 1 >= 0)
            {
                yield return new Coordinate(x, y - 1);

            }

            if (y - 1 >= 0 && x + 1 < intData[y - 1].Length)
            {
                yield return new Coordinate(x + 1, y - 1);
            }

            if (x - 1 >= 0)
            {
                yield return new Coordinate(x - 1, y);
            }

            if (x + 1 < intData[y].Length)
            {
                yield return new Coordinate(x + 1, y);
            }

            if (y + 1 < intData.Length && x - 1 >= 0)
            {
                yield return new Coordinate(x - 1, y + 1);
            }

            if (y + 1 < intData.Length)
            {
                yield return new Coordinate(x , y + 1);
            }

            if (y + 1 < intData.Length && x + 1 < intData[y + 1].Length)
            {
                yield return new Coordinate(x + 1, y + 1);
            }
        }

        private static void ResetFlashedOctopuses(Coordinate[] flashes, int[][] intData)
        {
            foreach (var flash in flashes)
            {
                intData[flash.Y][flash.X] = 0;
            }
        }

        private static void ChallengeTwo()
        {
            var sw = new Stopwatch();
            const string INPUT_FILE_NAME = "InputDataChallenge2.txt";
            var inputData = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), INPUT_FILE_NAME));
            IEnumerable<string> stringData = inputData.Split(Environment.NewLine, StringSplitOptions.TrimEntries);
            sw.Start();


            sw.Stop();
            Console.WriteLine($"Took {sw.ElapsedMilliseconds} ms after reading in the start data.");
        }
    }
}