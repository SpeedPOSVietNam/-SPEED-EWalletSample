using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _SPEED__EWalletSample.Models
{
    public class MomoConfig
    {
        public string sandbox { get; set; }
        public string production { get; set; }
        public string partnerCode { get; set; }
        public string accessKey { get; set; }
        public string secretkey { get; set; }
    }

    public class MomoRequestQr
    {
        public string partnerCode { get; set; }
        public string partnerName { get; set; }
        public string storeId { get; set; }
        public string requestType { get; set; }
        public string ipnUrl { get; set; }
        public string redirectUrl { get; set; }
        public string orderId { get; set; }
        public string amount { get; set; }
        public string lang { get; set; }
        public bool autoCapture { get; set; }
        public string orderInfo { get; set; }
        public string requestId { get; set; }
        public string extraData { get; set; }
        public string signature { get; set; }
    }

    public class MomoResponseQr
    {
        public string partnerCode { get; set; }
        public string orderId { get; set; }
        public string requestId { get; set; }
        public int amount { get; set; }
        public long responseTime { get; set; }
        public string message { get; set; }
        public int resultCode { get; set; }
        public string payUrl { get; set; }
        public string deeplink { get; set; }
        public string qrCodeUrl { get; set; }
    }
}
