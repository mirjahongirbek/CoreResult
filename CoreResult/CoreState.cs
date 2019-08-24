using RepositoryCore.Models;

namespace CoreResults
{
    public static class CoreState
    {
        public static NetResult<Result> GetResult(string message)
        {
            return NetResult<Result>.NewResult(message);
        }
        public static NetResult<Result> GetResult(int message)
        {
            return NetResult<Result>.NewResult(message);
        }



    }



}
