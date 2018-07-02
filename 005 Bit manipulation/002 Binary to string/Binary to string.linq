<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

//using System.Text;

class Program
{

        public static String PrintBinary(double num)
        {
            if (num >= 1 || num <= 0)
            {
                return "ERROR";
            }

            StringBuilder binary = new StringBuilder();
            binary.Append(".");
            while (num > 0)
            {
                /* Setting a limit on length: 32 characters */
                if (binary.Length > 32)
                {
                    return "ERROR";
                }
                double r = num * 2;
                if (r >= 1)
                {
                    binary.Append(1);
                    num = r - 1;
                }
                else
                {
                    binary.Append(0);
                    num = r;
                }
            }
            return binary.ToString();
        }

        public static String PrintBinary2(double num)
        {
            if (num >= 1 || num <= 0)
            {
                return "ERROR";
            }

            StringBuilder binary = new StringBuilder();
            double frac = 0.5;
            binary.Append(".");
            while (num > 0)
            {
                /* Setting a limit on length: 32 characters */
                if (binary.Length >= 32)
                {
                    return "ERROR";
                }
                if (num >= frac)
                {
                    binary.Append(1);
                    num -= frac;
                }
                else
                {
                    binary.Append(0);
                }
                frac /= 2;
            }
            return binary.ToString();
        }



    public static void Main(String[] args)
    {
        String bs = PrintBinary(.125);
        Console.WriteLine(bs);

        for (int i = 0; i < 1000; i++)
        {
            double num = i / 1000.0;
            String binary = PrintBinary(num);
            String binary2 = PrintBinary2(num);
            if ((!(binary=="ERROR")) || (!(binary2=="ERROR")))
            {
                Console.WriteLine(num + " : " + binary + " " + binary2);
            }
        }
    }
}
