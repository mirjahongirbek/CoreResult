using CoreClient;
using System;

namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Rest client = Rest.Instanse("", "ddd");
         var response=   client.GetById(11);
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
