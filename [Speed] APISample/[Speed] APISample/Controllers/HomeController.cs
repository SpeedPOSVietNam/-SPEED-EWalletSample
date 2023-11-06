using _Speed__APISample.Areas.HelpPage.App_Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;

namespace _Speed__APISample.Controllers
{
    public class HomeController : ApiController
    {
        public ApiResult ReceiveStatus([FromBody] object _Request)
        {
            ApiResult _ApiResult = new ApiResult();

            try
            {
                ApiRequest _ApiRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiRequest>(_Request.ToString());
                if (_ApiRequest != null)
                {

                    FunctionHelper.WriteLogs(_ApiRequest.data.orderId,"IPN", Newtonsoft.Json.JsonConvert.SerializeObject(_ApiRequest));
                    string rawHash = "amount=" + _ApiRequest.data.amount + "&orderId=" + _ApiRequest.data.orderId + "&paygateCode=" + _ApiRequest.data.paygateCode + "&transId=" + _ApiRequest.data.transId + "";
                    string secureHash = FunctionHelper.HMACSHA256(rawHash, "IjsLFBnUnPXLViFJUOPPXgagvVNPfYvL");
                    FunctionHelper.WriteLogs(_ApiRequest.data.orderId, "Hash", Newtonsoft.Json.JsonConvert.SerializeObject(secureHash));
                    if (secureHash == _ApiRequest.secureHash)
                    {
                        _ApiResult.success = true;
                        _ApiResult.description = "success";
                    }
                    else {
                        _ApiResult.success = false;
                        _ApiResult.description = "Incorrect secureHash";
                    }
                }
                _ApiResult.success = false;
                _ApiResult.description = "fails";
            }
            catch (Exception ex)
            {
                FunctionHelper.WriteLogs("Exception", DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next(1, 10000), Newtonsoft.Json.JsonConvert.SerializeObject(_Request));


            }
            return _ApiResult;
        }
    }

    public class Data
    {
        public string orderId { get; set; }
        public string transId { get; set; }
        public string paygateCode { get; set; }
        public int amount { get; set; }
    }

    public class ApiRequest
    {
        public string status { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
        public string secureHash { get; set; }
    }

    public class ApiResult
    {
        public bool success { get; set; }
        public string description { get; set; }
    }


}
