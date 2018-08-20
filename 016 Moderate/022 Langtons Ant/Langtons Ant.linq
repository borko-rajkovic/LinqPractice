<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
  <Namespace>System.ComponentModel</Namespace>
</Query>

public class Position
{
    public int row;
    public int column;

    public Position(int row, int column)
    {
        this.row = row;
        this.column = column;
    }

    public override Boolean Equals(Object o)
    {
        if (o is Position)
        {
            Position p = (Position)o;
            return p.row == row && p.column == column;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return (row * 31) ^ column;
    }

    public Position Clone()
    {
        return new Position(row, column);
    }
}

public enum Orientation
{
    [Description("\u2190")]
    left,
    [Description("\u2191")]
    up,
    [Description("\u2192")]
    right,
    [Description("\u2193")]
    down
}

static class OrientationMethods
{
    public static Orientation GetTurn(this Orientation o, Boolean clockwise)
    {
        if (o == Orientation.left)
        {
            return clockwise ? Orientation.up : Orientation.down;
        }
        else if (o == Orientation.up)
        {
            return clockwise ? Orientation.right : Orientation.left;
        }
        else if (o == Orientation.right)
        {
            return clockwise ? Orientation.down : Orientation.up;
        }
        else
        { // down
            return clockwise ? Orientation.left : Orientation.right;
        }
    }
}

class Program
{
    public class Grid
    {
        private Boolean[][] grid;
        private Ant ant = new Ant();

        public Grid()
        {
            grid = new Boolean[1][];
            grid[0] = new Boolean[1];
        }

        /* Copy old values into new array, with an offset/shift applied to the row and columns. */
        private void CopyWithShift(Boolean[][] oldGrid, Boolean[][] newGrid, int shiftRow, int shiftColumn)
        {
            for (int r = 0; r < oldGrid.Length; r++)
            {
                for (int c = 0; c < oldGrid[0].Length; c++)
                {
                    newGrid[r + shiftRow][c + shiftColumn] = oldGrid[r][c];
                }
            }
        }

        /* Ensure that the given position will fit on the array. If 
         * necessary, double the size of the matrix, copy the old values 
         * over, and adjust the ant's position so that it's in a positive
         * ranges.
         */
        private void EnsureFit(Position position)
        {
            int shiftRow = 0;
            int shiftColumn = 0;

            /* Calculate new number of rows. */
            int numRows = grid.Length;
            if (position.row < 0)
            {
                shiftRow = numRows;
                numRows *= 2;
            }
            else if (position.row >= numRows)
            {
                numRows *= 2;
            }

            /* Calculate new number of columns. */
            int numColumns = grid[0].Length;
            if (position.column < 0)
            {
                shiftColumn = numColumns;
                numColumns *= 2;
            }
            else if (position.column >= numColumns)
            {
                numColumns *= 2;
            }

            /* Grow array, if necessary. Shift ant's position too. */
            if (numRows != grid.Length || numColumns != grid[0].Length)
            {
                Boolean[][] newGrid = new Boolean[numRows][];
                for (int i = 0; i < newGrid.Length; i++)
                {
                    newGrid[i] = new bool[numColumns];
                }
                CopyWithShift(grid, newGrid, shiftRow, shiftColumn);
                ant.AdjustPosition(shiftRow, shiftColumn);
                grid = newGrid;
            }
        }

        /* Flip color of cells. */
        private void Flip(Position position)
        {
            int row = position.row;
            int column = position.column;
            grid[row][column] = grid[row][column] ? false : true;
        }

        /* Move ant. */
        public void Move()
        {
            ant.Turn(grid[ant.position.row][ant.position.column]); // Turn
            Flip(ant.position); // flip
            ant.Move(); // move
            EnsureFit(ant.position); // grow
        }

