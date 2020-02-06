using CoreClient.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CoreClient
{
    public class LiteClient
    {

        public LiteDatabase Database { get; set; }
        private LiteCollection<MyModel> _models;
        private LiteCollection<ProjectConfig> _configs;
        public LiteClient(string databaseName="test.db")
        {
            if (!databaseName.Contains(".db"))
            {
                databaseName = databaseName + ".db";
            }
            Database = new LiteDB.LiteDatabase(databaseName);
            _models = Database.GetCollection<MyModel>();
            _configs = Database.GetCollection<ProjectConfig>();
        }
        public  void SaveData<T>(T model)
        {
            Database.GetCollection<T>().Insert(model);
        }
        public void SaveTraffic(MyModel model)
        {
           var traffic= _models.FindOne(m => m.StatusCode == model.StatusCode);
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
        public void UpdateAllResult(List<MyModel> results)
        {
            
            foreach(var i in results)
            {
                if (string.IsNullOrEmpty(i.Id)){
                    i.Id = ObjectId.NewObjectId().ToString();
                }
            }
            _models.Delete(m => true);
            _models.InsertBulk(results);
        }

        public void SaveConfig(ProjectConfig config)
        {
         //var _collection=   /Database.GetCollection<ProjectConfig>();
           var list= _configs.FindAll().ToList();
            if (list.Count == 0)
            {
                if (string.IsNullOrEmpty(config.Id))
                {
                    config.Id = ObjectId.NewObjectId().ToString();
                }
                _configs.Insert(config);
            }
           var conf= list[0];
            config.Id = conf.Id;
            _configs.Update(config);
        }
        public ProjectConfig GetConfigById(string id)
        {
            var confs = Database.GetCollection<ProjectConfig>().FindById(id);
            return confs;
        }
        public void UpdateConfig(ProjectConfig conf)
        {

            if (conf == null) return;
            Database.GetCollection<ProjectConfig>().Update(conf);
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
        public MyModel GetById(int id, ModelStatus modelStatus)
        {
           return _models.FindOne(m => m.StatusCode == id);
        }
        public List<MyModel> FindModels(Expression<Func<MyModel, bool>> filter)
        {
           return _models.Find(filter).ToList();
        }
                
    }
}
