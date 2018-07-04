<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

void Main()
{
	int n = 1775;
	uint m = (uint) n;
	Console.WriteLine(n);
	Console.WriteLine(Convert.ToString(n, 2));
	Console.WriteLine();
	Console.WriteLine(m);
	Console.WriteLine(Convert.ToString(m, 2));

	Console.WriteLine();
	Console.WriteLine();
	Console.WriteLine();
	Console.WriteLine();
	
	n-=2898;
	m = (uint) n;
	Console.WriteLine(n);
	Console.WriteLine(Convert.ToString(n, 2));
	Console.WriteLine();
	Console.WriteLine(m);
	Console.WriteLine(Convert.ToString(m, 2));
}

// Define other methods and classes here
