using _2048_BONUS;
using System;
using System.Collections.Generic;
using System.Linq;


namespace _2048_BONUS
{
    public enum GameStatus
    {
        Win,
        Lose,
        Idle
    }
}


public enum Direction
{
    Up,
    Down,
    Left,
    Right
}
public class Board
{
    private int[,] _data = new int[4, 4];

    public int[,] Data => _data;

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
                if (_data[row, col] == 0)
                {
                    emptyCells.Add(Tuple.Create(row, col));
                }
            }
        }

        // Randomly select an empty cell
        if (emptyCells.Any())
        {
            Random rand = new Random();
            Tuple<int, int> cell = emptyCells[rand.Next(emptyCells.Count)];
            _data[cell.Item1, cell.Item2] = rand.Next(1, 3) * 2; // Either 2 or 4
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
            column[i] = _data[i, col];
        }

        int[] mergedColumn = MergeTiles(column, direction);

        if (!Enumerable.SequenceEqual(column, mergedColumn))
        {
            for (int i = 0; i < 4; i++)
            {
                _data[i, col] = mergedColumn[i];
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
            rowData[i] = _data[row, i];
        }

        int[] mergedRow = MergeTiles(rowData, direction);

        if (!Enumerable.SequenceEqual(rowData, mergedRow))
        {
            for (int i = 0; i < 4; i++)
            {
                _data[row, i] = mergedRow[i];
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
    private Board _board = new Board();
    private GameStatus _status = GameStatus.Idle;
    private int _points = 0;

    public Board Board => _board;
    public GameStatus Status => _status;
    public int Points => _points;

    public void Start()
    {
        _board.InitialAssignment();
        _status = GameStatus.Idle;
        _points = 0;
    }

    public bool Move(Direction direction)
    {
        if (_status == GameStatus.Lose)
            return false;

        bool moved = _board.Move(direction);

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
                if (_board.Data[row, col] == 2048)
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
                if (_board.Data[row, col] == 0)
                {
                    return true;
                }
                if (row > 0 && _board.Data[row, col] == _board.Data[row - 1, col])
                {
                    return true;
                }
                if (col > 0 && _board.Data[row, col] == _board.Data[row, col - 1])
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
        Console.WriteLine("Score: " + _game.Points);
        Console.WriteLine();

        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                Console.Write(_game.Board.Data[row, col] + "\t");
            }
            Console.WriteLine();
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("This is running");
        ConsoleGame game = new ConsoleGame();
        game.Start();
    }
}
