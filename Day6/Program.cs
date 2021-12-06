﻿namespace Day6
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
                var amountOfNewFish = amountOfFishesAtCertainAgeInDaysMapping[FISH_REBIRTH_INDEX];
                var amountOfFishThatReincarnate = amountOfNewFish;
                amountOfFishesAtCertainAgeInDaysMapping.RemoveAt(FISH_REBIRTH_INDEX);
                amountOfFishesAtCertainAgeInDaysMapping[FISH_REINCARNATION_INDEX] += amountOfFishThatReincarnate;
                amountOfFishesAtCertainAgeInDaysMapping.Add(amountOfFishThatReincarnate);
            }

            Console.WriteLine($"At the start there were {fishes.Count} lantern fishes!");
            Console.WriteLine($"After {nrOfDays} days there are a total of {amountOfFishesAtCertainAgeInDaysMapping.Sum()} lantern fishes!");
        }

        private static void ChallengeTwo()
        {
            const string INPUT_FILE_NAME = "InputDataChallenge2.txt";
            var inputData = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), INPUT_FILE_NAME));
            IEnumerable<string> stringData = inputData.Split(Environment.NewLine, StringSplitOptions.TrimEntries);
            var fishes = stringData.SelectMany(n => n.Split(",").Select(n => int.Parse(n))).ToList();

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
                var amountOfNewFish = amountOfFishesAtCertainAgeInDaysMapping[FISH_REBIRTH_INDEX];
                var amountOfFishThatReincarnate = amountOfNewFish;
                amountOfFishesAtCertainAgeInDaysMapping.RemoveAt(FISH_REBIRTH_INDEX);
                amountOfFishesAtCertainAgeInDaysMapping[FISH_REINCARNATION_INDEX] += amountOfFishThatReincarnate;
                amountOfFishesAtCertainAgeInDaysMapping.Add(amountOfFishThatReincarnate);
            }

            Console.WriteLine($"At the start there were {fishes.Count} lantern fishes!");
            Console.WriteLine($"After {nrOfDays} days there are a total of {amountOfFishesAtCertainAgeInDaysMapping.Sum()} lantern fishes!");

        }
    }
}