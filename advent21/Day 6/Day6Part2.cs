namespace advent21
{
    internal class Day6Part2
    {
        private const string testInput = @".\Day 6\Day6TestInput.txt";
        private const string puzzleInput = @".\Day 6\Day6PuzzleInput.txt";
        private const int DaysToRun = 256;
        internal void Run()
        {
            var input = File
                .ReadAllLines(puzzleInput)[0]
                .Split(',')
                .Select(int.Parse)
                .ToList();

            var spawnStates = new long[9]; // 8 day states, +1 for new spawn tracking
            foreach (var initialValue in input)
            {
                spawnStates[initialValue]++;
            }
            Console.WriteLine($"Initial State: {string.Join(", ", spawnStates)}");

            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            for (int i = 0; i < DaysToRun; i++)
            {
                var fishReadyToSpawn = spawnStates[0];
                for (int j = 1; j < spawnStates.Length; j++) // Skip 0 as processed out of loop
                {
                    spawnStates[j-1] = spawnStates[j];
                }

                spawnStates[8] = fishReadyToSpawn; // Add new fish for each existing fish spawning
                spawnStates[6] += fishReadyToSpawn; // Reset spawned fish back to 7 days
            }
            sw.Stop();
            Console.WriteLine($"Complete in {sw.Elapsed.TotalMilliseconds}");
            Console.WriteLine($"Number of Lantern Fish: {spawnStates.Sum()}");
            Console.ReadKey();
        }
    }
}