        /* Print board. */
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int r = 0; r < grid.Length; r++)
            {
                for (int c = 0; c < grid[0].Length; c++)
                {
                    if (r == ant.position.row && c == ant.position.column)
                    {
                        sb.Append(ant.orientation);
                    }
                    else if (grid[r][c])
                    {
                        sb.Append("X");
                    }
                    else
                    {
                        sb.Append("_");
                    }
                }
                sb.Append("\n");
            }
            sb.Append("Ant: " + ant.orientation + ". \n");
            return sb.ToString();
        }
    }

    public class Board
    {
        private HashSet<Position> whites = new HashSet<Position>();
        private Ant ant = new Ant();
        private Position topLeftCorner = new Position(0, 0);
        private Position bottomRightCorner = new Position(0, 0);

        public Board() { }

        /* Move ant. */
        public void Move()
        {
            ant.Turn(IsWhite(ant.position)); // Turn
            Flip(ant.position); // flip
            ant.Move(); // move
            EnsureFit(ant.position);
        }

        /* Flip color of cells. */
        private void Flip(Position position)
        {
            if (whites.Contains(position))
            {
                whites.Remove(position);
            }
            else
            {
                whites.Add(position.Clone());
            }
        }

        /* "Grow" the grid by tracking the most top-left and 
         * bottom-right position that we've seen. */
        private void EnsureFit(Position position)
        {
            int row = position.row;
            int column = position.column;

            topLeftCorner.row = Math.Min(topLeftCorner.row, row);
            topLeftCorner.column = Math.Min(topLeftCorner.column, column);

            bottomRightCorner.row = Math.Max(bottomRightCorner.row, row);
            bottomRightCorner.column = Math.Max(bottomRightCorner.column, column);
        }

        /* Check if cell is white. */
        public Boolean IsWhite(Position p)
        {
            return whites.Contains(p);
        }

        /* Check if cell is white. */
        public Boolean IsWhite(int row, int column)
        {
            return whites.Contains(new Position(row, column));
        }

        /* Print board. */
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            int rowMin = topLeftCorner.row;
            int rowMax = bottomRightCorner.row;
            int colMin = topLeftCorner.column;
            int colMax = bottomRightCorner.column;
            for (int r = rowMin; r <= rowMax; r++)
            {
                for (int c = colMin; c <= colMax; c++)
                {
                    if (r == ant.position.row && c == ant.position.column)
                    {
                        sb.Append(ant.orientation);
                    }
                    else if (IsWhite(r, c))
                    {
                        sb.Append("X");
                    }
                    else
                    {
                        sb.Append("_");
                    }
                }
                sb.Append("\n");
            }
            sb.Append("Ant: " + ant.orientation + ". \n");
            return sb.ToString();
        }
    }

    public class Ant
    {
        public Position position = new Position(0, 0);
        public Orientation orientation = Orientation.right;

        public void Turn(Boolean clockwise)
        {
            orientation = orientation.GetTurn(clockwise);
        }

        public void Move()
        {
            if (orientation == Orientation.left)
            {
                position.column--;
            }
            else if (orientation == Orientation.right)
            {
                position.column++;
            }
            else if (orientation == Orientation.up)
            {
                position.row--;
            }
            else if (orientation == Orientation.down)
            {
                position.row++;
            }
        }

        public void AdjustPosition(int shiftRow, int shiftColumn)
        {
            position.row += shiftRow;
            position.column += shiftColumn;
        }
    }

    public static void Main(String[] args)
    {
        Board board = new Board();
        Grid grid = new Grid();
        Console.WriteLine(board.ToString());
        for (int i = 0; i < 100; i++)
        {
            Console.WriteLine("\n\n---- MOVE " + i + " ----");
            board.Move();
            String bs = board.ToString();
            Console.WriteLine(bs);

            grid.Move();
            String gs = grid.ToString();
            Console.WriteLine(gs);

            if (!bs.Equals(gs))
            {
                Console.WriteLine("ERROR");
                break;
            }

        }
    }
}
