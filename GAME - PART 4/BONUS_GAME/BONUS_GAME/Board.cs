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
        /// Checks if all elements in the array are equal or if the array can be divided into two equal halves.
        /// </summary>
        /// <param name="numbers">The array of integers to be checked.</param>
        /// <returns>True if all elements are equal or if two halves are equal, otherwise false.</returns>
        public static bool IsAllEqualOrTwoHalvesEqual(int[] numbers)
        {
            // Check if the array contains any zeros
            if (numbers.Contains(0))
            {
                return false;
            }

            IEnumerable<IGrouping<int, int>> groupedNumbers = numbers.GroupBy(n => n);
            int distinctGroupsCount = groupedNumbers.Count();

            if (distinctGroupsCount == 1)
            {
                return true; // All elements are equal
            }
            else if (distinctGroupsCount == 2)
            {
                // Check if first number in half is equal to the other in the half
                // So that [2,2,4,4] would be return true
                // But [2,4,4,2] would be return false

                return (numbers[0] == numbers[1]) && (numbers[2] == numbers[3]); 
            }
            else
            {
                return false; // More than two distinct numbers
            }
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

            // A set to keep indexes of merged tiles
            HashSet<int> merged = new HashSet<int>();

            // Check if array has equal halves (e.g [2,2,2,2] or [4,4,2,2] or [2,2,4,4])
            int[] columnData = Enumerable.Range(0, 4).Select(row => Data[row, col]).ToArray();
            bool allEqualOrTwoHalvesEqual = IsAllEqualOrTwoHalvesEqual(columnData);

            // Repeat the movement and merging until no further movement is possible
            do
            {
                bool iterationMoved = false; // Reset for each iteration
                columnData = new int[4];
                for (int row = 0; row < 4; row++)
                {
                    columnData[row] = Data[row, col];
                }

                // Determine the range of rows to iterate based on the direction
                int startRow = (direction == -1) ? 1 : 2;
                int endRow = (direction == -1) ? 4 : -1;
                int step = (direction == -1) ? 1 : -1;

                // Move tiles within the column
                for (int row = startRow; row != endRow; row += step)
                {
                    if (columnData[row] != 0)
                    {
                        int newRow = row;

                        // Check for equal tiles, and merge or move
                        while (newRow + direction >= 0 && newRow + direction < 4 && (columnData[newRow + direction] == 0 || columnData[newRow + direction] == columnData[row] || !merged.Contains(newRow + direction)))
                        {
                            if (columnData[newRow + direction] == columnData[row])
                            {
                                // Check if you can merge
                                if ((!merged.Contains(newRow) && !merged.Contains(newRow + direction)) || (merged.Count <= 2 && allEqualOrTwoHalvesEqual))
                                {
                                    columnData[newRow + direction] *= 2;
                                    points += columnData[newRow + direction];
                                    columnData[row] = 0;
                                    iterationMoved = true; // Set to true to continue the outer loop
                                    merged.Add(newRow);
                                    merged.Add(newRow + direction);
                                }
                                break;
                            }
                            else
                            {

                                // If the tile in the new position is empty (zero)
                                if (columnData[newRow + direction] == 0)
                                {
                                    // Move the current tile to the empty position
                                    columnData[newRow + direction] = columnData[row];
                                    columnData[row] = 0;
                                    newRow += direction; // Move to the next position
                                    iterationMoved = true; // Set to true to continue the outer loop
                                }
                                else
                                {
                                    break;
                                }
                            }
                            newRow += direction; // Move to the next position
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
                    break;
                }
            } while (true);

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

            // A set to keep indexes of merged tiles
            HashSet<int> merged = new HashSet<int>();

            // Check if array has equal halves (e.g [2,2,2,2] or [4,4,2,2] or [2,2,4,4])
            int[] rowData = Enumerable.Range(0, 4).Select(col => Data[row, col]).ToArray();
            bool allEqualOrTwoHalvesEqual = IsAllEqualOrTwoHalvesEqual(rowData);

            // Repeat the movement and merging until no further movement is possible
            do
            {
                bool iterationMoved = false; // Reset for each iteration
                rowData = new int[4];
                for (int col = 0; col < 4; col++)
                {
                    rowData[col] = Data[row, col];
                }

                // Determine the range of columns to iterate based on the direction
                int startCol = (direction == -1) ? 1 : 2;
                int endCol = (direction == -1) ? 4 : -1;
                int step = (direction == -1) ? 1 : -1;

                // Move tiles within the row
                for (int col = startCol; col != endCol; col += step)
                {
                    if (rowData[col] != 0)
                    {
                        int newCol = col;

                        // Check for equal tiles, and merge or move
                        while (newCol + direction >= 0 && newCol + direction < 4 && (rowData[newCol + direction] == 0 || rowData[newCol + direction] == rowData[col] || !merged.Contains(newCol + direction)))
                        {
                            if (rowData[newCol + direction] == rowData[col])
                            {
                                // Check if you can merge
                                if ((!merged.Contains(newCol) && !merged.Contains(newCol + direction)) || (merged.Count <= 2 && allEqualOrTwoHalvesEqual))
                                {
                                    rowData[newCol + direction] *= 2;
                                    points += rowData[newCol + direction];
                                    rowData[col] = 0;
                                    iterationMoved = true; // Set to true to continue the outer loop
                                    merged.Add(newCol);
                                    merged.Add(newCol + direction);
                                }
                                break;
                            }
                            else
                            {
                                // If the tile in the new position is empty (zero)
                                if (rowData[newCol + direction] == 0)
                                {
                                    // Move the current tile to the empty position
                                    rowData[newCol + direction] = rowData[col];
                                    rowData[col] = 0;
                                    newCol += direction; // Move to the next position
                                    iterationMoved = true; // Set to true to continue the outer loop
                                }
                                else
                                {
                                    break;
                                }
                            }
                            newCol += direction; // Move to the next position
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
                    break;
                }
            } while (true);

            return points;
        }


    }
}
