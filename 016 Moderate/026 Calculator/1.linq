<Query Kind="Program" />

class Program
{
    public enum Operator { ADD, SUBTRACT, MULTIPLY, DIVIDE, BLANK }

    public class Term
    {
        private double value;
        private Operator op = Operator.BLANK;


        public Term(double v, Operator op2)
        {
            value = v;
            op = op2;
        }

        public double GetNumber()
        {
            return value;
        }

        public Operator GetOperator()
        {
            return op;
        }

        public void SetNumber(double v)
        {
            value = v;
        }

        public static List<Term> ParseTermSequence(string sequence)
        {
            List<Term> terms = new List<Term>();
            int offset = 0;
            while (offset < sequence.Length)
            {
                Operator op = Operator.BLANK;
                if (offset > 0)
                {
                    op = ParseOperator(sequence[offset]);
                    if (op == Operator.BLANK)
                    {
                        return null;
                    }
                    offset++;
                }
                try
                {
                    int value = ParseNextNumber(sequence, offset);
                    offset += value.ToString().Length;
                    Term term = new Term(value, op);
                    terms.Add(term);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return terms;
        }

        public static Operator ParseOperator(char op)
        {
            switch (op)
            {
                case '+': return Operator.ADD;
                case '-': return Operator.SUBTRACT;
                case '*': return Operator.MULTIPLY;
                case '/': return Operator.DIVIDE;
            }
            return Operator.BLANK;
        }

        public static int ParseNextNumber(string sequence, int offset)
        {
            StringBuilder sb = new StringBuilder();
            while (offset < sequence.Length && char.IsDigit(sequence[offset]))
            {
                sb.Append(sequence[offset]);
                offset++;
            }
            return int.Parse(sb.ToString());
        }
    }

    public static Term CollapseTerm(Term primary, Term secondary)
    {
        if (primary == null) return secondary;
        if (secondary == null) return primary;

        double value = ApplyOp(primary.GetNumber(), secondary.GetOperator(), secondary.GetNumber());
        primary.SetNumber(value);
        return primary;
    }

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

    /* Compute the result of the arithmetic sequence. This
       works by reading left to right and applying each term to
       a result. When we see a multiplication or division, we 
       instead apply this sequence to a temporary variable. */
    public static double Compute(string sequence)
    {
        List<Term> terms = Term.ParseTermSequence(sequence);
        if (terms == null) return int.MinValue;

        double result = 0;
        Term processing = null;
        for (int i = 0; i < terms.Count; i++)
        {
            Term current = terms[i];
            Term next = i + 1 < terms.Count ? terms[i + 1] : null;

            /* Apply the current term to “processing”. */
            processing = CollapseTerm(processing, current);

            /* If next term is + or -, then this cluster is done 
             * and we should apply “processing” to “result”. */
            if (next == null || next.GetOperator() == Operator.ADD || next.GetOperator() == Operator.SUBTRACT)
            {
                result = ApplyOp(result, processing.GetOperator(), processing.GetNumber());
                processing = null;
            }
        }

        return result;
    }

    public static void Main(string[] args)
    {
        string expression = "6/5*3+4*5/2-12/6*3/3+3+3";
        double result = Compute(expression);
        Console.WriteLine(result);
    }
}
