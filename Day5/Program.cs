namespace Day5
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
            const string INPUT_FILE_NAME = "InputDataChallenge1.txt";
            var inputData = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), INPUT_FILE_NAME));
            IEnumerable<string> stringData = inputData.Split(Environment.NewLine, StringSplitOptions.TrimEntries);
            var coordinateRanges = stringData.Select(x => x.Split(" -> ").Select(z =>
            {
                var nrs = z.Split(",");
                return (X: int.Parse(nrs.First()), Y: int.Parse(nrs.Last()));
            }));

            var maxX = coordinateRanges.SelectMany(cr => cr.Select(r => r.X)).Max();
            var maxY = coordinateRanges.SelectMany(cr => cr.Select(r => r.Y)).Max();

            var diagram = CreateDiagram(maxX, maxY);

            foreach (var range in coordinateRanges.Where(coordinates => coordinates.First().X == coordinates.Last().X))
            {
                var yMin = range.Min(c => c.Y);
                var yMax = range.Max(c => c.Y);
                var x = range.First().X;
                for (var y = yMin; y <= yMax; y++)
                {
                    diagram[y][x]++;

                }

            }

            foreach (var range in coordinateRanges.Where(coordinates => coordinates.First().Y == coordinates.Last().Y))
            {
                var xMin = range.Min(c => c.X);
                var xMax = range.Max(c => c.X);
                var y = range.First().Y;
                for (var x = xMin; x <= xMax; x++)
                {
                    diagram[y][x]++;

                }
            }

            var result = diagram.SelectMany(x => x.Select(y => y)).Count(z => z >= 2);
            Console.WriteLine($"Amount of points where at least 2 lines overlap: {result}.");
        }

        private static void PrintDiagram(int[][] diagram)
        {
            foreach (var row in diagram)
            {
                Console.WriteLine(string.Join(" ", row));
            }
        }

        private static void PrintDiagramCoordinates(int[][] diagram)
        {
            foreach (var row in diagram)
            {
                Console.WriteLine(string.Join(" ", Enumerable.Range(0, row.Length)));
            }
        }

        private static int[][] CreateDiagram(int xZeroBased, int yZeroBased)
        {
            var diagram = new int[yZeroBased + 1][];
            for (int i = 0; i < diagram.Length; i++)
            {
                diagram[i] = Enumerable.Range(0, xZeroBased + 1).Select(_ => 0).ToArray();
            }

            return diagram;
        }

        private static void ChallengeTwo()
        {
            const string INPUT_FILE_NAME = "InputDataChallenge2.txt";
            var inputData = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), INPUT_FILE_NAME));
            IEnumerable<string> stringData = inputData.Split(Environment.NewLine, StringSplitOptions.TrimEntries);
            var coordinateRanges = stringData.Select(x => x.Split(" -> ").Select(z =>
            {
                var nrs = z.Split(",");
                return (X: int.Parse(nrs.First()), Y: int.Parse(nrs.Last()));
            }));
            var maxX = coordinateRanges.SelectMany(cr => cr.Select(r => r.X)).Max();
            var maxY = coordinateRanges.SelectMany(cr => cr.Select(r => r.Y)).Max();

            var diagram = CreateDiagram(maxX, maxY);

            var horizontalLines = coordinateRanges.Where(coordinates => coordinates.First().X == coordinates.Last().X);

            foreach (var range in horizontalLines)
            {
                var yMin = range.Min(c => c.Y);
                var yMax = range.Max(c => c.Y);
                var x = range.First().X;
                for (var y = yMin; y <= yMax; y++)
                {
                    diagram[y][x]++;
                }
            }

            var verticalLines = coordinateRanges.Where(coordinates => coordinates.First().Y == coordinates.Last().Y);
            foreach (var range in verticalLines)
            {
                var xMin = range.Min(c => c.X);
                var xMax = range.Max(c => c.X);
                var y = range.First().Y;
                for (var x = xMin; x <= xMax; x++)
                {
                    diagram[y][x]++;
                }
            }

            var diagonalLines = coordinateRanges.Where(coord => Math.Abs(coord.First().X - coord.Last().X) == Math.Abs(coord.First().Y - coord.Last().Y));
            foreach (var range in diagonalLines)
            {
                var yStart = range.First().Y;
                var yEnd = range.Last().Y;
                var xStart = range.First().X;
                var xEnd = range.Last().X;
                var ySign = Math.Sign(yEnd - yStart);
                var xSign = Math.Sign(xEnd - xStart);

                var yCount = yStart;
                var xCount = xStart;
                while (yCount != yEnd)
                {
                    diagram[yCount][xCount]++;

                    yCount += ySign * 1;
                    xCount += xSign * 1;
                }
                diagram[yCount][xCount]++;

            }

            PrintDiagram(diagram); // only use this with the puzzle sample size...
            var result = diagram.SelectMany(x => x.Select(y => y)).Count(z => z >= 2);
            Console.WriteLine($"Amount of points where at least 2 lines overlap: {result}.");
        }

    }
}