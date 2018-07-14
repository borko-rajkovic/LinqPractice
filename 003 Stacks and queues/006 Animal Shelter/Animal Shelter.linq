<Query Kind="Program" />

public class Program
{
    public abstract class Animal
    {
        protected String name;
        public Animal(String n)
        {
            name = n;
        }

        public abstract String Name();

        public int Order { get; set; }

        public Boolean IsOlderThan(Animal a)
        {
            return Order < a.Order;
        }
    }


    public class Cat : Animal
    {
        public Cat(String n) : base(n) { }

        public override string Name()
        {
            return "Cat: " + name;
        }
    }

    public class Dog : Animal
    {
        public Dog(String n) : base(n) { }

        public override String Name()
        {
            return "Dog: " + name;
        }
    }

    public class AnimalQueue
    {
        LinkedList<Dog> dogs = new LinkedList<Dog>();
        LinkedList<Cat> cats = new LinkedList<Cat>();
        private int order = 0;

        public void Enqueue(Animal a)
        {
            a.Order = order;
            order++;
            if (a is Dog)
            {
                dogs.AddLast((Dog)a);
            }
            else if (a is Cat)
            {
                cats.AddLast((Cat)a);
            }
        }

        public Animal DequeueAny()
        {
            if (dogs.Count == 0)
            {
                return DequeueCats();
            }
            else if (cats.Count == 0)
            {
                return DequeueDogs();
            }
            Dog dog = dogs.First.Value;
            Cat cat = cats.First.Value;
            if (dog.IsOlderThan(cat))
            {
                Dog firstDog = dogs.First.Value;
                dogs.RemoveFirst();
                return firstDog;
            }
            else
            {
                Cat firstCat = cats.First.Value;
                cats.RemoveFirst();
                return firstCat;
            }
        }

        public Animal Peek()
        {
            if (dogs.Count == 0)
            {
                return cats.First.Value;
            }
            else if (cats.Count == 0)
            {
                return dogs.First.Value;
            }
            Dog dog = dogs.First.Value;
            Cat cat = cats.First.Value;
            if (dog.IsOlderThan(cat))
            {
                return dog;
            }
            else
            {
                return cat;
            }
        }

        public int Size()
        {
            return dogs.Count + cats.Count;
        }

        public Dog DequeueDogs()
        {
            Dog firstDog = dogs.First.Value;
            dogs.RemoveFirst();
            return firstDog;
        }

        public Dog PeekDogs()
        {
            return dogs.First.Value;
        }

        public Cat DequeueCats()
        {
            Cat firstCat = cats.First.Value;
            cats.RemoveFirst();
            return firstCat;
        }

        public Cat PeekCats()
        {
            return cats.First.Value;
        }
    }


    public static void Main()
    {
        AnimalQueue animals = new AnimalQueue();
        animals.Enqueue(new Cat("Callie"));
        animals.Enqueue(new Cat("Kiki"));
        animals.Enqueue(new Dog("Fido"));
        animals.Enqueue(new Dog("Dora"));
        animals.Enqueue(new Cat("Kari"));
        animals.Enqueue(new Dog("Dexter"));
        animals.Enqueue(new Dog("Dobo"));
        animals.Enqueue(new Cat("Copa"));

        Console.WriteLine(animals.DequeueDogs().Name());
        Console.WriteLine(animals.DequeueDogs().Name());
        Console.WriteLine(animals.DequeueCats().Name());
        Console.WriteLine(animals.DequeueCats().Name());
        Console.WriteLine();
        Console.WriteLine(animals.DequeueAny().Name());
        Console.WriteLine(animals.DequeueAny().Name());
        Console.WriteLine(animals.DequeueAny().Name());

        animals.Enqueue(new Dog("Dapa"));
        animals.Enqueue(new Cat("Kilo"));

        while (animals.Size() != 0)
        {
            Console.WriteLine(animals.DequeueAny().Name());
        }
    }
}
