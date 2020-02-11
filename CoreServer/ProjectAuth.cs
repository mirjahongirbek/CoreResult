using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreServer
{
    public static class ProjectAuth
    {
        public static KeyValuePair<string, string>? AuthMe(this ControllerBase cBase)
        {
            if (cBase.Request.Headers.ContainsKey("Authorization")) return null;
            var header = cBase.Request.Headers["Authorization"];
            if (header.ToString().StartsWith("Basic ", System.StringComparison.OrdinalIgnoreCase))
            {
                var encodedUsernamePassword =
                 header.ToString().Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
                // Decode from Base64 to string
                var result = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));
                if (!string.IsNullOrEmpty(result))
                    return new KeyValuePair<string, string>(result.Split(':', 2)[0], result.Split(':', 2)[1]);
            }
            return null;
        }
    }
}
