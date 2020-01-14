using CoreClient.Interface;
using CoreClient.Models;
using RestSharp;
using System.Linq;

namespace CoreClient
{
    public class ClientConfig : ICoreConfig
    {
        LiteClient _client;
        public ClientConfig(LiteClient client) {
            _client = client;
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
        
        public ProjectConfig GetConfig()
        {
           var request = new RestSharp.RestRequest("/config/get?name=" + RestState.ProjectName);
           var getMe= RestState.Client.Request<ProjectConfig>(request);
            _client.UpdateConfig(getMe);
            return getMe;    
        }
        
    }

}
