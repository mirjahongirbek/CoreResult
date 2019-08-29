
using CoreClient.Models;
using LiteDB;
using Newtonsoft.Json;
using RestSharp;
using System.Linq;

namespace CoreClient
{
    public class Rest
    {
        RestSharp.RestClient _client;
        private string ProjectName { get; set; }
        private string Url { get { return "http://172.17.9.105:1600/api"; } }
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
            MyModel traffic = new MyModel();
            traffic.ProjectName = ProjectName;
            traffic.StatusCode = id;
            request.AddJsonBody(traffic);
           var response= Request<MyModel>(request);
            _lite.SaveTraffic(response);
            return null;
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
    public class LiteClient
    {

        public LiteDB.LiteDatabase Database { get; set; }
        public LiteClient(string databaseName="test.db")
        {
            if (!databaseName.Contains(".db"))
            {
                databaseName = databaseName + ".db";
            }
            Database = new LiteDB.LiteDatabase(databaseName);
        }
        public  void SaveData<T>(T model)

        {

            Database.GetCollection<T>().Insert(model);
        }
        public void SaveTraffic(MyModel model)
        {
           var traffic= Database.GetCollection<MyModel>().FindOne(m => m.StatusCode == model.StatusCode);
            if(traffic== null)
            {
               if(string.IsNullOrEmpty(model.Id))
                {
                    model.Id = ObjectId.NewObjectId().ToString();
                }
                Database.GetCollection<MyModel>().Insert(model);
            }
            else
            {
                model.Id = traffic.Id;
                Database.GetCollection<MyModel>().Update(model);
            }
        }
        public void SaveConfig(ProjectConfig config)
        {
         var _collection=   Database.GetCollection<ProjectConfig>();
           var list= _collection.FindAll().ToList();
            if (list.Count == 0)
            {
                if (string.IsNullOrEmpty(config.Id))
                {
                    config.Id = ObjectId.NewObjectId().ToString();
                }
                _collection.Insert(config);
            }
           var conf= list[0];
            config.Id = conf.Id;
            _collection.Update(config);
        }
        public ProjectConfig GetConfig()
        {
            var confs = Database.GetCollection<ProjectConfig>().FindAll().ToArray();
            if (confs.Count() > 0)
            {
               return confs[0];
            }
            return null;
        }
                
    }
}
