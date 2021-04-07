using System;
using System.Collections.Generic;

namespace Table
{
    class Program
    {
        static void Main(string[] args)
        {
            List<TestClass> list = new List<TestClass>();
            list.Add(new TestClass
            {
                Id = 1,
                FirstName = "Nikita",
                LastName = "Sidorenko",
                AccountType = 'M',
                Bonuses = 12,
                DateOfBirth = new DateTime(2000, 8, 23),
                Money = 322
            });

            list.Add(new TestClass
            {
                Id = 101,
                FirstName = "Nora",
                LastName = "Sevruck",
                AccountType = 'w',
                Bonuses = 123,
                DateOfBirth = new DateTime(2001, 8, 23),
                Money = 1337
            });
            var tableOfRecords = new TableOfRecords<TestClass>(list);
            tableOfRecords.PrintTable(WriteToConsole.PrintLine);
            tableOfRecords.PrintTable(WriteToFile.PrintLine);
            Console.ReadLine();
        }
    }
}
