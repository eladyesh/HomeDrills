using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BONUS_GAME
{
    public class Board
    {
        public int[,] Data { get; protected set; } = new int[4, 4];

        public void InitialAssignment()
        {
            // Generate initial tiles
            AddRandomTile();
            AddRandomTile();
        }

        private void AddRandomTile()
        {
            List<Tuple<int, int>> emptyCells = new List<Tuple<int, int>>();

            // Find empty cells
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (Data[row, col] == 0)
                    {
                        emptyCells.Add(Tuple.Create(row, col));
                    }
                }
            }

            // Randomly select an empty cell
            if (emptyCells.Count > 0)
            {
                Random rand = new Random();
                Tuple<int, int> cell = emptyCells[rand.Next(emptyCells.Count)];
                Data[cell.Item1, cell.Item2] = rand.Next(1, 3) * 2; // Either 2 or 4
            }
        }

        public bool Move(Direction direction)
        {
            bool moved = false;
            switch (direction)
            {
                case Direction.Up:
                    for (int col = 0; col < 4; col++)
                    {
                        moved |= MoveColumn(col, -1);
                    }
                    break;
                case Direction.Down:
                    for (int col = 0; col < 4; col++)
                    {
                        moved |= MoveColumn(col, 1);
                    }
                    break;
                case Direction.Left:
                    for (int row = 0; row < 4; row++)
                    {
                        moved |= MoveRow(row, -1);
                    }
                    break;
                case Direction.Right:
                    for (int row = 0; row < 4; row++)
                    {
                        moved |= MoveRow(row, 1);
                    }
                    break;
            }

            if (moved)
            {
                AddRandomTile();
            }

            return moved;
        }

        private bool MoveColumn(int col, int direction)
        {
            int[] column = new int[4];
            for (int i = 0; i < 4; i++)
            {
                column[i] = Data[i, col];
            }

            int[] mergedColumn = MergeTiles(column, direction);

            if (!Enumerable.SequenceEqual(column, mergedColumn))
            {
                for (int i = 0; i < 4; i++)
                {
                    Data[i, col] = mergedColumn[i];
                }
                return true;
            }
            return false;
        }

        private bool MoveRow(int row, int direction)
        {
            int[] rowData = new int[4];
            for (int i = 0; i < 4; i++)
            {
                rowData[i] = Data[row, i];
            }

            int[] mergedRow = MergeTiles(rowData, direction);

            if (!Enumerable.SequenceEqual(rowData, mergedRow))
            {
                for (int i = 0; i < 4; i++)
                {
                    Data[row, i] = mergedRow[i];
                }
                return true;
            }
            return false;
        }

        private int[] MergeTiles(int[] array, int direction)
        {
            int[] mergedArray = new int[4];
            int index = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != 0)
                {
                    if (index > 0 && mergedArray[index - 1] == array[i])
                    {
                        mergedArray[index - 1] *= 2;
                        // Increment score when merging tiles
                        Game.Score += mergedArray[index - 1];
                    }
                    else
                    {
                        mergedArray[index] = array[i];
                        index++;
                    }
                }
            }

            if (direction == -1)
            {
                return mergedArray;
            }
            else
            {
                return mergedArray.Reverse().ToArray();
            }
        }
    }

    public class Game
    {
        public Board Board = new Board();
        private GameStatus _status = GameStatus.Idle;
        private static int _score = 0;

        public static int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        public void Start()
        {
            Board.InitialAssignment();
            _status = GameStatus.Idle;
            _score = 0;
        }

        public GameStatus Status => _status;

        public bool Move(Direction direction)
        {
            if (_status == GameStatus.Lose)
                return false;

            bool moved = Board.Move(direction);

            if (moved)
            {
                if (IsWin())
                {
                    _status = GameStatus.Win;
                }
                else if (!CanMove())
                {
                    _status = GameStatus.Lose;
                }
            }

            return moved;
        }

        private bool IsWin()
        {
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (Board.Data[row, col] == 2048)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CanMove()
        {
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (Board.Data[row, col] == 0)
                    {
                        return true;
                    }
                    if (row > 0 && Board.Data[row, col] == Board.Data[row - 1, col])
                    {
                        return true;
                    }
                    if (col > 0 && Board.Data[row, col] == Board.Data[row, col - 1])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    public class ConsoleGame
    {
        private Game _game = new Game();

        public void Start()
        {
            Console.WriteLine("Welcome to 2048!");
            Console.WriteLine("Use arrow keys to move tiles. Press Q to quit.");
            Console.ReadKey(true);

            _game.Start();
            DrawBoard();

            while (_game.Status == GameStatus.Idle)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                {
                    _game.Move(Direction.Up);
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    _game.Move(Direction.Down);
                }
                else if (key.Key == ConsoleKey.LeftArrow)
                {
                    _game.Move(Direction.Left);
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    _game.Move(Direction.Right);
                }
                else if (key.Key == ConsoleKey.Q)
                {
                    break;
                }

                DrawBoard();
            }

            if (_game.Status == GameStatus.Win)
            {
                Console.WriteLine("Congratulations! You win!");
            }
            else if (_game.Status == GameStatus.Lose)
            {
                Console.WriteLine("Game over! You lose!");
            }
        }

        private void DrawBoard()
        {
            Console.Clear();
            Console.WriteLine("Score: " + Game.Score);
            Console.WriteLine();

            // Calculate the width of the entire board (including separators)
            int boardWidth = 4 * 8 + 5; // Width of each number (8 characters) + Width of separators (1 character)

            // Center the board horizontally
            int leftPadding = (Console.WindowWidth - boardWidth) / 2;

            // Iterate over each row in the board
            for (int row = 0; row < 4; row++)
            {
                // Set the cursor position for the current row
                Console.SetCursorPosition(leftPadding, Console.CursorTop);

                // Print horizontal separator at the beginning of each row
                Console.Write("|");

                // Iterate over each column in the row
                for (int col = 0; col < 4; col++)
                {
                    // Print the number with adjusted padding and font color
                    Console.ForegroundColor = GetColor(_game.Board.Data[row, col]);
                    Console.Write(_game.Board.Data[row, col].ToString().PadLeft(5).PadRight(7));
                    Console.ResetColor();

                    // Print vertical separator after each number
                    Console.Write("|");
                }

                // Move to the next row
                Console.WriteLine();
            }
        }

        private ConsoleColor GetColor(int number)
        {
            // Choose color based on the number
            switch (number)
            {
                case 0:
                    return ConsoleColor.Gray;
                case 2:
                    return ConsoleColor.Yellow;
                case 4:
                    return ConsoleColor.Green;
                case 8:
                    return ConsoleColor.Cyan;
                case 16:
                    return ConsoleColor.Blue;
                case 32:
                    return ConsoleColor.Magenta;
                case 64:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.White;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ConsoleGame game = new ConsoleGame();
            game.Start();
        }
    }
}
