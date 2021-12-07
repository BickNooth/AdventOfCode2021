namespace advent21
{
    internal class Day7
    {
        private const string testInput = @".\Day7\Day7TestInput.txt";
        private const string puzzleInput = @".\Day7\Day7PuzzleInput.txt";

        internal void Run()
        {
            var inputLines = File.ReadAllLines(puzzleInput)[0]
                .Split(',')
                .Select(int.Parse)
                .ToArray();

            Part2(inputLines);
            Console.ReadKey();
        }

        void Part1(int[] crabs)
        {
            int optimumFuel = default;
            int optimumPosition = default;

            for (int currentPosition = crabs.Min(); 
                currentPosition < crabs.Max(); 
                currentPosition++)
            {
                int currentFuel = default;
                foreach (var crab in crabs)
                {
                    currentFuel += Math.Abs(crab - currentPosition);
                }
                Console.WriteLine($"Crabs need {currentFuel} to reach {currentPosition}");

                if (currentFuel < optimumFuel || optimumFuel == default) 
                {
                    optimumFuel = currentFuel;
                    optimumPosition = currentPosition;
                }
                if (currentFuel > optimumFuel) break;
            }

            Console.WriteLine($"Best position is {optimumPosition} which took {optimumFuel} fuel");
        }

        void Part2(int[] crabs)
        {
            int optimumFuel = default;
            int optimumPosition = default;

            for (int currentPosition = crabs.Min();
                currentPosition < crabs.Max();
                currentPosition++)
            {
                int currentFuel = default;
                foreach (var crab in crabs)
                {
                    var difference = Math.Abs(crab - currentPosition);
                    // from 2 to 5 = 6 --- 3 * (3+1) = 12 / 2 = 6
                    // from 1 to 5 = 10 --- 4 * (4+1) = 20 / 2 = 10
                    // from 14 to 5 = 45 --- 9 * (9+1) = 80 / 2 = 45
                    currentFuel += difference * (difference + 1) / 2;
                }
                Console.WriteLine($"Crabs need {currentFuel} fuel to reach {currentPosition}");

                if (currentFuel < optimumFuel || optimumFuel == default)
                {
                    optimumFuel = currentFuel;
                    optimumPosition = currentPosition;
                }
                if (currentFuel > optimumFuel) break; 
            }

            Console.WriteLine($"Best position is {optimumPosition} which took {optimumFuel} fuel");
        }
    }
}
