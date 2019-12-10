
using CoreClient.Models;
using LiteDB;
using System.Linq;

namespace CoreClient
{
    internal class LiteClient
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
