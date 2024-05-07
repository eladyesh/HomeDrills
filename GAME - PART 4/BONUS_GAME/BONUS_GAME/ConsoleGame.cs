using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BONUS_GAME
{
    /// <summary>
    /// Represents the console-based user interface for the 2048 game.
    /// </summary>
    public class ConsoleGame
    {
        // Game instance
        private Game _game = new Game();

        /// <summary>
        /// Starts the game and handles user input.
        /// </summary>
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
                Console.ReadKey(true);
            }
            else if (_game.Status == GameStatus.Lose)
            {
                Console.WriteLine("Game over! You lose!");
                Console.ReadKey(true);
            }
        }

        /// <summary>
        /// Draws the game board on the console.
        /// </summary>
        private void DrawBoard()
        {
            // Clear the console before drawing the board
            Console.Clear();

            // Print the current score
            Console.WriteLine("Score: " + Game.Points);
            Console.WriteLine();

            // Calculate the width of the entire board (including separators)
            int boardWidth = 4 * 8 + 5; // Each number has 8 characters with padding, and there are 4 numbers with 5 separators

            // Center the board horizontally
            int leftPadding = (Console.WindowWidth - boardWidth) / 2;

            // Print the top border of the box
            Console.SetCursorPosition(leftPadding, Console.CursorTop);
            Console.Write("+");
            for (int i = 0; i < boardWidth - 5; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("+");

            // Iterate over each row in the board
            for (int row = 0; row < 4; row++)
            {
                // Set the cursor position for the current row
                Console.SetCursorPosition(leftPadding, Console.CursorTop);

                // Print vertical separator at the beginning of each row
                Console.Write("|");

                // Iterate over each column in the row
                for (int col = 0; col < 4; col++)
                {
                    // Print the number with adjusted padding and font color
                    Console.ForegroundColor = GetColor(_game.Board.Data[row, col]); // GetColor() returns the appropriate color based on the number
                    Console.Write(_game.Board.Data[row, col].ToString().PadLeft(5).PadRight(7)); // Adjust padding for each number
                    Console.ResetColor();

                    Console.Write(col == 3 ? " |" : "|");
                }

                // Move to the next row
                Console.WriteLine();

                // Print horizontal separator line after each row
                Console.SetCursorPosition(leftPadding, Console.CursorTop);
                Console.Write("+");
                for (int i = 0; i < boardWidth - 5; i++)
                {
                   Console.Write("-");
                }
                Console.WriteLine("+");
            }
        }

        /// <summary>
        /// Determines the console color for displaying a number.
        /// </summary>
        /// <param name="number">The number to determine the color for.</param>
        /// <returns>The console color for displaying the number.</returns>
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
                    return ConsoleColor.DarkCyan;
                case 32:
                    return ConsoleColor.Magenta;
                case 64:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.White;
            }
        }
    }
}
