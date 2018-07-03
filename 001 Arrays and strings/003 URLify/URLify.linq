<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    // Assume string has sufficient free space at the end
    public static void ReplaceSpaces(char[] str, int trueLength)
    {
        int spaceCount = 0, index, i = 0;
        for (i = 0; i < trueLength; i++)
        {
            if (str[i] == ' ')
            {
                spaceCount++;
            }
        }
        index = trueLength + spaceCount * 2;
        if (trueLength < str.Length) str[trueLength] = '\0';
        for (i = trueLength - 1; i >= 0; i--)
        {
            if (str[i] == ' ')
            {
                str[index - 1] = '0';
                str[index - 2] = '2';
                str[index - 3] = '%';
                index = index - 3;
            }
            else
            {
                str[index - 1] = str[i];
                index--;
            }
        }
    }

    public static int FindLastCharacter(char[] str)
    {
        for (int i = str.Length - 1; i >= 0; i--)
        {
            if (str[i] != ' ')
            {
                return i;
            }
        }
        return -1;
    }

    public static void Main(String[] args)
    {
        String str = "Mr John Smith    ";
        char[] arr = str.ToCharArray();
        int trueLength = FindLastCharacter(arr) + 1;
        ReplaceSpaces(arr, trueLength);
        Console.WriteLine("\"" + new string(arr) + "\"");
    }
}
