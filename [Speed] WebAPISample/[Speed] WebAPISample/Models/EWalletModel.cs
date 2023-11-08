namespace _Speed__WebAPISample.Models
{
    public class Data
    {
        public string orderId { get; set; }
        public string transId { get; set; }
        public string paygateCode { get; set; }
        public int amount { get; set; }
    }

    public class Payload
    {
        public string status { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
        public string secureHash { get; set; }
    }

    public class Result
    {
        public bool success { get; set; }
        public string description { get; set; }
    }
}
