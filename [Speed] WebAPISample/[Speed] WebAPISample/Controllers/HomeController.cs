using _Speed__WebAPISample.Hubs;
using _Speed__WebAPISample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace _Speed__WebAPISample.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly IHubContext<NotificateHub> _hubContext;

        public HomeController(IHubContext<NotificateHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost(Name = "")]
        public Result ReceiveStatus([FromBody] object _Request)
        {
            Result _Result = new Result();

            try
            {
                Payload _Payload = JsonConvert.DeserializeObject<Payload>(_Request.ToString());
                if (_Payload != null)
                {

                    FunctionHelper.WriteLogs(_Payload.data.orderId, "IPN", Newtonsoft.Json.JsonConvert.SerializeObject(_Payload));
                    string rawHash = "amount=" + _Payload.data.amount + "&orderId=" + _Payload.data.orderId + "&paygateCode=" + _Payload.data.paygateCode + "&transId=" + _Payload.data.transId + "";
                    string secureHash = FunctionHelper.HMACSHA256(rawHash, "IjsLFBnUnPXLViFJUOPPXgagvVNPfYvL");
                    FunctionHelper.WriteLogs(_Payload.data.orderId, "Hash", Newtonsoft.Json.JsonConvert.SerializeObject(secureHash));
                    
                    


                    if (secureHash == _Payload.secureHash)
                    {
                        _Result.success = true;
                        _Result.description = "success";
                        _hubContext.Clients.All.SendAsync("ReceiveStatus", "orderId: " + _Payload.data.orderId+ " : "+ _Payload.message);
                    }
                    else
                    {
                        _Result.success = false;
                        _Result.description = "Incorrect secureHash";
                    }
                }

            }
            catch (Exception ex)
            {
                FunctionHelper.WriteLogs("Exception", DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next(1, 10000), Newtonsoft.Json.JsonConvert.SerializeObject(_Request) + "\n" + ex.Message);


            }
            return _Result;
        }
    }
}