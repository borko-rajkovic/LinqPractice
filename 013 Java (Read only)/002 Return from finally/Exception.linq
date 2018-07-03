<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

public static void TestFinallyException(){

for(int i = 0; i< 10; i++)
	try {
		if(i==5) throw new Exception("Number is 5");
		Console.WriteLine(i);
	}
	catch(Exception ex){
		Console.WriteLine("Exception: "+ex.Message);
	}
	finally{
		Console.WriteLine("Finally executed");
	}
}

public static void Main()
{
	TestFinallyException();
}
