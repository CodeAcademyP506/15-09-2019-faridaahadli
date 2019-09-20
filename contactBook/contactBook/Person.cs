using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contactBook
{
    class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Number { get; set; }
        public Company Company { get; set; }
        public Person(string name, string surname, int number, Company company)
        {
            this.Name = name;
            this.Surname = surname;
            this.Number = number;
            this.Company = company;
        }
    }
}
