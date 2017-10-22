#region namespaces

using System;
using System.Threading.Tasks;
using Wen.Helpers.Extend;
using Wen.Helpers.Http;
using Wen.Helpers.Log;

#endregion

namespace Wen.Helpers.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SuperLogger.Register();
            SuperLogger.Debug("Debug");
            SuperLogger.Info("Info");
            Console.Read();
        }


    }
}