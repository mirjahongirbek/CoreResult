using CoreClient.Interface;
using CoreResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryCore.Models;

namespace CoreTest.Controllers
{
    [Route("api/[controller]/[action]")]
  
    public class ValuesController : ControllerBase
    {
        private static IHttpContextAccessor _httpContextAccessor;
        private ICoreConfig _config;
        public ValuesController(IHttpContextAccessor httpContextAccessor,
            ICoreConfig config)
        {
            _httpContextAccessor = httpContextAccessor;
            _config = config;
        }
        public static HttpContext Current => _httpContextAccessor.HttpContext;
        // GET api/values
        [HttpGet]
        public NetResult<ResponseData> Get()
        {
           var Key= _config.ToString("joha");
            return 16;
 
        }
      public int Id { get; set; }
       // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
