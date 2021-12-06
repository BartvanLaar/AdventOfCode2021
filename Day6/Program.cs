namespace Day6
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
            var fishes = stringData.SelectMany(n => n.Split(",").Select(n => int.Parse(n))).ToList();
            var ogFishCount = fishes.Count;
            var nrOfDays = 80;
            var map = Enumerable.Range(0, 9).Select(_ => 0L).ToList();

            // set initial data...
            foreach (var f in fishes)
            {
                map[f]++;
            }

            for (var i = 0; i < nrOfDays; i++)
            {
                var amountOfFishThatReset = map[0];
                var amountOfNewFish = amountOfFishThatReset;
                map.RemoveAt(0);
                map.Add(amountOfFishThatReset);
                map[6] += amountOfNewFish;
            }

            Console.WriteLine($"At the start there were {ogFishCount} lantern fishes!");
            Console.WriteLine($"After {nrOfDays} days there are a total of {map.Sum()} lantern fishes!");
        }

        private static void ChallengeTwo()
        {
            const string INPUT_FILE_NAME = "InputDataChallenge2.txt";
            var inputData = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), INPUT_FILE_NAME));
            IEnumerable<string> stringData = inputData.Split(Environment.NewLine, StringSplitOptions.TrimEntries);
            var fishes = stringData.SelectMany(n => n.Split(",").Select(n => int.Parse(n))).ToList();
            var ogFishCount = fishes.Count;
            var nrOfDays = 256;
            var map = Enumerable.Range(0, 9).Select(_ => 0L).ToList();

            // set initial data...
            foreach (var f in fishes)
            {
                map[f]++;
            }

            for (var i = 0; i < nrOfDays; i++)
            {
                var amountOfFishThatReset = map[0];
                var amountOfNewFish = amountOfFishThatReset;
                map.RemoveAt(0);
                map.Add(amountOfFishThatReset);
                map[6] += amountOfNewFish;
            }

            Console.WriteLine($"At the start there were {ogFishCount} lantern fishes!");
            Console.WriteLine($"After {nrOfDays} days there are a total of {map.Sum()} lantern fishes!");

        }
    }
}