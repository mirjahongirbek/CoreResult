using CoreClient.Interface;
using CoreClient.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RepositoryCore.Models;
using RestSharp;
using RestSharp.Authenticators;
using System;

namespace CoreClient
{
    public class Rest 
    {
        RestSharp.RestClient _client;
        private string ProjectName { get; set; }
        private string Url { get; set; }
        private static Rest _instanse;
        private LiteClient _lite;
        public ClientConfig _config;
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
        private Rest(string url, string projectName, IServiceCollection services)
        {

           _lite= new LiteClient(projectName);
            Url = url;
            ProjectName = projectName;
            _client = new RestClient(Url);
            RestState.Client = this;
            ICoreConfig config = new ClientConfig(_lite );
            services.AddSingleton(_lite);
            services.AddSingleton(config);

        }

        public static Rest Instanse(string url, string projectName, IServiceCollection services,  string login="", string password = "")
        {
            
            if (_instanse == null)
            {
                RestState.ProjectName = projectName;
                RestState.Url = url;
                _instanse = new Rest(url, projectName, services);
            }
            if(!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
            {
                _instanse._client.Authenticator = new HttpBasicAuthenticator(login, password);
            }
           
            return _instanse;

        }
        public static Rest Instanse(IServiceCollection services, string url= "http://172.17.9.105:1600/api", string projectName="joha"
            )
        {

            RestState.ProjectName = projectName;
            RestState.Url = url;
            if (_instanse== null)
            {
                _instanse = new Rest(url, projectName, services);
            }
     
            services.AddSingleton<ICoreConfig, ClientConfig>();
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
                
                var result=  JsonConvert.DeserializeObject<ResponseData>(response.Content);
                return ((JObject)result.Result).ToObject<T>();
            }
            return null;
        }
        public T RequestMe<T>(RestRequest request)
        {
            var response = _client.Execute(request);
            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
           var tip= typeof(T);
           var result= (T)Activator.CreateInstance(tip);
            return result;
        }

        public bool LifeTime
        {
            get { return false; }
        }
                       
    }
}
