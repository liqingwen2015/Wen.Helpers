using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wen.Helpers.Common;
using Wen.Helpers.Common.Npoi;

namespace Wen.Helpers.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var columnNames = new List<string>()
            {
                "Id",
                "Name"
            };

            var students = new List<Student>();
            for (int i = 0; i < 10; i++)
            {
                students.Add(new Student()
                {
                    Id = i + 1,
                    Name = i.ToString()
                });
            }

            NpoiHelepr.WriteExcel("01", columnNames, students, @"E:\== Temp ==\1.xlsx");
            Console.WriteLine("OK");
            Console.Read();
        }
    }

    [Serializable]
    class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
