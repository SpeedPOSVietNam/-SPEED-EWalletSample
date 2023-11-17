using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _SPEED__EWalletSample.Models
{
    public class QrRequest
    {
        public int amount { get; set; }
        public string bankCode { get; set; }
        public string language { get; set; }
        public string paygate { get; set; }
        public string paygateCode { get; set; }
        public string orderId { get; set; }
        public string orderInfo { get; set; }
        public string paygateMethod { get; set; }
        public string identifier { get; set; }
        public string authMethod { get; set; }
        public string secureHash { get; set; }
    }

    public class RefundRequest
    {
        public int amount { get; set; }
        public string paygate { get; set; }
        public string paygateCode { get; set; }
        public string orderId { get; set; }
        public string identifier { get; set; }
        public string authMethod { get; set; }
        public string secureHash { get; set; }
    }

    public class CheckStatusRequest
    {
        public string paygate { get; set; }
        public string paygateCode { get; set; }
        public string orderId { get; set; }
        public string identifier { get; set; }
        public string authMethod { get; set; }
        public string secureHash { get; set; }

    }

    public class ResultResponse
    {
        public bool success { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public QrData data { get; set; }
       
    }

    public class QrData
    {
        public string orderId { get; set; }
        public string paymentLink { get; set; }
        public ListenerJson listenerJson { get; set; }
        public string transId { get; set; }
        public string paygateCode { get; set; }
        public int amount { get; set; }
    }

    public class ListenerJson
    {
        public int amount { get; set; }
        public string message { get; set; }
        public string orderId { get; set; }
        public string payType { get; set; }
        public long transId { get; set; }
        public string extraData { get; set; }
        public string requestId { get; set; }
        public object signature { get; set; }
        public int resultCode { get; set; }
        public long lastUpdated { get; set; }
        public string partnerCode { get; set; }
        public List<object> refundTrans { get; set; }
        public long responseTime { get; set; }
    }

}
