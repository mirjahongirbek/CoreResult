using CoreClient.Models;
using System;

namespace CoreClient
{
    public class RestState
    {
        public static Rest Client { get; set; }
        public static string ProjectName { get; set; }
        public static string Url { get; set; }
        public static DateTime ConfDateTime { get; set; }
        public static ProjectConfig ProjectConfig { get; set; }

    }
    
}
