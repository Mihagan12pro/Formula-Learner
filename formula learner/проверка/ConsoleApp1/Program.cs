using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Runtime.Remoting.Messaging;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
           //StreamReader sr = new StreamReader("C:\\Users\\kargi\\OneDrive\\Рабочий стол\\formula learner\\ProgrammFiles\\for tests\\formulas.txt");
           //StreamReader sr2 = new StreamReader(@"C:\Users\kargi\OneDrive\Рабочий стол\formula learner\ProgrammFiles\for tests\tittles.txt");

            List<string> list1 = new List<string>();
            List <string> list2 = new List<string>();


            using (StreamReader sr = new StreamReader("C:\\Users\\kargi\\OneDrive\\Рабочий стол\\formula learner\\ProgrammFiles\\for tests\\formulas.txt"))
            {

                string line="";
                
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        list1.Add(line);
                        Console.WriteLine(line);
                    }
                


            }
            Console.WriteLine();
            foreach(var i in  list1) 
            {
                
                string a;
                a = Console.ReadLine();
                if (i==a)
                {
                    Console.WriteLine(i);
                }

            }

                


            Console.ReadKey();
        }
    }
}
