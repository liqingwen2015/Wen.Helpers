using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Wen.Helpers.Common;
using Wen.Helpers.Common.Npoi;

namespace Wen.Helpers.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var artical = "There are people you love, your hobbies, triumphs, successes, and happiness. There is so much more than stress and fear. Stress does not make you better and fear does not make you happier. Many women suffer from chronic stress because they let it happen. They let stress control their minds and thus their lives. Fear is a complicated thing, but human beings – especially we women – have enough power to keep it at bay.";
            var dict = CountWrods(artical);

            foreach (var i in dict)
            {
                Console.WriteLine($"{i.Key}-{i.Value}");
            }
            Console.WriteLine(dict.Count);
            Console.Read();
        }


    }
}
