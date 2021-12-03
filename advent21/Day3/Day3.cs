using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent21
{
    internal class Day3
    {
        private const string TestInput = @".\Day3\Day3Test.txt";
        private const string PuzzleInput = @".\Day3\Day3Puzzle.txt";

        internal void Run()
        {
            var binaryCharArrays = File.ReadAllLines(PuzzleInput).Select(binary => binary.ToCharArray()).ToArray();

            _ = binaryCharArrays.TryGetNonEnumeratedCount(out int countOfBinaryCharArrays);
            _ = binaryCharArrays[0].TryGetNonEnumeratedCount(out int binaryNumberLength);

            var gammaBinary = new int[binaryNumberLength];
            var epsilonBinary = new int[binaryNumberLength];

            for (int i = 0; i < binaryNumberLength; i++)
            {
                var totalBit = binaryCharArrays.Sum(
                            binary => int.Parse(binary[i].ToString())
                    );

                decimal gammaAverage = ((decimal)totalBit / countOfBinaryCharArrays);
                gammaBinary[i] = gammaAverage > 0.5m ? 1 : 0;
            }

            for (int i = 0; i < gammaBinary.Length; i++)
            {
                epsilonBinary[i] = gammaBinary[i] == 1 ? 0 : 1;
            }

            var gammaNumber = Convert.ToInt32(String.Join(String.Empty, gammaBinary), 2);
            var epsilonNumber = Convert.ToInt32(String.Join(String.Empty, epsilonBinary), 2);

            Console.WriteLine($"Power Consumption: {gammaNumber * epsilonNumber}");
            Console.ReadKey();
        }
    }
}
