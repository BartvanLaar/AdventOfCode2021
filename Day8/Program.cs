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
            Console.WriteLine($"Result is {outputData}");
            Console.WriteLine($"Took {sw.ElapsedMilliseconds} ms after reading in the start data.");
        }

        private static int Decode(string[] input, string[] outputToDecode)
        {
            const int ONE_CHAR_LENGTH = 2;
            const int TWO_CHAR_LENGTH = 5;
            const int FOUR_CHAR_LENGTH = 4;
            const int THREE_CHAR_LENGTH = 5;
            const int FIVE_CHAR_LENGTH = 5;
            const int SIX_CHAR_LENGTH = 6;
            const int SEVEN_CHAR_LENGTH = 3;
            const int EIGHT_CHAR_LENGTH = 7;
            const int NINE_CHAR_LENGTH = 6;
            const int ZERO_CHAR_LENGTH = 6;

            // refactor so this doesnt use other numbers unless absolutely necessary

            var one = input.First(s => s.Length == ONE_CHAR_LENGTH);
            var four = input.First(s => s.Length == FOUR_CHAR_LENGTH);
            var seven = input.First(s => s.Length == SEVEN_CHAR_LENGTH);
            var eight = input.First(s => s.Length == EIGHT_CHAR_LENGTH);
            
            var topRow = new string(seven.Except(one).ToArray());
            var bottomRowAndBottomLeft = new string(eight.Except(seven).Except(four).ToArray());
            var middleRowAndTopLeft = new string(four.Except(one).ToArray());

            var five = input.First(s => s.Except(four).Count() == 2 && s.Length == FIVE_CHAR_LENGTH);
            var nine = new string(five.Concat(one).Distinct().ToArray());
            var leftUnder = new string(eight.Except(nine).ToArray());
            var six = new string(five.Concat(leftUnder).ToArray());
            var bottomRow = new string(eight.Except(leftUnder).ToArray());
            var three = new string()
            var zero = input.First(s => s.Except(six).Any() && s.Except(three).Count() == 2 && s.Length == ZERO_CHAR_LENGTH);
            var two = input.First(s => s.Except(five).Any() && !s.Concat(four).Distinct().Except(eight).Any() && s.Length == TWO_CHAR_LENGTH);

            var mappings = new (string Mapping, int Value)[] { (zero, 0), (one, 1), (two, 2), (three, 3), (four, 4), (five, 5), (six, 6), (seven, 7), (eight, 8), (nine, 9) };
            var result = string.Empty;
            foreach (var o in outputToDecode)
            {
                foreach (var mapping in mappings)
                {
                    if (!o.Except(mapping.Mapping).Any())
                    {
                        result += mapping.Value;
                        break;
                    }
                }
            }
            return int.Parse(result);

        }

        private static string SortString(string input)
        {
            char[] characters = input.ToArray();
            Array.Sort(characters);
            return new string(characters);
        }
    }
}