using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent21
{
    internal class Day4
    {
        private const string testInput = @".\Day4\Day4Test.txt";
        private const string puzzleInput = @".\Day4\Day4Puzzle.txt";
   
        private BingoBoard[]? BingoBoards { get; set; }

        internal void Run()
        {
            #region Parse input to Bingo Boards
            var input = File.ReadAllLines(testInput);

            var numbersToCall = input
                .First()
                .Split(',')
                .Where(num => !string.IsNullOrWhiteSpace(num))
                .Select(int.Parse)
                .ToArray();

            var bingoNumberCollection =
                    ParseInputToIntArrays(
                        input.Skip(2).ToArray()
                    );

            var bingoNumberBoardChunks =
                    bingoNumberCollection
                    .Chunk(BingoSettings.numberOfBingoRows)
                    .ToArray();

            BingoBoards = bingoNumberBoardChunks
                .Select(board => new BingoBoard(board))
                .ToArray();
            #endregion

            #region Part 1
            BingoBoard? winningBoard = null;
            int winningNumber = 0;
            for (int i = 0; i < numbersToCall.Count(); i++)
            {
                if (winningBoard != null) { 
                    winningNumber = numbersToCall.ElementAt(i-1);
                    break;
                }

                foreach (var board in BingoBoards)
                {
                    board.CallNumber(numbersToCall.ElementAt(i));
                    if (board.HasBingo())
                    {
                        winningBoard = board;
                        break;
                    }
                }
            }

            int uncalledNumbers = winningBoard?.GetUncalledNumberSum() ?? 0;

            Console.WriteLine(uncalledNumbers * winningNumber);
            #endregion

            #region Part 2
            BingoBoard? losingBoard = null;
            int losingNumber = 0;

            var boardsStack = new Stack<BingoBoard>(BingoBoards);
            for (int i = 0; i < numbersToCall.Count(); i++)
            {
                bool finalBoard = BingoBoards
                        .Count(board => board.HasBingo() == true) == (BingoBoards.Length - 1);





                for (int j = 0; j < BingoBoards.Length; j++)
                {
                    BingoBoard? board = BingoBoards[j];
                    board.CallNumber(numbersToCall.ElementAt(i));
                    if (board.HasBingo())
                    {
                        boardsStack.Pop();
                    }
                }
            }

            int uncalledLosingNumbers = losingBoard?.GetUncalledNumberSum() ?? 0;

            Console.WriteLine(uncalledLosingNumbers * losingNumber);
            #endregion
            Console.ReadKey();
        }

        private static BingoNumber[][] ParseInputToIntArrays(string[] input)
        {
            List<BingoNumber[]> intRows = new();

            foreach (var inputRow in input)
            {
                if (string.IsNullOrWhiteSpace(inputRow)) continue;
                intRows.Add(
                          inputRow
                            .Split(' ')
                            .Where(str =>
                                !string.IsNullOrWhiteSpace(str))
                            .Select(num => new BingoNumber(num))
                            .ToArray()
                        );
            }

            return intRows.ToArray();
        }
    }

    internal struct BingoSettings
    {
        internal const int numberOfBingoRows = 5;
        internal const int numberOfBingoColumns = 5;
    }

    internal class BingoNumber
    {
        public int Number { get; }
        public bool Called { get; private set; } 

        public BingoNumber(int number)
        {
            Number = number;
            Called = false;
        }

        public BingoNumber(string str) : this(int.Parse(str)) { } 

        public void Call() => Called = true;

        public override string ToString()
        {
            return Number.ToString();
        }

    }

    internal class BingoBoard
    {
        public DataTable bingoBoard { get; set; }

        public BingoBoard(BingoNumber[][] bingoRowSet)
        {
            var board = new DataTable();
            for (int i = 0; i < BingoSettings.numberOfBingoColumns; i++)
            {
                board.Columns.Add(new DataColumn(i.ToString(), typeof(BingoNumber)));
            }
            bingoBoard = board;

            for (int i = 0; i < BingoSettings.numberOfBingoRows; i++)
            {
                AddBingoRow(bingoRowSet[i], i);
            }
        }

        public void AddBingoRow(BingoNumber[] number, int rowId)
        {            
            var row = bingoBoard.NewRow();

            for (int i = 0; i < BingoSettings.numberOfBingoColumns; i++)
            {
                row[i] = number[i];
            }

            bingoBoard.Rows.Add(row);
        }

        public void CallNumber(int numberToCall)
        {
            foreach (DataRow row in bingoBoard.Rows)
            {
                foreach (DataColumn column in bingoBoard.Columns)
                {
                    var value = ((BingoNumber)row[column]);

                    if (value.Number == numberToCall) value.Call();
                }
            }
        }

        public bool HasBingo() {
            foreach (DataRow row in bingoBoard.Rows)
            {
                bool bingo = true;
                for (int i = 0; i < BingoSettings.numberOfBingoColumns; i++)
                {
                    bingo = bingo && ((BingoNumber)row[i]).Called;
                }
                if (bingo is true) return true;
            }

            foreach (DataColumn column in bingoBoard.Columns)
            {
                bool bingo = true;
                foreach (DataRow row in bingoBoard.Rows)
                {
                    bingo = bingo && ((BingoNumber)row[column]).Called;
                }
                if(bingo is true) return true;
            }
            return false;
        }

        public int GetUncalledNumberSum()
        {
            int sum = 0;
            foreach (DataRow row in bingoBoard.Rows)
            {
                foreach (DataColumn column in bingoBoard.Columns)
                {
                    var value = ((BingoNumber)row[column]);

                    sum += value.Called ? 0 : value.Number;
                }
            }
            return sum;
        }
    }
}
