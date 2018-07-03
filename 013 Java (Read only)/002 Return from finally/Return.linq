<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

public static int TestFinallyReturn(){
	try {
		return 1;
	}
	finally{
		Console.WriteLine("Finally executed");
	}
}

public static void Main()
{
	int a = TestFinallyReturn();
	Console.WriteLine(a);
}
