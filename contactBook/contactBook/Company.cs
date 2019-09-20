using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contactBook
{
    class Company
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public Company(string name, string address)
        {
            this.Name = name;
            this.Address = address;
        }
    }
}
