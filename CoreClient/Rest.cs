
using CoreClient.Models;
using Newtonsoft.Json;
using RestSharp;

namespace CoreClient
{
    public class Rest 
    {
        RestSharp.RestClient _client;
        private string ProjectName { get; set; }
        private string Url { get; set; }
        private static Rest _instanse;
        private LiteClient _lite;
       public static dynamic Config { get
            {
                if(_instanse== null)
                {
                    throw new System.Exception("Rest is not Initialize");
                }
                return _instanse.GetConfig().Config;
                
            } }
        private Rest()
        {

        }
        private Rest(string url, string projectName)
        {

           _lite= new LiteClient(projectName);
            Url = url;
            ProjectName = projectName;
            _client = new RestClient(Url);

        }
        public static Rest Instanse(string url= "http://172.17.9.105:1600/api", string projectName="joha")
        {
            if(_instanse== null)
            {
                _instanse = new Rest(url, projectName);
            }
            RestState.Client = _instanse;
            return _instanse;
        }
        public MyModel GetById(int id, ModelStatus modelStatus)
        {
           var response= _lite.GetById(id, modelStatus);
            if(response!= null)
            {
                return response ;
            }
            RestRequest request = new RestSharp.RestRequest("/home/ByTraffic/",RestSharp.Method.POST);
            MyModel traffic = new MyModel();
            traffic.ProjectName = ProjectName;
            traffic.ModelStatus = ModelStatus.IntStatus;
            traffic.StatusCode = id;
            request.AddJsonBody(traffic);
           response= Request<MyModel>(request);
            _lite.SaveTraffic(response);
            return response;
        }
        //TODO Cachange
        public ProjectConfig GetConfig()
        {
           RestRequest request = new RestRequest("/config/Get?name=" + ProjectName);
            var rest= Request<ProjectConfig>(request);

            return rest;
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
        public bool LifeTime
        {
            get { return false; }
        }
                       
    }
    public class RestState
    {
        public static Rest Client { get; set; }
    }
}
