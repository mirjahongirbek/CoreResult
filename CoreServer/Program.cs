using CoreServer;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Diagnostics;

public class Program
{
    public static void Main(string[] args)
    {
        

        try
        {
           
            Console.Title = "Core Server";
            Console.WriteLine($@"Process Id: {Process.GetCurrentProcess().Id}");
            CreateWebHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
       
        }
        finally
        {
       
        }
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
        return WebHost.CreateDefaultBuilder(args)
            .UseUrls("http://*:1600")
            .UseStartup<Startup>();
    }

    //      UseUrls("http://*:2500")
}