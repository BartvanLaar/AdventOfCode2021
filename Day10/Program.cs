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
                    ParseLine(line);
                }
                catch (ParseException ex)
                {
                    scoreCounters[ex.ActualValue]++;
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

        private static void ParseLine(string line)
        {
            var charStack = new Stack<char>();

            foreach (var character in line)
            {
                if (_openToCloseTagMap.ContainsKey(character))
                {
                    charStack.Push(character);
                    continue;
                }

                var closingChar = _openToCloseTagMap[charStack.Pop()];
                if(closingChar == character)
                {
                    continue;
                }
                throw new ParseException() { ActualValue = character, ExpectedValue = closingChar };
            }
        }

        private static char[] ParseLine2(string line)
        {
            var charStack = new Stack<char>();

            foreach (var character in line)
            {
                if (_openToCloseTagMap.ContainsKey(character))
                {
                    charStack.Push(character);
                    continue;
                }

                var closingChar = _openToCloseTagMap[charStack.Peek()];
                if (closingChar == character)
                {
                    charStack.Pop();
                    continue;
                }
            }

            return charStack.Select(c =>_openToCloseTagMap[c]).ToArray();
        }

        private class ParseException : Exception
        {
            public char ActualValue { get; set; }
            public char ExpectedValue { get; set; }
        }

        private static void ChallengeTwo()
        {
            var sw = new Stopwatch();
            const string INPUT_FILE_NAME = "InputDataChallenge2.txt";
            var inputData = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), INPUT_FILE_NAME));
            IEnumerable<string> stringData = inputData.Split(Environment.NewLine, StringSplitOptions.TrimEntries);
            sw.Start();
            var scoreMap = new Dictionary<char, int>()
            {
                [')'] = 1,
                [']'] = 2,
                ['}'] = 3,
                ['>'] = 4,
            };
            var validLines = stringData.Where(s => { try { ParseLine(s); return true; } catch (ParseException) { return false; } });
            var sums = new List<long>();
            foreach (var line in validLines)
            {
                var result = ParseLine2(line);
                if (!result.Any())
                {
                    continue;
                }

                long sum = 0;
                foreach(var res in result)
                {
                    sum *= 5;
                    sum += scoreMap[res];
                }

                sums.Add(sum);
            }
            var resultSum = sums.OrderBy(x => x).ElementAt((sums.Count - 1)  / 2);

            sw.Stop();
            Console.WriteLine($"Score of auto complete = {resultSum}");

            Console.WriteLine($"Took {sw.ElapsedMilliseconds} ms after reading in the start data.");
        }
    }
}