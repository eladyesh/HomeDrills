using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BONUS_GAME
{
    /// <summary>
    /// Represents the game board for a 2048-like game.
    /// </summary>
    public class Board
    {
        /// <summary>
        /// Gets or sets the data grid representing the game board.
        /// </summary>
        public int[,] Data { get; protected set; } = new int[4, 4];

        /// <summary>
        /// Initializes the game board by adding two initial tiles.
        /// </summary>
        public void InitialAssignment()
        {
            // Generate initial tiles
            AddRandomTile();
            AddRandomTile();
        }

        /// <summary>
        /// Adds a random tile to an empty cell on the board.
        /// </summary>
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

        /// <summary>
        /// Moves tiles on the board in the specified direction.
        /// </summary>
        /// <param name="direction">The direction in which to move the tiles.</param>
        /// <returns>True if any tiles were moved, false otherwise.</returns>
        public int Move(Direction direction)
        {
            int gamePoints = 0;

            // Check what numbers are even at each column row
            // If you can move, update the moved to true, and return
            switch (direction)
            {
                case Direction.Up:
                    for (int col = 0; col < 4; col++)
                    {
                        gamePoints += MoveColumn(col, -1);
                    }
                    break;
                case Direction.Down:
                    for (int col = 0; col < 4; col++)
                    {
                        gamePoints += MoveColumn(col, 1);
                    }
                    break;
                case Direction.Left:
                    for (int row = 0; row < 4; row++)
                    {
                        gamePoints += MoveRow(row, -1);
                    }
                    break;
                case Direction.Right:
                    for (int row = 0; row < 4; row++)
                    {
                        gamePoints += MoveRow(row, 1);
                    }
                    break;
            }

            // Once moved, add random tile
            AddRandomTile();

            return gamePoints;
        }

        /// <summary>
        /// Moves tiles within a column in the specified direction.
        /// </summary>
        /// <param name="col">The column index.</param>
        /// <param name="direction">The direction in which to move the tiles.</param>
        /// <returns>True if any tiles were moved, false otherwise.</returns>
        private int MoveColumn(int col, int direction)
        {
            int points = 0;

            // Repeat the movement and merging until no further movement is possible
            do
            {
                int[] columnData = new int[4];
                for (int row = 0; row < 4; row++)
                {
                    columnData[row] = Data[row, col];
                }

                // Determine the range of rows to iterate based on the direction
                int startRow = (direction == -1) ? 1 : 2;
                int endRow = (direction == -1) ? 4 : -1;
                int step = (direction == -1) ? 1 : -1;

                // Track if any movement or merging occurred in this iteration
                bool iterationMoved = false;

                // Move tiles within the column
                for (int row = startRow; row != endRow; row += step)
                {
                    if (columnData[row] != 0)
                    {
                        int newRow = row;

                        // Check for equal tiles, and merge or move
                        while (newRow + direction >= 0 && newRow + direction < 4 && (columnData[newRow + direction] == 0 || columnData[newRow + direction] == columnData[row]))
                        {
                            if (columnData[newRow + direction] == columnData[row])
                            {
                                columnData[newRow + direction] *= 2;
                                points += columnData[newRow + direction];
                                columnData[row] = 0;
                                iterationMoved = true;
                                break;
                            }
                            else
                            {
                                columnData[newRow + direction] = columnData[row];
                                columnData[row] = 0;
                                newRow += direction;
                                iterationMoved = true;
                            }
                        }
                    }
                }

                // Update the board if any movement or merging occurred in this iteration
                if (iterationMoved)
                {
                    for (int row = 0; row < 4; row++)
                    {
                        Data[row, col] = columnData[row];
                    }
                }
                else
                {
                    // If no movement occurred in this iteration, break the loop
                    break;
                }

            } while (true); // Repeat until no further movement is possible

            return points;
        }

        /// <summary>
        /// Moves tiles within a row in the specified direction.
        /// </summary>
        /// <param name="row">The row index.</param>
        /// <param name="direction">The direction in which to move the tiles.</param>
        /// <returns>True if any tiles were moved, false otherwise.</returns>
        private int MoveRow(int row, int direction)
        {
            int points = 0;

            // Repeat the movement and merging until no further movement is possible
            do
            {
                int[] rowData = new int[4];
                for (int col = 0; col < 4; col++)
                {
                    rowData[col] = Data[row, col];
                }

                // Determine the range of columns to iterate based on the direction
                int startCol = (direction == -1) ? 1 : 2;
                int endCol = (direction == -1) ? 4 : -1;
                int step = (direction == -1) ? 1 : -1;

                // Track if any movement or merging occurred in this iteration
                bool iterationMoved = false;

                // Move tiles within the row
                for (int col = startCol; col != endCol; col += step)
                {
                    if (rowData[col] != 0)
                    {
                        int newCol = col;

                        // Check for equal tiles, and merge or move
                        while (newCol + direction >= 0 && newCol + direction < 4 && (rowData[newCol + direction] == 0 || rowData[newCol + direction] == rowData[col]))
                        {
                            if (rowData[newCol + direction] == rowData[col])
                            {
                                rowData[newCol + direction] *= 2;
                                points += rowData[newCol + direction];
                                rowData[col] = 0;
                                iterationMoved = true;
                                break;
                            }
                            else
                            {
                                rowData[newCol + direction] = rowData[col];
                                rowData[col] = 0;
                                newCol += direction;
                                iterationMoved = true;
                            }
                        }
                    }
                }

                // Update the board if any movement or merging occurred in this iteration
                if (iterationMoved)
                {
                    for (int col = 0; col < 4; col++)
                    {
                        Data[row, col] = rowData[col];
                    }
                }
                else
                {
                    // If no movement occurred in this iteration, break the loop
                    break;
                }

            } while (true); // Repeat until no further movement is possible

            return points;
        }
    }
}
