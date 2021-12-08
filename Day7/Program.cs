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

            var results = intData.GroupBy(i => i).Select(i => (Value: i.First(), Count: i.Count()));
            var maxIndex = results.MaxBy(x => x.Value).Value;

            var currentFuelCount = int.MaxValue;
            for (var index = 0; index < maxIndex; index++)
            {
                var newFuelCount = results.Sum(r => Math.Abs(r.Value - index) * r.Count);
                currentFuelCount = newFuelCount < currentFuelCount ? newFuelCount : currentFuelCount;
            }

            sw.Stop();
            Console.WriteLine($"Answer is {currentFuelCount}");
            Console.WriteLine($"Took {sw.ElapsedMilliseconds} ms after reading in the start data.");
        }

        private static void ChallengeTwo()
        {
            var sw = new Stopwatch();
            const string INPUT_FILE_NAME = "InputDataChallenge2.txt";
            var inputData = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), INPUT_FILE_NAME));
            IEnumerable<string> stringData = inputData.Split(Environment.NewLine, StringSplitOptions.TrimEntries);
            IEnumerable<int> intData = stringData.SelectMany(s => s.Split(",").Select(i => int.Parse(i)));
            sw.Start();

            var results = intData.GroupBy(i => i).Select(i => (Value: i.First(), Count: i.Count()));
            var maxIndex = results.MaxBy(x => x.Value).Value;
            var currentFuelCount = int.MaxValue;

            for (var index = 0; index < maxIndex; index++)
            {
                var newFuelCount = results.Sum(r => FakeFactorial3(Math.Abs(r.Value - index)) * r.Count);
                currentFuelCount = newFuelCount < currentFuelCount ? newFuelCount : currentFuelCount;
            }

            sw.Stop();
            Console.WriteLine($"Answer is {currentFuelCount}");
            Console.WriteLine($"Took {sw.ElapsedMilliseconds} ms after reading in the start data.");
        }
        
        // Apparently this recursion method is slow...
        public static int FakeFactorial(int n)
        {
            return n == 0 ? 0 : n + FakeFactorial(n - 1);
        }
        
        // This is much faster!
        public static int FakeFactorial2(int n)
        {
            var sum = 0;
            for (var i = 0; i <= n; i++)
            {
                sum += i;
            }
            return sum;
        }

        public static int FakeFactorial3(int n)
        {
            return (n * (n + 1)) / 2;
        }


    }
}