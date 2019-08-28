
using LiteDB;
using LiteRepository.Context;

namespace Service.Db
{
    public class LiteDbContext : ILiteContext
    {
        public LiteDbContext()
        {
            Database = new LiteDatabase("test.db");
        }
        public LiteDatabase Database { get; private set; }
    }
}
