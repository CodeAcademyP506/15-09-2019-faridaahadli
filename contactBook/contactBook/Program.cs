using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace contactBook
{
    class Program
    {
        public delegate void WriteLine(string pathPhone, string PathBack);
        public static List<Person> phoneDirectory = new List<Person>()
        {
            new Person("Farida","Ahadli",123456,new Company("Azercell","Ganjlik")),
             new Person("Lale","Humbetova",424299,new Company("Baku Electronics","Memar Ecemi"))
        };
        static void Main(string[] args)
        {
            string pathPhone = @"C:\Users\HP\Desktop\New folder\phone.txt";
            string pathBack = @"C:\Users\HP\Desktop\New folder\phoneBackup.txt";
            if (!File.Exists(pathPhone))
            {
                File.Create(pathPhone);
            }

            if (!File.Exists(pathBack))
            {
                File.Create(pathBack);
            }

            WriteLine writer = new WriteLine(SwWriteline);
            writer += ConsWriteline;
           
            
            
            while (true)
            {
                Cons();
                string choose = Console.ReadLine();
                if (choose == "1")
                {
                    Console.WriteLine("Pls,Enter Name, Surname, Phone number, Company, Company address:");
                    try
                    {
                        Person person = new Person(Console.ReadLine(), Console.ReadLine(), Convert.ToInt32(Console.ReadLine()), new Company(Console.ReadLine(), Console.ReadLine()));
                        if (!phoneDirectory.Any(p => p.Name == person.Name && p.Surname == person.Surname))
                            phoneDirectory.Add(person);
                        else
                            Console.WriteLine("This person already exists.");

                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"{ex.Message} You must enter the phone number.");
                    }

                    writer(pathPhone, pathBack);
                }
                if (choose == "3")
                {
                    ConsWriteline(pathPhone, pathBack);
                }
                if (choose == "2")
                {
                    Console.WriteLine("Pls,Enter Name and Surname:");
                    string name = Console.ReadLine().ToLower();
                    string surname = Console.ReadLine().ToLower();
                    try
                    {
                        var person = phoneDirectory.First(p => p.Name.ToLower() == name && p.Surname.ToLower() == surname);
                        phoneDirectory.Remove(person);
                        writer(pathPhone, pathBack);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                if (choose == "4")
                {
                    Console.WriteLine("Pls,write the word which you want to search:");
                    Search();
                }
                if (choose == "5")
                {
                    Console.WriteLine("Pls,write name and surname:");
                    Edit(Console.ReadLine(), Console.ReadLine());
                }
            }



            void SwWriteline(string path1, string path2)
            {

                StreamWriter sw = new StreamWriter(path1);
                using (sw)
                {

                    foreach (Person person in phoneDirectory)
                    {
                        sw.WriteLine($"{person.Name},{person.Surname},{person.Number},{person.Company.Name},{person.Company.Address}");
                    }
                }
                File.Copy(path1, path2, true);
            }
            void ConsWriteline(string path1, string Path2)
            {
                phoneDirectory.Clear();
                using (StreamReader sr = new StreamReader(path1))
                {
                    var data = sr.ReadLine();
                    while (data != null)
                    {
                        string[] datas = data.Split(',');
                        Person person = new Person(datas[0], datas[1], Convert.ToInt32(datas[2]), new Company(datas[3], datas[4]));
                        Console.WriteLine($"Name: {person.Name}, Surname: {person.Surname}, Phone: {person.Number}, Company: {person.Company.Name}, Company address: {person.Company.Address} ");
                        phoneDirectory.Add(person);

                        data = sr.ReadLine();
                    }
                }

            }
            void Search()
            {
                using (StreamReader sr = new StreamReader(pathPhone))
                {
                    var search = Console.ReadLine().ToLower().Split(' ');
                    List<Person> searchList = new List<Person>();
                    var data = sr.ReadLine();
                    while (data != null)
                    {
                        for (int i = 0; i < search.Length; i++)
                        {

                            if (data.ToLower().Contains(search[i]))
                            {
                                string[] datas = data.Split(',');
                                Person person = new Person(datas[0], datas[1], Convert.ToInt32(datas[2]), new Company(datas[3], datas[4]));
                                if (!searchList.Any(p=>p.Name==person.Name && p.Surname==person.Surname))
                                {
                                    searchList.Add(person);
                                    Console.WriteLine($"Name: {person.Name}, Surname: {person.Surname}, Phone: {person.Number}, Company: {person.Company.Name}, Company address: {person.Company.Address} ");
                                }
                            }

                        }


                        data = sr.ReadLine();
                    }
                }
            }
            void Edit(string name,string surname)
            {
                if (phoneDirectory.Any(p => p.Name.ToLower() == name.ToLower() && p.Surname.ToLower() == surname.ToLower()))
                {
                    var editObject = phoneDirectory.First(p => p.Name.ToLower() == name.ToLower() && p.Surname.ToLower() == surname.ToLower());
                    Console.WriteLine("Which property do you want to change. Choose one of them:\n1.Name\n2.Surname\n3.Phone number\n4.Company name\n5.Company address");
                    var ans = Console.ReadLine();
                    Console.WriteLine("Enter word");
                    var word = Console.ReadLine();
                    switch (ans)
                    {

                        case "1":
                            editObject.Name = word;
                            break;
                        case "2":
                            editObject.Surname = word;
                            break;
                        case "3":
                            try
                            {
                                editObject.Number = Convert.ToInt32(word);
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine($"{ex.Message} You must enter the phone number.");
                            }

                            break;
                        case "4":
                            editObject.Company.Name = word;
                            break;
                        case "5":
                            editObject.Company.Address = word;
                            break;
                        default:
                            Console.WriteLine("This operation isn't exist");
                            break;

                    }
                    writer(pathPhone, pathBack);
                }
                else
                    Console.WriteLine("This person couldn't find ");
            }
        }
        public static void Cons()
        {
            Console.WriteLine("1.Elave et");
            Console.WriteLine("2.Sil");
            Console.WriteLine("3.Siyahiya bax");
            Console.WriteLine("4.Siyahida axtar");
            Console.WriteLine("5.Deyis gosterilsin");
        }
    }
}
