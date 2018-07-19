<Query Kind="Program" />

public class Program
{
	public static string fileName = "input.txt";

	public static int FindOpenNumber(String filename)
	{
		int rangeSize = (1 << 20); // 2^20 bits (2^17 bytes)

		/* Get count of number of values within each block. */
		int[] blocks = GetCountPerBlock(filename, rangeSize);

		/* Find a block with a missing value. */
		int blockIndex = FindBlockWithMissing(blocks, rangeSize);
		if (blockIndex < 0) return -1;

		/* Create bit vector for items within this range. */
		byte[] bitVector = GetBitVectorForRange(filename, blockIndex, rangeSize);

		/* Find a zero in the bit vector */
		int offset = FindZero(bitVector);
		if (offset < 0) return -1;

		/* Compute missing value. */
		return blockIndex * rangeSize + offset;
	}

	/* Get count of items within each range. */
	public static int[] GetCountPerBlock(String filename, int rangeSize)
	{

		int arraySize = int.MaxValue / rangeSize + 1;
		int[] blocks = new int[arraySize];

		const Int32 BufferSize = 128;
		using (var fileStream = File.OpenRead(fileName))
		using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
		{
			String line;
			while ((line = streamReader.ReadLine()) != null)
			{
				int value = Int32.Parse(line);
				blocks[value / rangeSize]++;
			}
		}

		return blocks;
	}


	/* Find a block whose count is low. */
	public static int FindBlockWithMissing(int[] blocks, int rangeSize)
	{
		for (int i = 0; i < blocks.Length; i++)
		{
			if (blocks[i] < rangeSize)
			{
				return i;
			}
		}
		return -1;
	}


	/* Create a bit vector for the values within a specific range. */
	public static byte[] GetBitVectorForRange(String filename, int blockIndex, int rangeSize)
	{

		int startRange = blockIndex * rangeSize;
		int endRange = startRange + rangeSize;
		byte[] bitVector = new byte[rangeSize / 8 /*size of byte*/];

		const Int32 BufferSize = 128;
		using (var fileStream = File.OpenRead(fileName))
		using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
		{
			String line;
			while ((line = streamReader.ReadLine()) != null)
			{
				/* If the number is inside the block that's missing 
				 * numbers, we record it */
				int value = Int32.Parse(line);
				if (startRange <= value && value < endRange)
				{
					int offset = value - startRange;
					byte mask = (byte)(1 << (offset % 8 /*size of byte*/));
					bitVector[offset / 8 /*size of byte*/] |= mask;
				}

			}
		}

		return bitVector;
	}


	/* Find bit index that is 0 within byte. */
	public static int FindZero(byte b)
	{
		for (int i = 0; i < 8 /*size of byte*/; i++)
		{
			int mask = 1 << i;
			if ((b & mask) == 0)
			{
				return i;
			}
		}
		return -1;
	}

	/* Find a zero within the bit vector and return the index. */
	public static int FindZero(byte[] bitVector)
	{
		for (int i = 0; i < bitVector.Length; i++)
		{
			if (bitVector[i] != unchecked((byte)(~0)))
			{ // If not all 1s
				int bitIndex = FindZero(bitVector[i]);
				return i * 8 /*size of byte*/ + bitIndex;
			}
		}
		return -1;
	}


	public static void Main(String[] args)
	{
		int max = 10000000;
		int missing = 1234325;
		Console.WriteLine("Generated file from 0 to " + max + " with " + missing + " missing.");
		Console.WriteLine("Searching for missing number...");
		Console.WriteLine("Missing value: " + FindOpenNumber(fileName));
	}
}
