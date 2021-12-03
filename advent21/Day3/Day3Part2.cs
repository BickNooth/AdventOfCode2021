namespace advent21
{
    internal class Day3Part2
    {
        private interface ILifeSupport
        {
            int LifeSupportValue { get; }
            IEnumerable<char> LifeSupportValueBinary { set; }
            int GetAverage(decimal numberToRound);
            int GetNumber(IEnumerable<char> binary);
        }

        private class OxygenLifeSupport : ILifeSupport
        {
            public int LifeSupportValue => GetNumber(LifeSupportValueBinary);
            public IEnumerable<char> LifeSupportValueBinary { private get; set; }
            public int GetAverage(decimal numberToRound) => numberToRound >= 0.5m ? 1 : 0;
            public int GetNumber(IEnumerable<char> binary) => Convert.ToInt32(string.Join(string.Empty, binary), 2);
        }

        private class CarbonLifeSupport : ILifeSupport
        {
            public int LifeSupportValue => GetNumber(LifeSupportValueBinary);
            public IEnumerable<char> LifeSupportValueBinary { private get; set; }
            public int GetAverage(decimal numberToRound) => numberToRound < 0.5m ? 0 : 1;
            public int GetNumber(IEnumerable<char> binary) => Convert.ToInt32(string.Join(string.Empty, binary), 2);
        }

        private const string TestInput = @".\Day3\Day3Test.txt";
        private const string PuzzleInput = @".\Day3\Day3Puzzle.txt";

        internal void Run()
        {
            var binaryCharArrays = File.ReadAllLines(PuzzleInput).Select(binary => binary.ToCharArray()).ToArray();
            _ = binaryCharArrays[0].TryGetNonEnumeratedCount(out int binaryNumberLength);
            
            var oxygenLifeSupport = new OxygenLifeSupport();
            var carbonLifeSupport = new CarbonLifeSupport();

            CalculateLifeSupport(binaryNumberLength, binaryCharArrays, oxygenLifeSupport);
            CalculateLifeSupport(binaryNumberLength, binaryCharArrays, carbonLifeSupport);

            Console.WriteLine($"Life Support Rating: {oxygenLifeSupport.LifeSupportValue * carbonLifeSupport.LifeSupportValue}");
            Console.ReadKey();
        }

        private static void CalculateLifeSupport(int binaryNumberLength, char[][] binary, ILifeSupport lifeSupport)
        {
            for (var i = 0; i < binaryNumberLength + 1; i++)
            {
                if (binary.TryGetNonEnumeratedCount(out var curCount) && curCount == 1)
                {
                    lifeSupport.LifeSupportValueBinary = binary[0];
                    break;
                }

                binary = ReduceBinary(binary, i, lifeSupport);
            }
        }

        private static int CommonValue(IEnumerable<char> enumerable, ILifeSupport lifeSupport)
        {
            _ = enumerable.TryGetNonEnumeratedCount(out var enumerableLength);

            var totalBit = enumerable.Sum(e => int.Parse(e.ToString()));
            var totalDecimalAverage = ((decimal)totalBit / enumerableLength);
                
            return lifeSupport.GetAverage(totalDecimalAverage);
        }

        private static char[][] ReduceBinary(char[][] binaryArray, int charIndex, ILifeSupport lifeSupport)
        {
            var keepOnes =
                lifeSupport.GetType() == typeof(OxygenLifeSupport)
                    ? CommonValue(binaryArray.Select(x => x[charIndex]), lifeSupport) == 1
                    : CommonValue(binaryArray.Select(x => x[charIndex]), lifeSupport) == 0;
            return keepOnes 
                ? binaryArray.Where(intArray => intArray[charIndex] == '1').ToArray() 
                : binaryArray.Where(intArray => intArray[charIndex] == '0').ToArray();
        }
    }
}
