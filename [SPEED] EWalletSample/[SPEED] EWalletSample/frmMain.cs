using _SPEED__EWalletSample.Models;
using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace _SPEED__EWalletSample
{
    public partial class frmMain : Form
    {
        Socket SkServer;
        Thread ThrBeginAccept;
        IPEndPoint ipe;
        List<Socket> lstSkClient = new List<Socket>();
        bool isClientConnect = true;
        bool isAccept = false, isReceive = false;
        bool isRunning = false;
        bool isWaiting = false;
        internal int countInv = 0;
        int indexWallet = 0;
        string Transact = "";
        string Amount = "";
        string MethodPay = "";
        public frmMain()
        {
            Application.EnableVisualStyles();
            InitializeComponent();
            wbQRCode.Navigate(Application.StartupPath + "\\QRCode\\index.html");
            ((Control)wbQRCode).Enabled = false;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            Configuration.InitConfig();

            string[] wallet = { PaymentMethod.Momo.ToString(), PaymentMethod.VNPay.ToString(), PaymentMethod.ZaloPay.ToString() };

            foreach (string item in wallet)
            {
                cbWallet.Items.Add(item.ToUpper());
            }
            cbWallet.SelectedIndex = indexWallet;

            if (!isRunning)
            {
                isAccept = true;
                isReceive = true;
                ipe = new IPEndPoint(IPAddress.Any, int.Parse(Configuration.getConfig["wsPort"]));
                SkServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                SkServer.Bind(ipe);
                SkServer.Listen(3);
                isRunning = true;
                ThrBeginAccept = new Thread(new ThreadStart(BeginAccept));
                ThrBeginAccept.IsBackground = true;
                ThrBeginAccept.Start();
            }
        }



        private void btnGenQr_Click(object sender, EventArgs e)
        {
            wbQRCode.Focus();

            switch (cbWallet.SelectedIndex)
            {
                case 0:
                    MethodPay = PaymentMethod.Momo;
                    break;
                case 1:
                    MethodPay = PaymentMethod.VNPay;
                    break;
                case 2:
                    MethodPay = PaymentMethod.ZaloPay;
                    break;
            }


            Transact = txtTransact.Text != "" ? txtTransact.Text : Guid.NewGuid().ToString().Substring(0, 10).Replace("-", "") + "-" + DateTime.Now.ToString("yyyyMMddhhmmss");
            Amount = txtAmount.Text;


            QrRequest _QrRequest = new QrRequest()
            {
                amount = Convert.ToInt32(Amount),
                bankCode = Configuration.getConfig["bankCode"],
                language = Configuration.getConfig["language"],
                paygate = MethodPay,
                paygateCode = Configuration.getConfig["" + MethodPay + "PayCode"],
                orderId = Transact,
                orderInfo = Transact,
                paygateMethod = Configuration.getConfig["paygateMethod"],
                identifier = Configuration.getConfig["identifier"],
                authMethod = "sha256",
            };

            string rawHash = "amount=" + Amount + "&authMethod=" + _QrRequest.authMethod + "&bankCode=" + _QrRequest.bankCode + "&identifier=" + _QrRequest.identifier + "&language=" + _QrRequest.language + "&orderId=" + Transact + "&paygate=" + _QrRequest.paygate + "&paygateCode=" + _QrRequest.paygateCode + "&paygateMethod=" + _QrRequest.paygateMethod + "";
            _QrRequest.secureHash = HMACSHA256(rawHash, Configuration.getConfig["secretKey"]);

            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Configuration.getConfig["url"] + "/crv/payment/create");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object sd, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };


                string strBody = Newtonsoft.Json.JsonConvert.SerializeObject(_QrRequest);
                txtRequest.Text = strBody.ToString();
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(strBody);
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var srPayPos = new StreamReader(httpResponse.GetResponseStream()))
                {

                    ResultResponse _ResultResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultResponse>(srPayPos.ReadToEnd());
                    txtResponse.Text = Newtonsoft.Json.JsonConvert.SerializeObject(_ResultResponse);
                    if (_ResultResponse.status == "00")
                    {
                        GenerateQr(_ResultResponse.data.paymentLink);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                wbQRCode.Focus();
            }

        }

        private void btnRefund_Click(object sender, EventArgs e)
        {
            wbQRCode.Navigate(Application.StartupPath + "\\QRCode\\index.html");

            if (txtTransact.Text == "")
            {
                MessageBox.Show("Please enter Transact#");
                return;
            }

            if (txtAmount.Text == "")
            {
                MessageBox.Show("Please enter Amount");
                return;
            }

            switch (cbWallet.SelectedIndex)
            {
                case 0:
                    MethodPay = PaymentMethod.Momo;
                    break;
                case 1:
                    MethodPay = PaymentMethod.VNPay;
                    break;
                case 2:
                    MethodPay = PaymentMethod.ZaloPay;
                    break;
            }


            Transact = txtTransact.Text;
            Amount = txtAmount.Text;


            RefundRequest _RefundRequest = new RefundRequest()
            {
                amount = Convert.ToInt32(Amount),
                paygate = MethodPay,
                paygateCode = Configuration.getConfig["" + MethodPay + "PayCode"],
                orderId = Transact,
                identifier = Configuration.getConfig["identifier"],
                authMethod = "sha256",
            };

            string rawHash = "amount=" + Amount + "&authMethod=" + _RefundRequest.authMethod + "&identifier=" + _RefundRequest.identifier + "&orderId=" + Transact + "&paygate=" + _RefundRequest.paygate + "&paygateCode=" + _RefundRequest.paygateCode + "";
            _RefundRequest.secureHash = HMACSHA256(rawHash, Configuration.getConfig["secretKey"]);

            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Configuration.getConfig["url"] + "/crv/payment/refund");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object sd, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };


                string strBody = Newtonsoft.Json.JsonConvert.SerializeObject(_RefundRequest);
                txtRequest.Text = strBody.ToString();
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(strBody);
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var srPayPos = new StreamReader(httpResponse.GetResponseStream()))
                {

                    ResultResponse _ResultResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultResponse>(srPayPos.ReadToEnd());
                    txtResponse.Text = Newtonsoft.Json.JsonConvert.SerializeObject(_ResultResponse);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            wbQRCode.Navigate(Application.StartupPath + "\\QRCode\\index.html");
            if (txtTransact.Text == "")
            {
                MessageBox.Show("Please enter Transact#");
                return;
            }

         
            switch (cbWallet.SelectedIndex)
            {
                case 0:
                    MethodPay = PaymentMethod.Momo;
                    break;
                case 1:
                    MethodPay = PaymentMethod.VNPay;
                    break;
                case 2:
                    MethodPay = PaymentMethod.ZaloPay;
                    break;
            }


            Transact = txtTransact.Text;



            CheckStatusRequest _CheckStatusRequest = new CheckStatusRequest()
            {
               
                paygate = MethodPay,
                paygateCode = Configuration.getConfig["" + MethodPay + "PayCode"],
                orderId = Transact,
                identifier = Configuration.getConfig["identifier"],
                authMethod = "sha256",
            };

            string rawHash = "authMethod=" + _CheckStatusRequest.authMethod + "&identifier=" + _CheckStatusRequest.identifier + "&orderId=" + Transact + "&paygate=" + _CheckStatusRequest.paygate + "&paygateCode=" + _CheckStatusRequest.paygateCode + "";
            _CheckStatusRequest.secureHash = HMACSHA256(rawHash, Configuration.getConfig["secretKey"]);

            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(Configuration.getConfig["url"] + "/crv/payment/status");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object sd, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };


                string strBody = Newtonsoft.Json.JsonConvert.SerializeObject(_CheckStatusRequest);
                txtRequest.Text = strBody.ToString();
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(strBody);
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var srPayPos = new StreamReader(httpResponse.GetResponseStream()))
                {

                    ResultResponse _ResultResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultResponse>(srPayPos.ReadToEnd());
                    txtResponse.Text = Newtonsoft.Json.JsonConvert.SerializeObject(_ResultResponse);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GenerateQr(string _Content)
        {
            string imgSrc = "";

            QRCodeGenerator qrGenertor = new QRCodeGenerator();
            QRCodeData qrData = qrGenertor.CreateQrCode(_Content, (QRCodeGenerator.ECCLevel)int.Parse(Configuration.getConfig["eccLevel"]));

            QRCode qrCode = new QRCode(qrData);

            bmQR = qrCode.GetGraphic(int.Parse(Configuration.getConfig["pixelsPerModule"]), Color.Black, Color.White, null, 0, 6, false);

            Bitmap bmQRLogo = qrCode.GetGraphic(int.Parse(Configuration.getConfig["pixelsPerModule"]), Color.Black, Color.White, GetIconBitmap(Application.StartupPath + "\\" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + @".png"), int.Parse(Configuration.getConfig["qrLogoSize"]), 1, false);

            MemoryStream ms = new MemoryStream();

            if (bmQRLogo.Size.Width > int.Parse(Configuration.getConfig["resizeMax"]))
                new Bitmap(bmQRLogo, int.Parse(Configuration.getConfig["resizeMax"]), int.Parse(Configuration.getConfig["resizeMax"])).Save(ms, ImageFormat.Png);
            else
                bmQRLogo.Save(ms, ImageFormat.Png);
            byte[] byteImage = ms.ToArray();
            imgSrc = "data:image/png;base64," + Convert.ToBase64String(byteImage);


            foreach (Socket sk in lstSkClient)
            {
                sk.Send(GetFrameFromString(imgSrc));
            }
        }


        public static string HMACSHA256(string message, string key)
        {
            byte[] keyByte = Encoding.UTF8.GetBytes(key);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                string hex = BitConverter.ToString(hashmessage);
                hex = hex.Replace("-", "").ToLower();
                return hex;

            }
        }

        private Bitmap GetIconBitmap(string path)
        {
            Bitmap img = null;
            if (File.Exists(path))
            {
                try
                {
                    img = new Bitmap(path);
                }
                catch (Exception)
                {
                }
            }
            return img;
        }

        private string ConvertToUnsign(string str)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = str.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty)
                        .Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        Bitmap bmQR;



        public void BeginAccept()
        {
            try
            {
                while (isAccept)
                {
                    Socket sk = SkServer.Accept();
                    Thread ThrBeginisReceive = new Thread(BeginReceive);
                    ThrBeginisReceive.IsBackground = true;
                    ThrBeginisReceive.Start(sk);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            isRunning = false;
            Console.WriteLine("BeginAccept END");
        }

        public void BeginReceive(object obj)
        {
            Socket SkClient = (Socket)obj;
            while (isReceive)
            {
                try
                {
                    byte[] buff = new byte[1024];
                    int recv = SkClient.Receive(buff);
                    string data = System.Text.Encoding.UTF8.GetString(buff).Substring(0, recv);
                    if (Regex.IsMatch(data, "^GET"))
                    {
                        var key = data.Replace("ey:", "`")
                                      .Split('`')[1]
                                      .Replace("\r", "").Split('\n')[0]
                                      .Trim();
                        var test1 = AcceptKey(ref key);
                        var newLine = "\r\n";
                        var response = "HTTP/1.1 101 Switching Protocols" + newLine
                             + "Upgrade: websocket" + newLine
                             + "Connection: Upgrade" + newLine
                             + "Sec-WebSocket-Accept: " + test1 + newLine + newLine;
                        SkClient.Send(System.Text.Encoding.UTF8.GetBytes(response));
                        lstSkClient.Add(SkClient);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    try
                    {
                        lstSkClient.Remove(SkClient);
                    }
                    catch (Exception) { }
                    return;
                }
            }

        }

        public static T[] SubArray<T>(T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        private string AcceptKey(ref string key)
        {
            string guid = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
            string longKey = key + guid;
            byte[] hashBytes = ComputeHash(longKey);
            return Convert.ToBase64String(hashBytes);
        }

        static SHA1 sha1 = SHA1CryptoServiceProvider.Create();
        private static byte[] ComputeHash(string str)
        {
            return sha1.ComputeHash(System.Text.Encoding.ASCII.GetBytes(str));
        }

        public string GetDecodedData(byte[] buffer, int length)
        {
            byte b = buffer[1];
            int dataLength = 0;
            int totalLength = 0;
            int keyIndex = 0;

            if (b - 128 <= 125)
            {
                dataLength = b - 128;
                keyIndex = 2;
                totalLength = dataLength + 6;
            }

            if (b - 128 == 126)
            {
                dataLength = BitConverter.ToInt16(new byte[] { buffer[3], buffer[2] }, 0);
                keyIndex = 4;
                totalLength = dataLength + 8;
            }

            if (b - 128 == 127)
            {
                dataLength = (int)BitConverter.ToInt64(new byte[] { buffer[9], buffer[8], buffer[7], buffer[6], buffer[5], buffer[4], buffer[3], buffer[2] }, 0);
                keyIndex = 10;
                totalLength = dataLength + 14;
            }

            if (totalLength > length)
                throw new Exception("The buffer length is small than the data length");

            byte[] key = new byte[] { buffer[keyIndex], buffer[keyIndex + 1], buffer[keyIndex + 2], buffer[keyIndex + 3] };

            int dataIndex = keyIndex + 4;
            int count = 0;
            for (int i = dataIndex; i < totalLength; i++)
            {
                buffer[i] = (byte)(buffer[i] ^ key[count % 4]);
                count++;
            }

            return Encoding.ASCII.GetString(buffer, dataIndex, dataLength);
        }

        public enum EOpcodeType
        {
            Fragment = 0,
            Text = 1,
            Binary = 2,
            ClosedConnection = 8,
            Ping = 9,
            Pong = 10
        }

        public byte[] GetFrameFromString(string Message, EOpcodeType Opcode = EOpcodeType.Text)
        {
            byte[] response;
            byte[] bytesRaw = Encoding.Default.GetBytes(Message);
            byte[] frame = new byte[10];

            int indexStartRawData = -1;
            int length = bytesRaw.Length;

            frame[0] = (byte)(128 + (int)Opcode);
            if (length <= 125)
            {
                frame[1] = (byte)length;
                indexStartRawData = 2;
            }
            else if (length >= 126 && length <= 65535)
            {
                frame[1] = (byte)126;
                frame[2] = (byte)((length >> 8) & 255);
                frame[3] = (byte)(length & 255);
                indexStartRawData = 4;
            }
            else
            {
                frame[1] = (byte)127;
                frame[2] = (byte)((length >> 56) & 255);
                frame[3] = (byte)((length >> 48) & 255);
                frame[4] = (byte)((length >> 40) & 255);
                frame[5] = (byte)((length >> 32) & 255);
                frame[6] = (byte)((length >> 24) & 255);
                frame[7] = (byte)((length >> 16) & 255);
                frame[8] = (byte)((length >> 8) & 255);
                frame[9] = (byte)(length & 255);

                indexStartRawData = 10;
            }

            response = new byte[indexStartRawData + length];

            int i, reponseIdx = 0;

            for (i = 0; i < indexStartRawData; i++)
            {
                response[reponseIdx] = frame[i];
                reponseIdx++;
            }

            for (i = 0; i < length; i++)
            {
                response[reponseIdx] = bytesRaw[i];
                reponseIdx++;
            }

            return response;
        }

        public static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtTransact.Text = Guid.NewGuid().ToString().Substring(0, 10).Replace("-", "") + "-" + DateTime.Now.ToString("yyyyMMddhhmmss");
        }

        private void cbWallet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (indexWallet != cbWallet.SelectedIndex)
            {
                wbQRCode.Navigate(Application.StartupPath + "\\QRCode\\index.html");
            }
        }



    }
}
