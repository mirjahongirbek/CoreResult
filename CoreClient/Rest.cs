
using CoreClient.Models;
using Newtonsoft.Json;
using RestSharp;

namespace CoreClient
{
    public class Rest
    {
        RestSharp.RestClient _client;
        private string ProjectName { get; set; }
        private string Url { get { return "http://172.17.9.105:1600/api"; } }
        private static Rest _instanse;
        private Rest()
        {

        }
        private Rest(string url, string projectName)
        {
            ProjectName = projectName;
            _client = new RestClient(Url);
        }
        public static Rest Instanse(string url, string projectName)
        {
            if(_instanse== null)
            {
                _instanse = new Rest(url, projectName);
            }
            return _instanse;
        }
        public object GetById(int id)
        {
            RestRequest request = new RestSharp.RestRequest("/home/ByTraffic/",RestSharp.Method.POST);
            Traffic traffic = new Traffic();
            traffic.ProjectName = ProjectName;
            traffic.StatusCode = id;
            request.AddJsonBody(traffic);
           var response= Request<Traffic>(request);
            return null;
        }
        public T Request<T>(RestRequest request)
            where T:class
        {
           var response= _client.Execute(request);
            if (response.IsSuccessful)
            {
                return  JsonConvert.DeserializeObject<T>(response.Content);
            }
            return null;
        }
        private T GetFromData<T>(int Id)
            where T:class
        {
            return null;
        }
        public bool LifeTime
        {
            get { return false; }
        }
        
        private void AddTraffic(Traffic traffic)
        {
            
        }
        private void AddConfig(ProjectConfig config)
        {

        }
        
               
    }
}
