namespace advent21
{
    internal class Day5
    {
        private const string testInput = @".\Day5\Day5TestInput.txt";
        private const string puzzleInput = @".\Day5\Day5PuzzleInput.txt";
        private const bool includeDiagonals = true;
        private struct Coord {
            public int x, y;
            public Coord(int[] vals)
            {
                x = vals[0];
                y = vals[1];
            }

            public override string ToString() => $"{x},{y}";            
        }

        private enum IncrementDirection
        {
            Backwards = -1,
            Stationary = 0,
            Forwards = 1,
        }
        internal void Run()
        {
            var inputLines = File.ReadAllLines(puzzleInput)
                .Where( x => !string.IsNullOrWhiteSpace(x) )
                .ToArray();

            Dictionary<Coord, int> diagram = new();

            foreach (var line in inputLines)
            {
                var groups = line.Split(" -> ");
                var startPositionGroup = groups[0].Split(',').Select(int.Parse).ToArray();
                var endPositionGroup = groups[1].Split(',').Select(int.Parse).ToArray();
                var startPosition = new Coord(startPositionGroup);
                var endPosition = new Coord(endPositionGroup);

                if (startPosition.x != endPosition.x 
                    && startPosition.y != endPosition.y
                    && !includeDiagonals)
                {
                    Console.WriteLine($"Skipping line, Diagonal between {startPosition} and {endPosition}");
                    continue;
                }
                Console.WriteLine($"Start: {startPosition}, End: {endPosition}");

                var xDirection = startPosition.x < endPosition.x
                                    ? IncrementDirection.Forwards
                                    : (startPosition.x > endPosition.x
                                        ? IncrementDirection.Backwards
                                        : IncrementDirection.Stationary
                                    );

                var yDirection = startPosition.y < endPosition.y
                    ? IncrementDirection.Forwards
                    : (startPosition.y > endPosition.y
                        ? IncrementDirection.Backwards
                        : IncrementDirection.Stationary
                    );

                var distance = Math.Max( // Use Absolute value to trim negative sign
                            Math.Abs(startPosition.x - endPosition.x),
                            Math.Abs(startPosition.y - endPosition.y)
                        );

                Coord currentCoord = startPosition;
                for (int i = 0; i < distance + 1; i++)
                {
                    diagram.TryGetValue(currentCoord, out var coordCount);
                    diagram[currentCoord] = coordCount + 1;

                    currentCoord = new Coord() { 
                        x = currentCoord.x + ((int)xDirection) ,
                        y = currentCoord.y + ((int)yDirection) ,
                    };
                }
            }

            var coordsCrossedMoreThanOnce = diagram.Count(x => x.Value > 1);

            Console.WriteLine("Coords crossed more than once "+coordsCrossedMoreThanOnce.ToString());
            Console.ReadKey();
        }
    }
}
