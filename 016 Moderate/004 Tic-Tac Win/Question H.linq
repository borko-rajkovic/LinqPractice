<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public enum Piece { Empty, Red, Blue };

    public class Position
    {
        public int row, column;
        public Position(int row, int column)
        {
            this.row = row;
            this.column = column;
        }
    }

    public static int[][] RandomMatrix(int M, int N, int min, int max)
    {
        int[][] matrix = new int[M][];
        for (int i = 0; i < M; i++)
        {
            matrix[i] = new int[N];
        }
        for (int i = 0; i < M; i++)
        {
            for (int j = 0; j < N; j++)
            {
                matrix[i][j] = RandomIntInRange(min, max);
            }
        }
        return matrix;
    }

    public static int RandomIntInRange(int min, int max)
    {
        return RandomInt(max + 1 - min) + min;
    }

    public static int RandomInt(int n)
    {
        return RandomIntNumbers.Next(n);
    }

    private static readonly Random RandomIntNumbers = new Random();

    public static void PrintMatrix(int[][] matrix)
    {
        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix[i].Length; j++)
            {
                if (matrix[i][j] < 10 && matrix[i][j] > -10)
                {
                    Console.Write(" ");
                }
                if (matrix[i][j] < 100 && matrix[i][j] > -100)
                {
                    Console.Write(" ");
                }
                if (matrix[i][j] >= 0)
                {
                    Console.Write(" ");
                }
                Console.Write(" " + matrix[i][j]);
            }
            Console.WriteLine();
        }
    }

    public class PositionIterator : IEnumerable<Position>
    {
        private int rowIncrement, colIncrement, size;
        private Position current;

        public PositionIterator(Position p, int rowIncrement, int colIncrement, int size)
        {
            this.rowIncrement = rowIncrement;
            this.colIncrement = colIncrement;
            this.size = size;
            current = new Position(p.row - rowIncrement, p.column - colIncrement);
        }

        public IEnumerator<Position> GetEnumerator()
        {
            while (current.row + rowIncrement < size && current.column + colIncrement < size)
            {
                current = new Position(current.row + rowIncrement, current.column + colIncrement);
                yield return current;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


    public static Piece HasWon(Piece[][] board)
    {
        if (board.Length != board[0].Length) return Piece.Empty;
        int size = board.Length;

        List<PositionIterator> instructions = new List<PositionIterator>();
        for (int i = 0; i < board.Length; i++)
        {
            instructions.Add(new PositionIterator(new Position(0, i), 1, 0, size));
            instructions.Add(new PositionIterator(new Position(i, 0), 0, 1, size));
        }
        instructions.Add(new PositionIterator(new Position(0, 0), 1, 1, size));
        instructions.Add(new PositionIterator(new Position(0, size - 1), 1, -1, size));

        foreach (PositionIterator iterator in instructions)
        {
            Piece winner = HasWon(board, iterator);
            if (winner != Piece.Empty)
            {
                return winner;
            }
        }
        return Piece.Empty;
    }

    public static Piece HasWon(Piece[][] board, PositionIterator iterator)
    {
        int i = 0;
        Position firstPosition;
        Piece first=Piece.Empty;
        foreach (var item in iterator)
        {
            if (i == 0)
            {
                firstPosition = item;
                first = board[firstPosition.row][firstPosition.column];
            }
            Position position = item;
            if (board[position.row][position.column] != first)
            {
                return Piece.Empty;
            }
            i++;
        }

        return first;
    }

    public static void Main(String[] args)
    {

        int N = 3;
        int[][] board_t = RandomMatrix(N, N, 0, 2);
        Piece[][] board = new Piece[N][];
        for (int i = 0; i < N; i++)
        {
            board[i] = new Piece[N];
        }

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                int x = board_t[i][j];
                board[i][j] = (Piece)x;
            }
        }

        Piece p1 = HasWon(board);

        Console.WriteLine(p1);
        PrintMatrix(board_t);
    }
}
