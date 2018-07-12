<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

// (n & (n-1) == 0) checks if number is power of 2 or is 0 (have at most one bit of value 1)
//
//
//
// A & B == 0
//
// will be true if they do not share any bit with value 1;
// because only bits with 1 in same place will give 1 at the outut
//
// N-1 will change right-most part of N up to and including first bit of value 1
//
// Ex: 
//
// N   = 10001101
// N-1 = 10001100
//
// N   = 10001100
// N-1 = 10001011
//
// In general:
//
// N   = abcd1000
// N-1 = abcd0111
//
//
// In order for ((n & (n - 1)) == 0) to be true,
// left part of general expression needs to be all zeros (abcd), otherwise there will be at least one bit of value 1 passing by
//
//
// Because all digits prior to last digit of value 1 followed by 0 or more digits of value 0 must be all zeros,
// this means there can be at most one bit with value of 1, or n==0
//
// In other words, n must be either 0 or some power of 2

public static Boolean fun(int n){
	return (n & (n-1))==0;
}

public static void Main()
{
	Console.WriteLine(fun(0));
	Console.WriteLine(fun(1));
	Console.WriteLine(fun(2));
	Console.WriteLine(fun(3));
	Console.WriteLine(fun(4));
	Console.WriteLine(fun(8));
	Console.WriteLine(fun(15));
	Console.WriteLine(fun(16));
}
