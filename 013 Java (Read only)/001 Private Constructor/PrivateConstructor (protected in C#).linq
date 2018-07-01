<Query Kind="Program" />

    public class Program
    {

        private Program()
        {
            Console.WriteLine("Q");
        }

        public class A
        {
            protected A()
            {
            }
        }

        public class B : A
        {
            public B()
            {
            }
        }


        public static void Main(String[] args)
        {
            new B();
			Console.WriteLine("If A is protected, we cannot instantiate A from Main, but can instatiate B");
			Console.WriteLine("new A(); would throw an exception");
        }
    }
