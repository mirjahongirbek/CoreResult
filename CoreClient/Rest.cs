using CoreClient.Interface;
using CoreClient.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RepositoryCore.Models;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Timers;

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
        public static dynamic Config
        {
            get
            {
                if (_instanse == null)
                {
                    throw new System.Exception("Rest is not Initialize");
                }
                return _instanse.GetConfig().Config;

            }
        }
        Timer timer;

        private Rest(string url, string projectName, IServiceCollection services, double sec, string login = "", string password = "")
        {

            _lite = new LiteClient(projectName);
            Url = url;
            ProjectName = projectName;
            _client = new RestClient(Url);
            _client.Authenticator = new HttpBasicAuthenticator(username: login, password: password);
            RestState.Client = this;
            ICoreConfig config = new ClientConfig(_lite);
            services.AddSingleton(_lite);
            services.AddSingleton(config);
            timer = new Timer();
            timer.Interval = sec * 1000;
            timer.Elapsed += updateService;
        }

        private void updateService(object sender, ElapsedEventArgs e)
        {
            _config.GetServer();
            UpdateResult();
        }

        public static Rest Instanse(string url, string projectName, IServiceCollection services, double sec = 600, string login = "", string password = "")
        {

            if (_instanse == null)
            {
                RestState.ProjectName = projectName;
                RestState.Url = url;
                _instanse = new Rest(url, projectName, services, sec, login, password);


            }
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
            {
                _instanse._client.Authenticator = new HttpBasicAuthenticator(login, password);
            }
            services.AddSingleton<ICoreConfig, ClientConfig>();
            return _instanse;

        }

        public MyModel GetById(int id, ModelStatus modelStatus)
        {
            var response = _lite.GetById(id, modelStatus);
            if (response != null)
            {
                return response;
            }
            RestRequest request = new RestRequest("/MyRest/ByTraffic/", RestSharp.Method.POST);
            MyModel traffic = new MyModel();
            traffic.ProjectName = ProjectName;
            traffic.ModelStatus = ModelStatus.IntStatus;
            traffic.StatusCode = id;
            request.AddJsonBody(traffic);
            response = Request<MyModel>(request);
            if(response!= null)
            {
                _lite.SaveTraffic(response);

                return response;
            }return new MyModel();
            

        }
        //TODO Cachange
        public ProjectConfig GetConfig()
        {
            RestRequest request = new RestRequest("/MyRest/GetConfig?name=" + ProjectName);
            var rest = Request<ProjectConfig>(request);

            return rest;
        }
        public MyModel GetIfNotExist(Expression<Func<MyModel, bool>> filter)
        {
            var list = _lite.FindModels(filter);
            if (list.Count == 1)
            {
                return list[0];
            }
            if (list.Count > 1)
            {
                return list[list.Count - 1];
            }
            return null;

        }

        public void UpdateResult()
        {
            RestRequest request = new RestRequest("/MyRest/GetAllResult?name=" + RestState.ProjectName, Method.GET);
            var results = Request<List<MyModel>>(request);
            _lite.UpdateAllResult(results);
        }

        public T Request<T>(RestRequest request)
            where T : class
        {
            try
            {
                var response = _client.Execute(request);
                if (response.IsSuccessful)
                {

                    var result = JsonConvert.DeserializeObject<ResponseData>(response.Content);

                    if (result.Result is JObject)
                        return ((JObject)result.Result).ToObject<T>();
                    if (result.Result is JArray)
                        return ((JArray)result.Result).ToObject<T>();

                }
                return null;
            }
            catch (Exception ext)
            {
                return null;
            }
        }
        public T RequestMe<T>(RestRequest request)
        {
            var response = _client.Execute(request);
            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            var tip = typeof(T);
            var result = (T)Activator.CreateInstance(tip);
            return result;
        }

        public bool LifeTime
        {
            get { return false; }
        }

    }
}
