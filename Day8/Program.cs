using System.Diagnostics;

namespace Day8
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
            sw.Start();
            var outputData = stringData.SelectMany(s => s.Split(" | ").Last().Split(" ")).GroupBy(i => i.Length).ToDictionary(k => k.Key, v => v.Count());

            var result = outputData[2] + outputData[4] + outputData[3] + outputData[7];
            sw.Stop();
            Console.WriteLine($"Number of unique numbers is {result} ");
            Console.WriteLine($"Took {sw.ElapsedMilliseconds} ms after reading in the start data.");
        }

        private static void ChallengeTwo()
        {
            var sw = new Stopwatch();
            const string INPUT_FILE_NAME = "InputDataChallenge2.txt";
            var inputData = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), INPUT_FILE_NAME));
            IEnumerable<string> stringData = inputData.Split(Environment.NewLine, StringSplitOptions.TrimEntries);
            sw.Start();
            var outputData = stringData.Sum(s =>
            {
                var split = s.Split(" | ");
                return Decode(split[0].Split(" "), split[1].Split(" "));
            });

            sw.Stop();
            Console.WriteLine($"Took {sw.ElapsedMilliseconds} ms after reading in the start data.");
        }

        private static int Decode(string[] input, string[] outputToDecode)
        {
            const int ONE_CHAR_LENGTH = 2;
            const int FOUR_CHAR_LENGTH = 4;
            const int FIVE_CHAR_LENGTH = 5;
            const int SIX_CHAR_LENGTH = 6;
            const int SEVEN_CHAR_LENGTH = 3;
            const int EIGHT_CHAR_LENGTH = 7;

            var oneEncodedAsString = input.First(s => s.Length == ONE_CHAR_LENGTH);
            var sevenEncodedAsString = input.First(s => s.Length == SEVEN_CHAR_LENGTH);
            var fourEncodedAsString = input.First(s => s.Length == FOUR_CHAR_LENGTH);
            var eightEncodedAsString = input.First(s => s.Length == EIGHT_CHAR_LENGTH);

            var topRowOfDisplay = new string(sevenEncodedAsString.Except(oneEncodedAsString).ToArray());
            var middleRowAndTopLeftOfDisplay = new string(fourEncodedAsString.Except(oneEncodedAsString).ToArray());

            // contains doesnt work in this case.. need to check for individual characters.. perhaps use .except?
            var five = input.First(s => !s.Contains(fourEncodedAsString) && s.Length == FIVE_CHAR_LENGTH);
            var six = input.First(s => !s.Contains(fourEncodedAsString) && s.Length == SIX_CHAR_LENGTH);

            return 0;

        }

        private static string SortString(string input)
        {
            char[] characters = input.ToArray();
            Array.Sort(characters);
            return new string(characters);
        }
    }
}