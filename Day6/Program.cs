using System.Diagnostics;

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
            var fishes = stringData.SelectMany(n => n.Split(",").Select(n => int.Parse(n))).ToArray();

            var nrOfDays = 80;
            var amountOfFishesAtCertainAgeInDaysMapping = Enumerable.Range(0, 9).Select(_ => 0L).ToList();

            // set initial data...
            foreach (var f in fishes)
            {
                amountOfFishesAtCertainAgeInDaysMapping[f]++;
            }
            const int FISH_REBIRTH_INDEX = 0;
            const int FISH_REINCARNATION_INDEX = 6;
            for (var i = 0; i < nrOfDays; i++)
            {
                var amountOfFishThatReincarnate = amountOfFishesAtCertainAgeInDaysMapping[FISH_REBIRTH_INDEX];
                var amountOfNewFish = amountOfFishThatReincarnate;
                amountOfFishesAtCertainAgeInDaysMapping.RemoveAt(FISH_REBIRTH_INDEX);
                amountOfFishesAtCertainAgeInDaysMapping[FISH_REINCARNATION_INDEX] += amountOfFishThatReincarnate;
                amountOfFishesAtCertainAgeInDaysMapping.Add(amountOfNewFish);
            }

            Console.WriteLine($"At the start there were {fishes.Length} lantern fishes!");
            Console.WriteLine($"After {nrOfDays} days there are a total of {amountOfFishesAtCertainAgeInDaysMapping.Sum()} lantern fishes!");
        }

        private static void ChallengeTwo()
        {
            var sw = new Stopwatch();
            const string INPUT_FILE_NAME = "InputDataChallenge2.txt";
            var inputData = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), INPUT_FILE_NAME));
            IEnumerable<string> stringData = inputData.Split(Environment.NewLine, StringSplitOptions.TrimEntries);
            var fishes = stringData.SelectMany(n => n.Split(",").Select(n => int.Parse(n))).ToArray();
            sw.Start();

            var nrOfDays = 256;
            var amountOfFishesAtCertainAgeInDaysMapping = Enumerable.Range(0, 9).Select(_ => 0L).ToList();

            // set initial data...
            foreach (var f in fishes)
            {
                amountOfFishesAtCertainAgeInDaysMapping[f]++;
            }
            const int FISH_REBIRTH_INDEX = 0;
            const int FISH_REINCARNATION_INDEX = 6;
            for (var i = 0; i < nrOfDays; i++)
            {
                var amountOfFishThatReincarnate  = amountOfFishesAtCertainAgeInDaysMapping[FISH_REBIRTH_INDEX];
                var amountOfNewFish = amountOfFishThatReincarnate;
                amountOfFishesAtCertainAgeInDaysMapping.RemoveAt(FISH_REBIRTH_INDEX);
                amountOfFishesAtCertainAgeInDaysMapping[FISH_REINCARNATION_INDEX] += amountOfFishThatReincarnate;
                amountOfFishesAtCertainAgeInDaysMapping.Add(amountOfNewFish);
            }
            var sum = amountOfFishesAtCertainAgeInDaysMapping.Sum();
            sw.Stop();
            Console.WriteLine($"Took {sw.ElapsedTicks / (TimeSpan.TicksPerMillisecond / 1000)} μs after reading in the start data.");
            Console.WriteLine($"At the start there were {fishes.Length} lantern fishes!");
            Console.WriteLine($"After {nrOfDays} days there are a total of {sum} lantern fishes!");

        }
    }
}