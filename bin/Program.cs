using System;
using System.Linq;
using System.Collections.Generic;

namespace MyAppCshrp
{
    class Mail
    {
        public String Name { get; set; }
        public int Age { get; set; }
        public String Home { get; set; }

        public override String ToString()
        {
            return String.Format("Name: {0}, Age: {1}, Address: {2}", Name, Age, Home);
        }
    }
    class Address
    {
        public int Id { get; set; }
        public String Home { get; set; }

        public override String ToString()
        {
            return String.Format("Address: {0}", Home);
        }
    }
    class Person
    {
        public String Name { get; set; }
        public int Age { get; set; }
        public int AddressId { get; set; }

        public override String ToString()
        {
            return String.Format("Name: {0}, Age: {1}", Name, Age.ToString("D2"));
        }
    }
    class MainClass
    {
        private static void print(IEnumerable<Mail> result, String query)
        {
            Console.WriteLine("[QUERY] {0}", query);
            foreach (Mail i in result)
                Console.WriteLine("[OUTPUT] {0}", i);
        }
        private static void greaterThan(List<Person> people, List<Address> addresses)
        {
            var result = (from p in people
                          join a in addresses on p.AddressId equals a.Id
                          where p.Age > 1
                          select new Mail() { Name = p.Name, Age = p.Age, Home = a.Home });
            print(result, "SELECT * FROM people as p JOIN address as a ON a.Id = p.AddressId WHERE p.Age > 1;");
        }
        private static void lessThan(List<Person> people, List<Address> addresses)
        {
            var result = (from p in people
                          join a in addresses on p.AddressId equals a.Id
                          where p.Age < 2
                          select new Mail() { Name = p.Name, Age = p.Age, Home = a.Home });
            print(result, "SELECT * FROM people as p JOIN address as a ON a.Id = p.AddressId WHERE p.Age < 2;");
        }
        private static void equals(List<Person> people, List<Address> addresses)
        {
            var result = (from p in people
                          join a in addresses on p.AddressId equals a.Id
                          where p.Age == 1
                          select new Mail() { Name = p.Name, Age = p.Age, Home = a.Home });
            print(result, "SELECT * FROM people as p JOIN address as a ON a.Id = p.AddressId WHERE p.Age = 1;");
        }
        public static void Main(string[] args)
        {
            List<Person> people = new List<Person>() {
                        new Person(){Name="Adam", Age=0, AddressId=0},
                        new Person(){Name="Betty", Age=1, AddressId=1},
                        new Person(){Name="Charlie", Age=2, AddressId=0}
            };

            List<Address> addresses = new List<Address>()
            {
                new Address(){Id=0, Home="100 Main"},
                new Address(){Id=1, Home="1600 Penn"}
            };

            Console.WriteLine("[INPUT] {0}", String.Join<Person>(", ", people.ToArray()));
            Console.WriteLine("[INPUT] {0}", String.Join<Address>(", ", addresses.ToArray()));
            greaterThan(people,addresses);
            lessThan(people, addresses);
            equals(people, addresses);
        }
    }
}
