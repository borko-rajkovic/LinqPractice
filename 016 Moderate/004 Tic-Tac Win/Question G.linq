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

    public class Check
    {
        public int row, column;
        private int rowIncrement, columnIncrement;
        public Check(int row, int column, int rowI, int colI)
        {
            this.row = row;
            this.column = column;
            this.rowIncrement = rowI;
            this.columnIncrement = colI;
        }

        public void Increment()
        {
            row += rowIncrement;
            column += columnIncrement;
        }

        public Boolean InBounds(int size)
        {
            return row >= 0 && column >= 0 &&
                    row < size && column < size;
        }
    }

    public static Piece HasWon(Piece[][] board)
    {
        if (board.Length != board[0].Length) return Piece.Empty;
        int size = board.Length;

        /* Create list of things to check. */
        List<Check> instructions = new List<Check>();
        for (int i = 0; i < board.Length; i++)
        {
            instructions.Add(new Check(0, i, 1, 0));
            instructions.Add(new Check(i, 0, 0, 1));
        }
        instructions.Add(new Check(0, 0, 1, 1));
        instructions.Add(new Check(0, size - 1, 1, -1));

        /* Check them. */
        foreach (Check instr in instructions)
        {
            Piece winner = hasWon(board, instr);
            if (winner != Piece.Empty)
            {
                return winner;
            }
        }
        return Piece.Empty;
    }

    public static Piece hasWon(Piece[][] board, Check instr)
    {
        Piece first = board[instr.row][instr.column];
        while (instr.InBounds(board.Length))
        {
            if (board[instr.row][instr.column] != first)
            {
                return Piece.Empty;
            }
            instr.Increment();
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
