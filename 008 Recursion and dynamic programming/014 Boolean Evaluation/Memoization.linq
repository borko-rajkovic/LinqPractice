<Query Kind="Program" />

class Program
{
    public static int count = 0;
    public static Boolean StringToBool(String c)
    {
        return c.Equals("1") ? true : false;
    }

    public static int CountEval(String s, Boolean result, Dictionary<String, int> memo)
    {
        count++;
        if (s.Length == 0) return 0;
        if (s.Length == 1) return StringToBool(s) == result ? 1 : 0;
        if (memo.ContainsKey(result + s)) return memo[result + s];

        int ways = 0;

        for (int i = 1; i < s.Length; i += 2)
        {
            char c = s[i];
            String left = s.Substring(0, i);
            String right = s.Substring(i + 1, s.Length-(i+1));
            int leftTrue = CountEval(left, true, memo);
            int leftFalse = CountEval(left, false, memo);
            int rightTrue = CountEval(right, true, memo);
            int rightFalse = CountEval(right, false, memo);
            int total = (leftTrue + leftFalse) * (rightTrue + rightFalse);

            int totalTrue = 0;
            if (c == '^')
            {
                totalTrue = leftTrue * rightFalse + leftFalse * rightTrue;
            }
            else if (c == '&')
            {
                totalTrue = leftTrue * rightTrue;
            }
            else if (c == '|')
            {
                totalTrue = leftTrue * rightTrue + leftFalse * rightTrue + leftTrue * rightFalse;
            }

            int subWays = result ? totalTrue : total - totalTrue;
            ways += subWays;
        }

        memo.Add(result + s, ways);
        return ways;
    }

    public static int CountEval(String s, Boolean result)
    {
        return CountEval(s, result, new Dictionary<String, int>());
    }

    public static void Main(String[] args)
    {
        String expression = "0^0|1&1^1|0|1";
        Boolean result = true;

        Console.WriteLine(CountEval(expression, result));
        Console.WriteLine(count);
    }

}
