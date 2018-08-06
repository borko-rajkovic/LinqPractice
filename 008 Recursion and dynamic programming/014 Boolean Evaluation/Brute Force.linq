<Query Kind="Program" />

class Program
{
    public static int count = 0;
    public static Boolean StringToBool(String c)
    {
        return c.Equals("1") ? true : false;
    }

    public static int CountEval(String s, Boolean result)
    {
        count++;
        if (s.Length == 0) return 0;
        if (s.Length == 1) return StringToBool(s) == result ? 1 : 0;

        int ways = 0;

        for (int i = 1; i < s.Length; i += 2)
        {
            char c = s[i];
            String left = s.Substring(0, i);
            String right = s.Substring(i + 1, (s.Length-(i+1)));
            int leftTrue = CountEval(left, true);
            int leftFalse = CountEval(left, false);
            int rightTrue = CountEval(right, true);
            int rightFalse = CountEval(right, false);
            int total = (leftTrue + leftFalse) * (rightTrue + rightFalse);

            int totalTrue = 0;
            if (c == '^')
            { // required: one true and one false
                totalTrue = leftTrue * rightFalse + leftFalse * rightTrue;
            }
            else if (c == '&')
            { // required: both true
                totalTrue = leftTrue * rightTrue;
            }
            else if (c == '|')
            { // required: anything but both false
                totalTrue = leftTrue * rightTrue + leftFalse * rightTrue + leftTrue * rightFalse;
            }

            int subWays = result ? totalTrue : total - totalTrue;
            ways += subWays;
        }

        return ways;
    }

    public static void Main(String[] args)
    {
        String expression = "0^0|1&1^1|0|1";
        Boolean result = true;

        Console.WriteLine(CountEval(expression, result));
        Console.WriteLine(count);
    }
}
