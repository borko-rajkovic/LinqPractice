<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public class Result
    {
        public int hits;
        public int pseudoHits;

        public Result(int h, int p)
        {
            hits = h;
            pseudoHits = p;
        }

        public Result()
        {
        }

        public override String ToString()
        {
            return "(" + hits + ", " + pseudoHits + ")";
        }
    };

    public static int Code(char c)
    {
        switch (c)
        {
            case 'B':
                return 0;
            case 'G':
                return 1;
            case 'R':
                return 2;
            case 'Y':
                return 3;
            default:
                return -1;
        }
    }

    public static int MAX_COLORS = 4;

    public static Result Estimate(String guess, String solution)
    {
        if (guess.Length != solution.Length) return null;
        Result res = new Result();
        int[] frequencies = new int[MAX_COLORS];

        /* Compute hits and built frequency table */
        for (int i = 0; i < guess.Length; i++)
        {
            if (guess[i] == solution[i])
            {
                res.hits++;
            }
            else
            {
                /* Only increment the frequency table (which will be used for pseudo-hits) if
                 * it's not a hit. If it's a hit, the slot has already been "used." */
                int code = Code(solution[i]);
                if (code >= 0)
                {
                    frequencies[code]++;
                }
            }
        }

        /* Compute pseudo-hits */
        for (int i = 0; i < guess.Length; i++)
        {
            int code = Code(guess[i]);
            if (code >= 0 && frequencies[code] > 0 && guess[i] != solution[i])
            {
                res.pseudoHits++;
                frequencies[code]--;
            }
        }
        return res;
    }

    /************************** TEST CODE **********************************/

    public static char LetterFromCode(int k)
    {
        switch (k)
        {
            case 0:
                return 'B';
            case 1:
                return 'G';
            case 2:
                return 'R';
            case 3:
                return 'Y';
            default:
                return '0';
        }
    }

    public static Result EstimateBad(String g, String s)
    {
        char[] guess = g.ToCharArray();
        char[] solution = s.ToCharArray();
        int hits = 0;
        for (int i = 0; i < guess.Length; i++)
        {
            if (guess[i] == solution[i])
            {
                hits++;
                solution[i] = '0';
                guess[i] = '0';
            }
        }

        int pseudohits = 0;

        for (int i = 0; i < guess.Length; i++)
        {
            if (guess[i] != '0')
            {
                for (int j = 0; j < solution.Length; j++)
                {
                    if (solution[j] != '0')
                    {
                        if (solution[j] == guess[i])
                        {
                            pseudohits++;
                            solution[j] = '0';
                            break;
                        }
                    }
                }
            }
        }

        return new Result(hits, pseudohits);
    }

    public static String RandomString()
    {
        int length = 4;
        char[] str = new char[length];
        Random generator = new Random();

        for (int i = 0; i < length; i++)
        {
            int v = generator.Next(4);
            char c = LetterFromCode(v);
            str[i] = c;
        }

        return new string(str);
    }

    public static Boolean Test(String guess, String solution)
    {
        Result res1 = Estimate(guess, solution);
        Result res2 = EstimateBad(guess, solution);
        if (res1.hits == res2.hits && res1.pseudoHits == res2.pseudoHits)
        {
            return true;
        }
        else
        {
            Console.WriteLine("FAIL: (" + guess + ", " + solution + "): " + res1.ToString() + " | " + res2.ToString());
            return false;
        }
    }

    public static Boolean TestRandom()
    {
        String guess = RandomString();
        String solution = RandomString();
        return Test(guess, solution);
    }

    public static Boolean Test(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (!TestRandom())
            {
                return true;
            }
        }
        return false;
    }

    public static void Main(String[] args)
    {
        Test(1000);
    }
}
