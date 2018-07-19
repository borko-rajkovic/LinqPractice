<Query Kind="Program" />

public class Program
{
    public static long numberOfInts = ((long)int.MaxValue) + 1;
    public static byte[] bitfield = new byte[(int)(numberOfInts / 8)];
    public static string fileName = "input.txt";

    public static void FindOpenNumber()
    {
        const Int32 BufferSize = 128;
        using (var fileStream = File.OpenRead(fileName))
        using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
        {
            String line;
            while ((line = streamReader.ReadLine()) != null)
            {
                /* Finds the corresponding number in the bitfield by using
                 * the OR operator to set the nth bit of a byte 
                 * (e.g., 10 would correspond to bit 2 of index 1 in
                 * the byte array). */

                int n = Int32.Parse(line);
                bitfield[n / 8] |= (byte)(1 << (n % 8));
            }
        }

        for (int i = 0; i < bitfield.Length; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                /* Retrieves the individual bits of each byte. When 0 bit
                 * is found, finds the corresponding value. */
                if ((bitfield[i] & (1 << j)) == 0)
                {
                    Console.WriteLine(i * 8 + j);
                    return;
                }
            }
        }
    }

    public static void Main(String[] args)
    {
        FindOpenNumber();
    }
}
