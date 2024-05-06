using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BONUS_GAME
{
    /// <summary>
    /// Represents the game logic for a 2048-like game.
    /// </summary>
    public class Game
    {
        // Represents the game board.
        public Board Board = new Board();

        // Represents the current status of the game.
        private GameStatus _status = GameStatus.Idle;

        // Represents the player's score.
        private static int _score = 0;

        // Gets or sets the player's score.
        public static int Points
        {
            get { return _score; }
            protected set { _score = value; }
        }

        /// <summary>
        /// Starts a new game by initializing the board and resetting the score and status.
        /// </summary>
        public void Start()
        {
            Board.InitialAssignment();
            _status = GameStatus.Idle;
            _score = 0;
        }

        // Gets the current status of the game.
        public GameStatus Status => _status;

        /// <summary>
        /// Moves the tiles on the board in the specified direction.
        /// </summary>
        /// <param name="direction">The direction in which to move the tiles.</param>
        /// <returns>True if the tiles were moved, false otherwise.</returns>
        public void Move(Direction direction)
        {
            // If the game is already lost, disallow further moves.
            if (_status == GameStatus.Lose)
                return;

            // Attempt to move the tiles in the specified direction.
            int score = Board.Move(direction);
            Points += score;

            // Check if the move was successful and update game status accordingly.
            if (IsWin())
            {
                _status = GameStatus.Win;
            }
            else if (!CanMove())
            {
                _status = GameStatus.Lose;
            }
        }

        /// <summary>
        /// Checks if the player has won the game (reached the 2048 tile).
        /// </summary>
        /// <returns>True if the player has won, false otherwise.</returns>

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

        /// <summary>
        /// Checks if there are any legal moves left on the board.
        /// </summary>
        /// <returns>True if there are legal moves left, false otherwise.</returns>
        private bool CanMove()
        {
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    // Check for empty cell to move to
                    if (Board.Data[row, col] == 0)
                    {
                        return true;
                    }

                    // Check if can move left 
                    if (row > 0 && Board.Data[row, col] == Board.Data[row - 1, col])
                    {
                        return true;
                    }

                    // Check if can move up
                    // No need for furtuer check, because if can move down or right it will be revealed here
                    if (col > 0 && Board.Data[row, col] == Board.Data[row, col - 1])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
