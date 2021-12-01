namespace Day1
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

            Console.WriteLine("How many measurements are larger than the previous measurement?");
            var result = data.Skip(1).Where((d, i) => int.Parse(d) > int.Parse(data[i])).Count();
            Console.WriteLine($"Answer is: {result}.");
        }

        private static void ChallengeTwo()
        {

        }


    }
}