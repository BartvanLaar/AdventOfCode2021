using System.Diagnostics;

namespace Day7
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
            IEnumerable<int> intData = stringData.SelectMany(s => s.Split(",").Select(i => int.Parse(i)));
            sw.Start();

            var results = intData.GroupBy(i => i).Select(i => (Value: i.First(), Count: i.Count())).OrderBy(i => i.Value).ToArray();
            var currentFuelCount = int.MaxValue;
            foreach (var res in results)
            {
                var newFuelCount = results.Sum(r => Math.Abs(r.Value - res.Value) * r.Count);
                if (newFuelCount < currentFuelCount)
                {
                    currentFuelCount = newFuelCount;
                }
            }

            sw.Stop();
            Console.WriteLine($"Answer is {currentFuelCount}");
            Console.WriteLine($"Took {sw.ElapsedTicks / (TimeSpan.TicksPerMillisecond / 1000)} μs after reading in the start data.");
        }

        private static void ChallengeTwo()
        {
            var sw = new Stopwatch();
            const string INPUT_FILE_NAME = "InputDataChallenge2.txt";
            var inputData = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), INPUT_FILE_NAME));
            IEnumerable<string> stringData = inputData.Split(Environment.NewLine, StringSplitOptions.TrimEntries);
            IEnumerable<int> intData = stringData.SelectMany(s => s.Split(",").Select(i => int.Parse(i)));
            sw.Start();

            var results = intData.GroupBy(i => i).Select(i => (Value: i.First(), Count: i.Count())).OrderBy(i => i.Value).ToArray();
            var currentFuelCount = int.MaxValue;
            foreach (var res in results)
            {
                var newFuelCount = results.Sum(r =>
                {
                    var value = Math.Abs(r.Value - res.Value);
                    return FakeFactorial(value) * r.Count;
                });
                if (newFuelCount < currentFuelCount)
                {
                    currentFuelCount = newFuelCount;
                }
            }

            sw.Stop();
            Console.WriteLine($"Answer is {currentFuelCount}");
            Console.WriteLine($"Took {sw.ElapsedTicks / (TimeSpan.TicksPerMillisecond / 1000)} μs after reading in the start data.");
        }

        public static int FakeFactorial(int n)
        {
            return n == 0 ? 0 : n + FakeFactorial(n - 1);
        }
    }
}