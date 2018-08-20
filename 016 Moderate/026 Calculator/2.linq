<Query Kind="Program" />

class Program
{
    public enum Operator { ADD, SUBTRACT, MULTIPLY, DIVIDE, BLANK }

    /* Return the operator that occurs as offset. */
    public static Operator ParseNextOperator(string sequence, int offset)
    {
        if (offset < sequence.Length)
        {
            char op = sequence[offset];
            switch (op)
            {
                case '+': return Operator.ADD;
                case '-': return Operator.SUBTRACT;
                case '*': return Operator.MULTIPLY;
                case '/': return Operator.DIVIDE;
            }
        }
        return Operator.BLANK;
    }

    /* Return the number that starts at offset. */
    public static int ParseNextNumber(string seq, int offset)
    {
        StringBuilder sb = new StringBuilder();
        while (offset < seq.Length && char.IsDigit(seq[offset]))
        {
            sb.Append(seq[offset]);
            offset++;
        }
        return int.Parse(sb.ToString());
    }

    /* Apply operator: left [op] right. */
    public static double ApplyOp(double left, Operator op, double right)
    {
        if (op == Operator.ADD)
        {
            return left + right;
        }
        else if (op == Operator.SUBTRACT)
        {
            return left - right;
        }
        else if (op == Operator.MULTIPLY)
        {
            return left * right;
        }
        else if (op == Operator.DIVIDE)
        {
            return left / right;
        }
        else
        {
            return right;
        }
    }

    /* Return priority of operator. Mapped so that:
     *     addition == subtraction < multiplication == division. */
    public static int PriorityOfOperator(Operator op)
    {
        switch (op)
        {
            case Operator.ADD: return 1;
            case Operator.SUBTRACT: return 1;
            case Operator.MULTIPLY: return 2;
            case Operator.DIVIDE: return 2;
            case Operator.BLANK: return 0;
        }
        return 0;
    }

    /* Collapse top until priority(futureTop) > priority(top). 
     * Collapsing means to pop the top 2 numbers and apply the 
     * operator popped from the top of the operator stack, and then
     * push that onto the numbers stack.*/
    public static void CollapseTop(Operator futureTop, Stack<double> numberStack, Stack<Operator> operatorStack)
    {
        while (operatorStack.Count >= 1 && numberStack.Count >= 2)
        {
            if (PriorityOfOperator(futureTop) <= PriorityOfOperator(operatorStack.Peek()))
            {
                double second = numberStack.Pop();
                double first = numberStack.Pop();
                Operator op = operatorStack.Pop();
                double collapsed = ApplyOp(first, op, second);
                numberStack.Push(collapsed);
            }
            else
            {
                break;
            }
        }
    }

    public static double Compute(String sequence)
    {
        Stack<Double> numberStack = new Stack<Double>();
        Stack<Operator> operatorStack = new Stack<Operator>();

        for (int i = 0; i < sequence.Length; i++)
        {
            try
            {
                /* Get number and push. */
                int value = ParseNextNumber(sequence, i);
                numberStack.Push(value);

                /* Move to the operator. */
                i += value.ToString().Length;
                if (i >= sequence.Length)
                {
                    break;
                }

                /* Get operator, collapse top as needed, push operator. */
                Operator op = ParseNextOperator(sequence, i);
                CollapseTop(op, numberStack, operatorStack);
                operatorStack.Push(op);
            }
            catch (Exception)
            {
                return int.MinValue;
            }
        }

        /* Do final collapse. */
        CollapseTop(Operator.BLANK, numberStack, operatorStack);
        if (numberStack.Count == 1 && operatorStack.Count == 0)
        {
            return numberStack.Pop();
        }
        return 0;
    }

    public static void Main(string[] args)
    {
        string expression = "6/5*3+4*5/2-12/6*3/3+3+3";
        double result = Compute(expression);
        Console.WriteLine(result);
    }
}
