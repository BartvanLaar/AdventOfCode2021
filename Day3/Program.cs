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
            var data = inputData.Split(Environment.NewLine, StringSplitOptions.TrimEntries);

            var gammaRateAsString = string.Empty;
            var epsilonRateAsString = string.Empty;
            for (var i = 0; i < data.Max(x => x.Length); i++)
            {
                var maxBitOccurrence = data.Select(x => x[i]).GroupBy(x => x).Select(x => (Bit: x.First(), Count: x.Count())).MaxBy(x => x.Count);
                gammaRateAsString += maxBitOccurrence.Bit;
                epsilonRateAsString += maxBitOccurrence.Bit == '1' ? '0' : '1';
            }
            var gammaRate = Convert.ToInt32(gammaRateAsString, 2);
            var epsilonRate = Convert.ToInt32(epsilonRateAsString, 2);

            var powerConsumption = gammaRate * epsilonRate;

            Console.WriteLine($"Power consumption of submarine is {powerConsumption}");

        }

        private static void ChallengeTwo()
        {
          
        }


    }
}