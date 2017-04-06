using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wen.Helpers.Common;

namespace Wen.Helpers.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var redis = new RedisHelper(12);
            redis.StringSet("test1:test2", new Student() { Id = 1, Name = "Wen" });
            var data = redis.StringGet<Student>("test");
            Console.WriteLine($"{data.Id} {data.Name}");
            Console.WriteLine("ok");

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
