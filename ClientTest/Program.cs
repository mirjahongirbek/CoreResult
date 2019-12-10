using CoreClient;
using CoreResults;
using RepositoryCore.Models;
using System;

namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Rest client = Rest.Instanse("", "ddd");
            CoreState.Rest = client;
           var ddd= sRest();
            
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
       static NetResult<Result> sRest()
        {
            return 11;
        }
    }
}
