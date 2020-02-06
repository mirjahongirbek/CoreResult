using CoreClient.Interface;
using CoreClient.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoreClient
{
    public class ClientConfig : ICoreConfig
    {
        LiteClient _client;
        public ClientConfig(LiteClient client) {
            _client = client;
            GetConfig();
        }
        public  string ToString(string key)
        {
            var conf = GetConfig();
            var real = conf.Config.FirstOrDefault(m => m.Key == key);
            if (string.IsNullOrEmpty(real.Value))
            {
                return "";
            }
            return real.Value;
        }
        
        public ProjectConfig GetServer()
        {
           var request = new RestSharp.RestRequest("/MyRest/GetConfig?name=" + RestState.ProjectName);
               var getMe= RestState.Client.Request<ProjectConfig>(request);
            _client.UpdateConfig(getMe);
            RestState.ProjectConfig = getMe;
            return getMe;    
        }
        public ProjectConfig GetConfig()
        {
            if(RestState.ProjectConfig== null)
            {
                return GetServer();
            }
            if (RestState.ConfDateTime.AddMinutes(5) < DateTime.Now)
            {
                Task.Factory.StartNew(()=>GetServer());
            }
            return RestState.ProjectConfig;
        }
        
    }

}
