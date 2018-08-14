<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    private Dictionary<String, Byte> tagDictionary;
    private static readonly Byte[] END = { 0, 1 };

    private List<String> tokens;
    private int currentTokenIndex;

    public Program(Dictionary<String, Byte> tagDictionary) { this.tagDictionary = tagDictionary; }

    public byte[] Encode(char[] input)
    {
        // tokenize
        Tokenize(input);
        currentTokenIndex = 0;

        // parse
        MemoryStream outputStream = new MemoryStream();
        EncodeTokens(outputStream);
        return outputStream.ToArray();
    }

    private void EncodeTokens(MemoryStream output)
    {
        NextToken("<");

        // read tag name
        String tagName = NextToken();
        output.WriteByte(GetTagCode(tagName));

        // read attributes
        while (!HasNextToken(">") && !HasNextTokens("/", ">"))
        {
            // read next attribute
            String key = NextToken();
            NextToken("=");
            String value = NextToken();

            output.WriteByte(GetTagCode(key));
            foreach (char c in value.ToCharArray())
            {
                output.WriteByte((byte)c);
            }
            output.WriteByte(END[0]);
            output.WriteByte(END[1]);
        }

        // end of attributes
        output.WriteByte(END[0]);
        output.WriteByte(END[1]);

        // finish this element
        if (HasNextTokens("/", ">"))
        {
            NextToken("/");
            NextToken(">");
        }
        else
        {
            NextToken(">");
            // while not the end tag
            while (!HasNextTokens("<", "/"))
            {
                // encode child
                EncodeTokens(output);
            }
            // ending tag
            NextToken("<");
            NextToken("/");
            NextToken(tagName);
            NextToken(">");
        }

        output.WriteByte(END[0]);
        output.WriteByte(END[1]);
    }

    private String NextToken()
    {
        if (currentTokenIndex >= tokens.Count)
        {
            throw new IOException("Unexpected end of input.");
        }

        String token = tokens[currentTokenIndex];
        currentTokenIndex++;
        return token;
    }

    private void NextToken(String expectedToken)
    {
        if (currentTokenIndex >= tokens.Count)
        {
            throw new IOException("Unexpected end of input.");
        }

        String token = tokens[currentTokenIndex];
        if (token.Equals(expectedToken))
        {
            currentTokenIndex++;
        }
        else
        {
            throw new IOException("Unexpected input. Expected '"
                    + expectedToken + "'; found '" + token + "'.");
        }
    }

    private Boolean HasNextToken(String expectedToken)
    {
        if (currentTokenIndex < tokens.Count)
        {
            return tokens[currentTokenIndex].Equals(expectedToken);
        }
        else
        {
            return false;
        }
    }

    private Boolean HasNextTokens(params string[] expectedTokens)
    {
        if (currentTokenIndex + expectedTokens.Length > tokens.Count)
        {
            return false;
        }

        for (int i = 0; i < expectedTokens.Length; i++)
        {
            if (!tokens[currentTokenIndex + i]
                    .Equals(expectedTokens[i])) return false;
        }
        return true;
    }

    private void Tokenize(char[] input)
    {
        tokens = new List<String>();
        int i = 0;
        while (i < input.Length)
        {
            i = SetNextToken(input, i);
        }
    }

    private int SetNextToken(char[] input, int inputIndex)
    {
        int i = inputIndex;
        while (i < input.Length && input[i] == ' ') i++;
        if (i == input.Length) return i;

        // get 1 char token
        char c = input[i];
        if (c == '<' || c == '>' || c == '=' || c == '/')
        {
            tokens.Add(c.ToString());
            return i + 1;
        }

        // get multiple char token
        StringBuilder s = new StringBuilder();
        do
        {
            s.Append(c);
            i++;
            c = input[i];
            if (c == '<' || c == '>' || c == '=' ||
                c == '/' || c == ' ')
            {
                break;
            }
        } while (i < input.Length);
        tokens.Add(s.ToString());
        return i;
    }

    private byte GetTagCode(String tag)
    {
        Byte? tagCode = tagDictionary[tag];
        if (tagCode == null)
        {
            throw new IOException("Unknown tag: " + tag);
        }
        return (byte)tagCode;
    }

    public static void Print(byte[] output)
    {
        foreach (byte b in output)
        {
            Console.Write(b);
            Console.Write(" ");
        }
        Console.WriteLine();
    }

    public static void Main(String[] args)
    {
        try
        {
            Dictionary<String, Byte> tagDictionary = new Dictionary<String, Byte>
            {
                { "a", 10 },
                { "root", 11 },
                { "href", 20 },
                { "target", 21 },
                { "name", 50 },
                { "id", 51 }
            };

            Program encoder = new Program(tagDictionary);
            String input;
            byte[] output;

            input = "<root></root>";
            output = encoder.Encode(input.ToCharArray());
            Print(output);

            input = "<root id=a />";
            output = encoder.Encode(input.ToCharArray());
            Print(output);

            input = "<root><a href=abc id=xyz></a><a></a></root>";
            output = encoder.Encode(input.ToCharArray());
            Print(output);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
