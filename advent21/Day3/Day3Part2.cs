using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent21
{
    internal class Day3Part2
    {
        enum LifeSupport
        {
            Oxygen,
            Carbon
        }

        private const string TestInput = @".\Day3\Day3Test.txt";
        private const string PuzzleInput = @".\Day3\Day3Puzzle.txt";

        internal void RunP2()
        {
            #region Local Functions
            char[][] ReduceBinary(char[][] binaryArray, int charIndex, int binaryNumberLength, LifeSupport lifeSupport)
            {
                bool keepOnes =
                    lifeSupport == LifeSupport.Oxygen
                        ? MostCommonValue(binaryArray.Select(x => x[charIndex])) == 1
                        : LeastCommonValue(binaryArray.Select(x => x[charIndex])) == 0;
                if (keepOnes)
                {
                    return binaryArray.Where(intArray => intArray[charIndex] == '1').ToArray();
                }
                else
                {
                    return binaryArray.Where(intArray => intArray[charIndex] == '0').ToArray();
                }
            }

            static int MostCommonValue(IEnumerable<char> enumerable)
            {
                _ = enumerable.TryGetNonEnumeratedCount(out int enumerableLength);

                var totalBit = enumerable.Sum(e => int.Parse(e.ToString()));
                decimal average = ((decimal)totalBit / enumerableLength);

                return average >= 0.5m ? 1 : 0;
            }

            static int LeastCommonValue(IEnumerable<char> enumerable)
            {
                _ = enumerable.TryGetNonEnumeratedCount(out int enumerableLength);

                var totalBit = enumerable.Sum(e => int.Parse(e.ToString()));
                decimal average = ((decimal)totalBit / enumerableLength);

                return average < 0.5m ? 0 : 1;
            }
            #endregion

            var binaryCharArrays = File.ReadAllLines(PuzzleInput).Select(binary => binary.ToCharArray()).ToArray();
            _ = binaryCharArrays.TryGetNonEnumeratedCount(out int countOfBinaryCharArrays);
            _ = binaryCharArrays[0].TryGetNonEnumeratedCount(out int binaryNumberLength);

            var oxygenBinary = new char[binaryNumberLength];
            var carbonBinary = new char[binaryNumberLength];

            char[][] binary = binaryCharArrays;
            for (int i = 0; i < binaryNumberLength + 1; i++)
            {
                if(binary.TryGetNonEnumeratedCount(out var curCount) && curCount == 1)
                {
                    oxygenBinary = binary[0];
                    break;
                }
                binary = ReduceBinary(binary, i, binaryNumberLength, LifeSupport.Oxygen);
            }

            binary = binaryCharArrays;
            for (int i = 0; i < binaryNumberLength + 1; i++)
            {
                if (binary.TryGetNonEnumeratedCount(out var curCount) && curCount == 1)
                {
                    carbonBinary = binary[0];
                    break;
                }
                binary = ReduceBinary(binary, i, binaryNumberLength, LifeSupport.Carbon);
            }

            

            var oxygenNumber = Convert.ToInt32(String.Join(String.Empty, oxygenBinary), 2);
            var carbonNumber = Convert.ToInt32(String.Join(String.Empty, carbonBinary), 2);

            Console.WriteLine($"Life Support Rating: {oxygenNumber * carbonNumber}");
            Console.ReadKey();
        }
    }
}
