<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public class Person
    {
        public int birth;
        public int death;
        public Person(int birthYear, int deathYear)
        {
            birth = birthYear;
            death = deathYear;
        }
    }

    public static int MaxAliveYear(Person[] people, int min, int max)
    {
        int maxAlive = 0;
        int maxAliveYear = min;

        for (int year = min; year <= max; year++)
        {
            int alive = 0;
            foreach (Person person in people)
            {
                if (person.birth <= year && year <= person.death)
                {
                    alive++;
                }
            }
            if (alive > maxAlive)
            {
                maxAlive = alive;
                maxAliveYear = year;
            }
        }

        return maxAliveYear;
    }

    public static void Main(String[] args)
    {
        int n = 10000;
        int first = 0;
        int last = 200000;
        Random random = new Random();
        Person[] people = new Person[n];
        for (int i = 0; i < n; i++)
        {
            int birth = first + random.Next(last - first);
            int death = birth + random.Next(last - birth);
            people[i] = new Person(birth, death);
            //Console.WriteLine(birth + ", " + death);
        }

        Console.WriteLine(n);
        for (int i = 0; i < n; i++)
        {
            //int birth = first + random.nextInt(last - first);
            //int death = birth + random.nextInt(last - birth);
            //people[i] = new Person(birth, death);
            Console.WriteLine(people[i].birth);
        }
        Console.WriteLine(n);
        for (int i = 0; i < n; i++)
        {
            //int birth = first + random.nextInt(last - first);
            //int death = birth + random.nextInt(last - birth);
            //people[i] = new Person(birth, death);
            Console.WriteLine(people[i].death);
        }

        int year = MaxAliveYear(people, first, last);
        Console.WriteLine(year);

    }
}
