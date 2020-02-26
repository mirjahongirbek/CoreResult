using RepositoryCore.Enums;
using RepositoryCore.Models;

namespace CoreClient
{
    public static class GetDefaultResult
    {
        public static void SetStatusCode(this ResponseData data, StatusCore code)
        {
            data.StatusCore = code;

        }
    }

}
