<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

public static void TestFinallyContinue(){

for(int i = 0; i< 10; i++)
	try {
		if(i==5) continue;
		Console.WriteLine(i);
	}
	finally{
		Console.WriteLine("Finally executed");
	}
}

public static void Main()
{
	TestFinallyContinue();
}
