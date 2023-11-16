using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _SPEED__EWalletSample.Models
{

    public class Data
    {
        public string orderId { get; set; }
        public string transId { get; set; }
        public string paygateCode { get; set; }
        public int amount { get; set; }
    }

    public class StatusResult
    {
        public string status { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
        public string secureHash { get; set; }
    }

}
