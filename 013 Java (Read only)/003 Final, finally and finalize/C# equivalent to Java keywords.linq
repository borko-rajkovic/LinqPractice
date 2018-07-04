<Query Kind="Program" />

/*

final ->

variable = 	readonly (essentially const, but evaluated at runtime, so allows expressions)
			-	value type - value cannot change
			-	reference type - pointer cannot change
class = sealed
methods = sealed

finally -> try/catch block
finalize -> dispose for "using" statement; finalize in case to clear object when application is not referencing it (if used in thread)

*/

public class A{
	public int a;
	public int b;
}

public readonly A aTest = new A(){a=1,b=5};
public readonly int intTest = DateTime.Now.Second;

public void Main(){
	A aNew = new A(){a=0,b=1};
	Console.WriteLine(intTest);
	//cannot compile next line
	//intTest=1;
	Console.WriteLine(aTest.b);
	aTest.b=4;
	Console.WriteLine(aTest.b);
	//cannot compile next line
	//aTest=aNew;
}
