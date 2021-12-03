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
            int depthPos = 0;
            int horizontalPos = 0;
            var processInputElement = ((string Action, int Weight) d) =>
            {
                switch (d.Action)
                {
                    case "forward"  : horizontalPos += d.Weight; break;
                    case "up"       : depthPos      -= d.Weight; break;
                    case "down"     : depthPos      += d.Weight; break;
                }
            };
            IEnumerable<(string Action, int Weight)> parsedData = data.Select(d => { var split = d.Split(" "); return (split[0], int.Parse(split[1])); });
            foreach (var d in parsedData)
            {
                processInputElement(d);
            }

            Console.WriteLine($"Position of submarine: Horizontal {horizontalPos} Depth {depthPos}");
            Console.WriteLine($"Answer to question is: {horizontalPos * depthPos}");
        }

        private static void ChallengeTwo()
        {
            const string INPUT_FILE_NAME = "InputDataChallenge2.txt";
            var inputData = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), INPUT_FILE_NAME));
            var data = inputData.Split(Environment.NewLine, StringSplitOptions.TrimEntries);
            int depthPos = 0;
            int horizontalPos = 0;
            int aim = 0;
            var processInputElement = ((string Action, int Weight) d) =>
            {
                switch (d.Action)
                {
                    case "forward":
                        {
                            horizontalPos += d.Weight;
                            depthPos += aim * d.Weight;
                            break;
                        }
                    case "up": aim -= d.Weight; break;
                    case "down": aim += d.Weight; break;
                }
            };

            IEnumerable<(string Action, int Weight)> parsedData = data.Select(d => { var split = d.Split(" "); return (split[0], int.Parse(split[1])); });
            foreach (var d in parsedData)
            {
                processInputElement(d);
            }

            Console.WriteLine($"Position of submarine: Horizontal {horizontalPos} Depth {depthPos}");
            Console.WriteLine($"Answer to question is: {horizontalPos * depthPos}");
        }


    }
}