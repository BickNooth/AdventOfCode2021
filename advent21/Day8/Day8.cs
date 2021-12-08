namespace advent21
{
    internal class Day8
    {
        private const string testInput = @".\Day8\Day8TestInput.txt";
        private const string puzzleInput = @".\Day8\Day8PuzzleInput.txt";

        internal void Run()
        {
            var inputLines = File.ReadAllLines(puzzleInput);

            Part1(inputLines);
            Console.ReadKey();
        }

        void Part1(string[] input)
        {
            var outputPatterns = input.Select(x => x.Split('|', StringSplitOptions.RemoveEmptyEntries).Last()).ToArray();
            //2 3 4 7 unique segments for 1 4 7 8
            var sum = outputPatterns
                        .Sum(outputPatterns => outputPatterns
                                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                .Count(output
                                        => output.Length == 2
                                        || output.Length == 3
                                        || output.Length == 4
                                        || output.Length == 7
                        ));
            Console.WriteLine(sum);
        }
    }
}
