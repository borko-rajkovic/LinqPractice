[What are the differences between Generics in C# and Java… and Templates in C++?][stackoverflow]
[Differences Between C++ Templates and C# Generics][microsoftlink]
[Anders interview about templates and generics][artima]
[How do C# generics compare to C++ templates][msdnblog]

# C# Generics allow you to declare something like this.
```sh
List<Person> foo = new List<Person>();
```
and then the compiler will prevent you from putting things that aren't Person into the list.
Behind the scenes the C# compiler is just putting List<Person> into the .NET dll file, but at runtime the JIT compiler goes and builds a new set of code, as if you had written a special list class just for containing people - something like ListOfPerson.

The benefit of this is that it makes it really fast. There's no casting or any other stuff, and because the dll contains the information that this is a List of Person, other code that looks at it later on using reflection can tell that it contains Person objects (so you get intellisense and so on).

The downside of this is that old C# 1.0 and 1.1 code (before they added generics) doesn't understand these new List<something>, so you have to manually convert things back to plain old List to interoperate with them. This is not that big of a problem, because C# 2.0 binary code is not backwards compatible. The only time this will ever happen is if you're upgrading some old C# 1.0/1.1 code to C# 2.0

# Java Generics allow you to declare something like this.
```sh
ArrayList<Person> foo = new ArrayList<Person>();
```
On the surface it looks the same, and it sort-of is. The compiler will also prevent you from putting things that aren't Person into the list.

The difference is what happens behind the scenes. Unlike C#, Java does not go and build a special ListOfPerson - it just uses the plain old ArrayList which has always been in Java. When you get things out of the array, the usual Person p = (Person)foo.get(1); casting-dance still has to be done. The compiler is saving you the key-presses, but the speed hit/casting is still incurred just like it always was.
When people mention "Type Erasure" this is what they're talking about. The compiler inserts the casts for you, and then 'erases' the fact that it's meant to be a list of Person not just Object

The benefit of this approach is that old code which doesn't understand generics doesn't have to care. It's still dealing with the same old ArrayList as it always has. This is more important in the java world because they wanted to support compiling code using Java 5 with generics, and having it run on old 1.4 or previous JVM's, which microsoft deliberately decided not to bother with.

The downside is the speed hit I mentioned previously, and also because there is no ListOfPerson pseudo-class or anything like that going into the .class files, code that looks at it later on (with reflection, or if you pull it out of another collection where it's been converted into Object or so on) can't tell in any way that it's meant to be a list containing only Person and not just any other array list.

# C++ Templates allow you to declare something like this
```sh
std::list<Person>* foo = new std::list<Person>();
```
It looks like C# and Java generics, and it will do what you think it should do, but behind the scenes different things are happening.

It has the most in common with C# generics in that it builds special pseudo-classes rather than just throwing the type information away like java does, but it's a whole different kettle of fish.

Both C# and Java produce output which is designed for virtual machines. If you write some code which has a Person class in it, in both cases some information about a Person class will go into the .dll or .class file, and the JVM/CLR will do stuff with this.

C++ produces raw x86 binary code. Everything is not an object, and there's no underlying virtual machine which needs to know about a Person class. There's no boxing or unboxing, and functions don't have to belong to classes, or indeed anything.

Because of this, the C++ compiler places no restrictions on what you can do with templates - basically any code you could write manually, you can get templates to write for you.
The most obvious example is adding things:

In C# and Java, the generics system needs to know what methods are available for a class, and it needs to pass this down to the virtual machine. The only way to tell it this is by either hard-coding the actual class in, or using interfaces. For example:

```sh
string addNames<T>( T first, T second ) { return first.Name() + second.Name(); }
```

That code won't compile in C# or Java, because it doesn't know that the type T actually provides a method called Name(). You have to tell it - in C# like this:
```sh
interface IHasName{ string Name(); };
string addNames<T>( T first, T second ) where T : IHasName { .... }
```
And then you have to make sure the things you pass to addNames implement the IHasName interface and so on. The java syntax is different (<T extends IHasName>), but it suffers from the same problems.

The 'classic' case for this problem is trying to write a function which does this

```sh
string addNames<T>( T first, T second ) { return first + second; }
```
You can't actually write this code because there are no ways to declare an interface with the + method in it. You fail.

C++ suffers from none of these problems. The compiler doesn't care about passing types down to any VM's - if both your objects have a .Name() function, it will compile. If they don't, it won't. Simple.

# The following are the key differences between C# Generics and C++ templates:

- C# generics do not provide the same amount of flexibility as C++ templates. For example, it is not possible to call arithmetic operators in a C# generic class, although it is possible to call user defined operators.

- C# does not allow non-type template parameters, such as template C<int i> {}.

- C# does not support explicit specialization; that is, a custom implementation of a template for a specific type.

- C# does not support partial specialization: a custom implementation for a subset of the type arguments.

- C# does not allow the type parameter to be used as the base class for the generic type.

- C# does not allow type parameters to have default types.

- In C#, a generic type parameter cannot itself be a generic, although constructed types can be used as generics. C++ does allow template parameters.

- C++ allows code that might not be valid for all type parameters in the template, which is then checked for the specific type used as the type parameter. C# requires code in a class to be written in such a way that it will work with any type that satisfies the constraints. For example, in C++ it is possible to write a function that uses the arithmetic operators + and - on objects of the type parameter, which will produce an error at the time of instantiation of the template with a type that does not support these operators. C# disallows this; the only language constructs allowed are those that can be deduced from the constraints.

[stackoverflow]: <https://stackoverflow.com/questions/31693/what-are-the-differences-between-generics-in-c-sharp-and-java-and-templates-i>
[microsoftlink]: <https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/differences-between-cpp-templates-and-csharp-generics>
[artima]:<https://www.artima.com/intv/generics2.html>
[msdnblog]: <https://blogs.msdn.microsoft.com/csharpfaq/2004/03/12/how-do-c-generics-compare-to-c-templates/>
