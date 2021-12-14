using System.Collections.Concurrent;
using System.Diagnostics;

namespace Day12
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

            var nodeRegister = new ConcurrentDictionary<string, List<string>>();
            foreach (var line in stringData)
            {
                var lineSplit = line.Split("-");
                var f = lineSplit.First();
                var l = lineSplit.Last();

                nodeRegister.AddOrUpdate(f, new List<string>() { l }, (k, v) => { v.Add(l); return v; });
                nodeRegister.AddOrUpdate(l, new List<string>() { f }, (k, v) => { v.Add(f); return v; });
            }
            Console.WriteLine($"Amount of nodes in network is {nodeRegister.Count()}.");
            var result = CalculatePossibleRoutesChallenge1(nodeRegister, new List<string>(), "start", "end");

            Console.WriteLine($"Amount of unique paths through the network is {result}");
            sw.Stop();
            Console.WriteLine($"Took {sw.ElapsedMilliseconds} ms after reading in the start data.");
        }

        private static int CalculatePossibleRoutesChallenge1(IDictionary<string, List<string>> nodeRegister, List<string> currentPath, string start, string end)
        {
            if (nodeRegister[start] == nodeRegister[end])
            {
                throw new Exception();
            }

            currentPath.Add(start);

            var validNodes = nodeRegister[start].Where(n => n.Any(c => !char.IsLower(c)) || !currentPath.Contains(n)).ToArray();

            var sum = 0;
            foreach (var node in validNodes)
            {
                if (node == end)
                {
                    sum++;
                }
                else
                {
                    sum += CalculatePossibleRoutesChallenge1(nodeRegister, currentPath.ToList(), node, end);
                }
            }

            return sum;
        }

        private static int CalculatePossibleRoutesChallenge2(IDictionary<string, List<string>> nodeRegister, List<string> currentPath, string start, string end)
        {
            if (nodeRegister[start] == nodeRegister[end])
            {
                throw new Exception();
            }

            currentPath.Add(start);

            var validNodes = nodeRegister[start].Where(n =>
            {
                var isStartNode = n == "start";
                var isEndNode = n == "end";
                if (isStartNode || isEndNode)
                {
                    return !currentPath.Contains(n);
                }
                var isLower = n.All(c => char.IsLower(c));
                if (!isLower)
                {
                    return true;
                }
                var isMaxCountReached = currentPath.Where(nn => nn.All(c => char.IsLower(c))).GroupBy(x => x).Any(y => y.Key != "start" && y.Key != "end" && y.Count() >= 2);
                return !isMaxCountReached || !currentPath.Contains(n);

            }).ToArray();

            var sum = 0;
            foreach (var node in validNodes)
            {
                if (node == end)
                {
                    sum++;
                }
                else
                {
                    sum += CalculatePossibleRoutesChallenge2(nodeRegister, currentPath.ToList(), node, end);
                }
            }

            return sum;
        }

        private static void ChallengeTwo()
        {
            var sw = new Stopwatch();
            const string INPUT_FILE_NAME = "InputDataChallenge2.txt";
            var inputData = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), INPUT_FILE_NAME));
            IEnumerable<string> stringData = inputData.Split(Environment.NewLine, StringSplitOptions.TrimEntries);
            sw.Start();

            var nodeRegister = new ConcurrentDictionary<string, List<string>>();
            foreach (var line in stringData)
            {
                var lineSplit = line.Split("-");
                var f = lineSplit.First();
                var l = lineSplit.Last();

                nodeRegister.AddOrUpdate(f, new List<string>() { l }, (k, v) => { v.Add(l); return v; });
                nodeRegister.AddOrUpdate(l, new List<string>() { f }, (k, v) => { v.Add(f); return v; });
            }
            Console.WriteLine($"Amount of nodes in network is {nodeRegister.Count()}.");
            var result = CalculatePossibleRoutesChallenge2(nodeRegister, new List<string>(), "start", "end");

            Console.WriteLine($"Amount of unique paths through the network is {result}");

            sw.Stop();
            Console.WriteLine($"Took {sw.ElapsedMilliseconds} ms after reading in the start data.");
        }
    }
}