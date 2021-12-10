using System.Diagnostics;

namespace Day10
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
            var scoreMap = new Dictionary<char, int>()
            {
                [')'] = 3,
                [']'] = 57,
                ['}'] = 1197,
                ['>'] = 25137,
            };

            var scoreCounters = new Dictionary<char, int>()
            {
                [')'] = 0,
                [']'] = 0,
                ['}'] = 0,
                ['>'] = 0,
            };

            sw.Start();
            foreach (var line in stringData)
            {
                try
                {
                    ParseLine(line, 0);
                }
                catch (ParseException ex)
                {
                    scoreCounters[ex.Value]++;
                    continue;
                }
            }

            var sum = scoreCounters.Keys.Select(s => scoreCounters[s] * scoreMap[s]).Sum();

            sw.Stop();
            Console.WriteLine($"sum of syntax error score = {sum}");
            Console.WriteLine($"Took {sw.ElapsedMilliseconds} ms after reading in the start data.");
        }

        private static Dictionary<char, char> _openToCloseTagMap = new Dictionary<char, char>()
        {
            ['('] = ')',
            ['['] = ']',
            ['{'] = '}',
            ['<'] = '>',
        };

        private static int ParseLine(string line, int index)
        {
            if (index > line.Length - 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            var ogChar = line[index];
            var currentChar = line[index];
            if (_openToCloseTagMap.Values.Contains(currentChar))
            {
                return index;
            }

            var closingChar = _openToCloseTagMap[currentChar];
            while (currentChar != closingChar)
            {
                try
                {
                    index = ParseLine(line, ++index);
                    currentChar = line[index];
                    if(currentChar == ogChar)
                    {
                        return index;
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    throw new ParseException() { Value = _openToCloseTagMap[ogChar] };
                }
            }

            return index;
        }

        private class ParseException : Exception
        {
            public int Index { get; set; }
            public char Value { get; set; }
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