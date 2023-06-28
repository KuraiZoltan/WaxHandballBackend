using Angular_Test_App.Model;
using Microsoft.AspNetCore.Mvc;

namespace Angular_Test_App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ApiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("getApiKey")]
        public Payload GetApiKey()
        {
            var payload = new Payload();
            payload.ApiKey = _configuration.GetValue<string>("SportApi:ApiKey");
            return payload;

        }
    }
}
