namespace Day4
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
            var randomNumbers = stringData.First().Split(",").Select(n => int.Parse(n)).ToArray();
            var boards = new List<List<int[]>>();
            stringData = stringData.Skip(1);
            foreach (var data in stringData)
            {
                if (data == string.Empty)
                {
                    boards.Add(new List<int[]>());
                    continue;
                }
                boards.Last().Add(data.Split(" ").Where(t => t != string.Empty).Select(n => int.Parse(n)).ToArray());
            }
            var playedNumbers = new List<int>();
            List<int[]> winningBoard = null;
            foreach (var nr in randomNumbers)
            {
                playedNumbers.Add(nr);
                foreach (var board in boards)
                {
                    if (CheckIfBoardIsWinner(board, playedNumbers))
                    {
                        winningBoard = board;
                        break;
                    }
                }

                if (winningBoard != null)
                {
                    break;
                }
            }

            if (winningBoard == null)
            {
                Console.WriteLine("There was no winner! Why did you stop generating numbers, keep going!");
                return;
            }

            var lastPlayedNumber = playedNumbers.Last();
            var winningBoardUncheckedNumbersSum = winningBoard.SelectMany(n => n).Except(playedNumbers).Sum();

            Console.WriteLine($"Winning result of board game is {lastPlayedNumber * winningBoardUncheckedNumbersSum}.");


        }

        private static bool CheckIfBoardIsWinner(List<int[]> board, List<int> playedNumbers)
        {
            return CheckRows(board, playedNumbers) || CheckColumns(board, playedNumbers);
        }

        private static bool CheckColumns(List<int[]> board, List<int> playedNumbers)
        {
            var rowLength = board.First().Length;

            foreach (var index in Enumerable.Range(0, rowLength))
            {
                var colValues = board.Select(n => n[index]);
                if (!colValues.Except(playedNumbers).Any())
                {
                    return true;
                }
            }

            return false;
        }

        private static bool CheckRows(List<int[]> board, List<int> playedNumbers)
        {
            foreach (var row in board)
            {
                if (!row.Except(playedNumbers).Any())
                {
                    return true;
                }
            }

            return false;
        }

        private static void ChallengeTwo()
        {
            const string INPUT_FILE_NAME = "InputDataChallenge2.txt";
            var inputData = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), INPUT_FILE_NAME));
            IEnumerable<string> stringData = inputData.Split(Environment.NewLine, StringSplitOptions.TrimEntries);

            var randomNumbers = stringData.First().Split(",").Select(n => int.Parse(n)).ToArray();
            var boards = new List<List<int[]>>();
            stringData = stringData.Skip(1);
            foreach (var data in stringData)
            {
                if (data == string.Empty)
                {
                    boards.Add(new List<int[]>());
                    continue;
                }
                boards.Last().Add(data.Split(" ").Where(t => t != string.Empty).Select(n => int.Parse(n)).ToArray());
            }
            var playedNumbers = new List<int>();
            List<List<int[]>> winningBoards = new List<List<int[]>>();

            foreach (var nr in randomNumbers)
            {
                playedNumbers.Add(nr);
                var winningBoardsTemp = boards.Where(board => CheckIfBoardIsWinner(board, playedNumbers));

                foreach(var board in winningBoardsTemp)
                {
                    if (!winningBoards.Contains(board))
                    {
                        winningBoards.Add(board);
                    }
                }
             
                if (winningBoards.Count() == boards.Count())
                {
                    break;
                }
            }

            var lastPlayedNumber = playedNumbers.Last();
            var winningBoardUncheckedNumbersSum = winningBoards.Last().SelectMany(n => n).Except(playedNumbers).Sum();

            Console.WriteLine($"Winning result of board game is {lastPlayedNumber * winningBoardUncheckedNumbersSum}.");

        }

    }
}