using CoreClient.Models;

namespace CoreClient.Interface
{
    public interface ICoreConfig
    {
        ProjectConfig GetConfig();
        string ToString(string key);
    }
}
