namespace advent21
{
    internal class Day6
    {
        private const string testInput = @".\Day 6\Day6TestInput.txt";
        private const string puzzleInput = @".\Day 6\Day6PuzzleInput.txt";
        private const int DaysToRun = 80;
        internal void Run()
        {
            var input = File
                .ReadAllLines(puzzleInput)[0]
                .Split(',')
                .Select(int.Parse)
                .ToList();

            List<LanternFish> spawnStates = input
                .Select(days => 
                    new LanternFish(days))
                .ToList();

            Console.WriteLine($"Initial State: {string.Join(", ", spawnStates)}");

            for (int i = 0; i < DaysToRun; i++)
            {
                List<LanternFish> newSpawnStates = new();
                foreach (var lanternFish in spawnStates)
                {
                    var newFish = lanternFish.DayPasses();
                    if (newFish != null) newSpawnStates.Add(newFish);
                }

                spawnStates.AddRange(newSpawnStates);
                Console.WriteLine($"After {i + 1} Day{(i == 0 ? "" : "s")}: {string.Join(",", spawnStates)}");
            }

            Console.WriteLine($"Number of Lantern Fish: {spawnStates.Count()}");
            Console.ReadKey();
        }

        internal class LanternFish 
        {
            public const int SpawnRate = 6; // Zero Indexed
            public const int NewSpawnDelay = 2;

            public LanternFish()
            {
                DaysLeftTilSpawn = SpawnRate + NewSpawnDelay;
            }

            public LanternFish(int currentDaysTilSpawn) => DaysLeftTilSpawn = currentDaysTilSpawn;

            public int DaysLeftTilSpawn { get; private set; }
            public LanternFish? DayPasses()
            {
                if (DaysLeftTilSpawn == 0)
                {
                    DaysLeftTilSpawn = SpawnRate;
                    return new LanternFish();
                }
                DaysLeftTilSpawn--;
                return null;
            }
            public override string ToString()
            {
                return DaysLeftTilSpawn.ToString();
            }
        }
    }
}
