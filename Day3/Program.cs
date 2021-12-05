namespace Day2
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
            var stringData = inputData.Split(Environment.NewLine, StringSplitOptions.TrimEntries);
            var maxLength = stringData.Max(x => x.Length);
            var data = stringData.Select(x => Convert.ToInt32(x, 2)).ToArray();

            var gammaRateAsArrayOfBits = Enumerable.Range(0, maxLength)
                .Reverse()
                .Select(index => data
                    .Select(n => (n >> index) & 1)
                    .GroupBy(x => x)
                    .Select(y => (Bit: y.First(), Count: y.Count()))
                    .MaxBy(z => z.Count))
                .Select(x => x.Bit);
            var gammaRate = Convert.ToInt32(string.Join(string.Empty, gammaRateAsArrayOfBits), 2);
            var mask = (1 << maxLength) - 1; // set mask to be able to get N required bits.... n= maxLength
            var epsilonRate = ~gammaRate & mask; // mask int to get N required bits 
            var powerConsumption = gammaRate * epsilonRate;

            Console.WriteLine($"Power consumption of submarine is {powerConsumption}");

        }

        private static void ChallengeTwo()
        {
            const string INPUT_FILE_NAME = "InputDataChallenge2.txt";
            var inputData = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), INPUT_FILE_NAME));
            var stringData = inputData.Split(Environment.NewLine, StringSplitOptions.TrimEntries);
            var maxLength = stringData.Max(x => x.Length);
            IEnumerable<int> oxygenRatingData = stringData.Select(x => Convert.ToInt32(x, 2)).ToArray();
            IEnumerable<int> co2ScrubberRatingData = stringData.Select(x => Convert.ToInt32(x, 2)).ToArray();

            foreach (var index in Enumerable.Range(0, maxLength).Reverse())
            {
                var temp = oxygenRatingData.Select(n => (n >> index) & 1).GroupBy(x => x).Select(y => (Bit: y.First(), Count: y.Count()));

                if (temp.Select(x => x.Count).Distinct().Count() == 1)
                {
                    oxygenRatingData = oxygenRatingData.Where(n => ((n >> index) & 1) == 0b1);
                }
                else
                {
                    var selectionFilter = temp.MaxBy(x => x.Count).Bit;
                    oxygenRatingData = oxygenRatingData.Where(n => ((n >> index) & 1) == selectionFilter);
                }

                if (oxygenRatingData.Count() < 2)
                {
                    break;
                }
            }

            var oxygenGeneratorRating = oxygenRatingData.First();

            foreach (var index in Enumerable.Range(0, maxLength).Reverse())
            {
                var temp = co2ScrubberRatingData.Select(n => (n >> index) & 1).GroupBy(x => x).Select(y => (Bit: y.First(), Count: y.Count()));

                if (temp.Select(x => x.Count).Distinct().Count() == 1)
                {
                    co2ScrubberRatingData = co2ScrubberRatingData.Where(n => ((n >> index) & 1) == 0b0);
                }
                else
                {
                    var selectionFilter = temp.MinBy(x => x.Count).Bit;
                    co2ScrubberRatingData = co2ScrubberRatingData.Where(n => ((n >> index) & 1) == selectionFilter);
                }

                if (co2ScrubberRatingData.Count() < 2)
                {
                    break;
                }
            }

            var co2ScrubberRating = co2ScrubberRatingData.First();

            Console.WriteLine($"The life support rating of the submarine is {oxygenGeneratorRating * co2ScrubberRating}");
        }

    }
}