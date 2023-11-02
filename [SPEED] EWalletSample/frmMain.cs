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
using System.Net.Sockets;
using System.Security.Cryptography;
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
        string Descript = "";
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

            string[] wallet = { "Momo", "VNPay" };

            foreach (string item in wallet)
            {
                cbWallet.Items.Add(item);
            }
            cbWallet.SelectedIndex = indexWallet;
            if (!isRunning)
            {
                isAccept = true;
                isReceive = true;
                ipe = new IPEndPoint(IPAddress.Any, int.Parse(Configuration.getConfig["WSPort"]));
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
            txtStatus.Visible = false;
            txtStatus.ForeColor = System.Drawing.SystemColors.ControlText;
            Transact = txtTransact.Text != "" ? txtTransact.Text : Guid.NewGuid().ToString();
            Amount = txtAmount.Text;
            Descript = txtDescript.Text;

            switch (indexWallet)
            {

                case 0:
                    {
                        CreateSaleMomo();
                        return;
                    }

                case 1:
                    {
                        CreateSaleVNPay();
                        return;
                    }
            }
        }

        #region MOMO
        private void CreateSaleMomo()
        {
            try
            {

                MomoConfig _Config = Newtonsoft.Json.JsonConvert.DeserializeObject<MomoConfig>(Configuration.getConfig["Momo"]);
                MomoRequestQr _MomoRequestQr = new MomoRequestQr()
                {
                    partnerCode = _Config.partnerCode,
                    partnerName = "Speed",
                    storeId = "1",
                    requestId = Transact != "" ? Transact : Guid.NewGuid().ToString(),
                    amount = Amount,
                    orderId = Transact != "" ? Transact : Guid.NewGuid().ToString(),
                    orderInfo = Descript != "" ? Descript : "Speed Payment",
                    redirectUrl = "https://momo.vn",
                    ipnUrl = "https://momo.vn",
                    autoCapture = true,
                    requestType = "captureWallet",
                    extraData = "",
                    lang = "en"
                };

                string rawHash = "accessKey=" + _Config.accessKey +
                "&amount=" + _MomoRequestQr.amount +
                "&extraData=" + _MomoRequestQr.extraData +
                "&ipnUrl=" + _MomoRequestQr.ipnUrl +
                "&orderId=" + _MomoRequestQr.orderId +
                "&orderInfo=" + _MomoRequestQr.orderInfo +
                "&partnerCode=" + _MomoRequestQr.partnerCode +
                "&redirectUrl=" + _MomoRequestQr.redirectUrl +
                "&requestId=" + _MomoRequestQr.requestId +
                "&requestType=" + _MomoRequestQr.requestType;
                _MomoRequestQr.signature = HMACSHA256(rawHash, _Config.secretkey);
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(_Config.sandbox + "/v2/gateway/api/create");

                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                string strBody = Newtonsoft.Json.JsonConvert.SerializeObject(_MomoRequestQr);
                txtRequest.Text = strBody.ToString();
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(strBody);
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var srPayPos = new StreamReader(httpResponse.GetResponseStream()))
                {

                    MomoResponseQr MomoResponseQr = Newtonsoft.Json.JsonConvert.DeserializeObject<MomoResponseQr>(srPayPos.ReadToEnd());
                    txtResponse.Text = Newtonsoft.Json.JsonConvert.SerializeObject(MomoResponseQr);
                    if (MomoResponseQr.resultCode == 0)
                    {
                        GenerateQr(MomoResponseQr.qrCodeUrl);
                        countInv = Convert.ToInt32(Configuration.getConfig["TimeCheck"]);
                        tmrCountDown.Start();
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

        private void CheckStatusMomo()
        {
            try
            {

                MomoConfig _Config = Newtonsoft.Json.JsonConvert.DeserializeObject<MomoConfig>(Configuration.getConfig["Momo"]);
                MomoRequestQr _MomoRequestQr = new MomoRequestQr()
                {
                    partnerCode = _Config.partnerCode,
                    requestId = Transact != "" ? Transact : Guid.NewGuid().ToString(),
                    orderId = Transact != "" ? Transact : Guid.NewGuid().ToString(),
                    lang = "en"
                };

                string rawHash = "accessKey=" + _Config.accessKey +
                "&orderId=" + _MomoRequestQr.orderId +
                "&partnerCode=" + _MomoRequestQr.partnerCode +
                "&requestId=" + _MomoRequestQr.requestId;

                _MomoRequestQr.signature = HMACSHA256(rawHash, _Config.secretkey);
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(_Config.sandbox + "/v2/gateway/api/query");

                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                string strBody = Newtonsoft.Json.JsonConvert.SerializeObject(_MomoRequestQr);
                txtRequest.Text = strBody.ToString();
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(strBody);
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var srPayPos = new StreamReader(httpResponse.GetResponseStream()))
                {

                    MomoResponseQr MomoResponseQr = Newtonsoft.Json.JsonConvert.DeserializeObject<MomoResponseQr>(srPayPos.ReadToEnd());
                    txtResponse.Text = Newtonsoft.Json.JsonConvert.SerializeObject(MomoResponseQr);
                    txtStatus.Text = MomoResponseQr.message;
                    if (MomoResponseQr.resultCode == 0)
                    {
                        wbQRCode.Navigate(Application.StartupPath + "\\QRCode\\success.html");
                        txtStatus.ForeColor = System.Drawing.Color.Green;
                        txtStatus.Text = "                 Payment success";
                        tmrCountDown.Stop();
                    }
                    else
                    {
                        countInv = Convert.ToInt32(Configuration.getConfig["TimeCheck"]);
                        tmrCountDown.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        #endregion


        #region VNPay
        private void CreateSaleVNPay()
        {
            try
            {

                VNPayConfig _Config = Newtonsoft.Json.JsonConvert.DeserializeObject<VNPayConfig>(Configuration.getConfig["VNPay"]);

                VNPayRequestQr _VNPayRequestQr = new VNPayRequestQr()
                {
                    appId = _Config.appId,
                    merchantName = _Config.merchantName,
                    serviceCode = _Config.serviceCode,
                    countryCode = _Config.countryCode,
                    masterMerCode = _Config.masterMerCode,
                    merchantType = _Config.merchantType,
                    merchantCode = _Config.merchantCode,
                    payloadFormat = null,
                    terminalId = _Config.terminalId,
                    payType = _Config.payType,
                    productId = _Config.productId,
                    productName = null,
                    imageName = null,
                    txnId = Transact,
                    amount = Amount,
                    tipAndFee = "",
                    ccy = _Config.ccy,
                    expDate = DateTime.Now.AddMinutes(int.Parse(_Config.expMin)).ToString("yyMMddHHmm"),
                    desc = Descript,
                    merchantCity = null,
                    merchantCC = null,
                    fixedFee = null,
                    percentageFee = null,
                    pinCode = null,
                    mobile = null,
                    billNumber = Transact,
                    creator = null,
                    consumerID = null,
                    purpose = ""
                };

                string rawHash = _VNPayRequestQr.appId + "|" +
                    _VNPayRequestQr.merchantName + "|" +
                    _VNPayRequestQr.serviceCode + "|" +
                    _VNPayRequestQr.countryCode + "|" +
                    _VNPayRequestQr.masterMerCode + "|" +
                    _VNPayRequestQr.merchantType + "|" +
                    _VNPayRequestQr.merchantCode + "|" +
                    _VNPayRequestQr.terminalId + "|" +
                    _VNPayRequestQr.payType + "|" +
                    _VNPayRequestQr.productId + "|" +
                    _VNPayRequestQr.txnId + @"|" +
                    _VNPayRequestQr.amount + @"||" +
                    _VNPayRequestQr.ccy + "|" +
                    _VNPayRequestQr.expDate + "|" +
                    _Config.createQrcodeSecretKey;

                _VNPayRequestQr.checksum = CreateMD5(rawHash);
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(_Config.url + "/QRCreateAPIRestV2/rest/CreateQrcodeApi/createQrcode");

                httpWebRequest.ContentType = "text/plain";
                httpWebRequest.Method = "POST";
                string strBody = Newtonsoft.Json.JsonConvert.SerializeObject(_VNPayRequestQr);
                txtRequest.Text = strBody.ToString();
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(strBody);
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var srPayPos = new StreamReader(httpResponse.GetResponseStream()))
                {

                    VNPayResponseQr VNPayResponseQr = Newtonsoft.Json.JsonConvert.DeserializeObject<VNPayResponseQr>(srPayPos.ReadToEnd());
                    txtResponse.Text = Newtonsoft.Json.JsonConvert.SerializeObject(VNPayResponseQr);
                    if (VNPayResponseQr.code == "00")
                    {
                        GenerateQr(VNPayResponseQr.data);
                        countInv = Convert.ToInt32(Configuration.getConfig["TimeCheck"]);
                        tmrCountDown.Start();
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

        private void CheckStatusVNPay()
        {
            try
            {
                VNPayConfig _Config = Newtonsoft.Json.JsonConvert.DeserializeObject<VNPayConfig>(Configuration.getConfig["VNPay"]);

                VNPayRequestCheckTrans _VNPayRequestCheckTrans = new VNPayRequestCheckTrans()
                {
                    txnId = Transact,
                    merchantCode = _Config.merchantCode,
                    terminalID = _Config.terminalId,
                    payDate = DateTime.Now.ToString("dd/MM/yyyy"),

                };

                string rawHash = _VNPayRequestCheckTrans.payDate + "|" +
                    _VNPayRequestCheckTrans.txnId + "|" +
                    _VNPayRequestCheckTrans.merchantCode + "|" +
                    _VNPayRequestCheckTrans.terminalID + "|" +
                    _Config.checkTransactionSecretKey;

                _VNPayRequestCheckTrans.checkSum = CreateMD5(rawHash);
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(_Config.url + "/CheckTransaction/rest/api/CheckTrans");

                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                string strBody = Newtonsoft.Json.JsonConvert.SerializeObject(_VNPayRequestCheckTrans);
                txtRequest.Text = strBody.ToString();
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(strBody);
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var srPayPos = new StreamReader(httpResponse.GetResponseStream()))
                {

                    VNPayResponseQr VNPayResponseQr = Newtonsoft.Json.JsonConvert.DeserializeObject<VNPayResponseQr>(srPayPos.ReadToEnd());
                    txtResponse.Text = Newtonsoft.Json.JsonConvert.SerializeObject(VNPayResponseQr);
                    txtStatus.Text = VNPayResponseQr.message;
                    if (VNPayResponseQr.code == "00")
                    {
                        wbQRCode.Navigate(Application.StartupPath + "\\QRCode\\success.html");
                        txtStatus.ForeColor = System.Drawing.Color.Green;
                        txtStatus.Text = "                 Payment success";
                        tmrCountDown.Stop();
                    }
                    else
                    {
                        countInv = Convert.ToInt32(Configuration.getConfig["TimeCheck"]);
                        tmrCountDown.Start();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        #endregion

        private void tmrCountDown_Tick(object sender, EventArgs e)
        {
            try
            {
                txtStatus.Visible = true;
                txtStatus.Text = "Transactions will be checked after " + countInv + "s";
                if (countInv <= 0)
                {
                    try
                    {
                        switch (indexWallet)
                        {

                            case 0:
                                {
                                    CheckStatusMomo();
                                    return;
                                }

                            case 1:
                                {
                                    CheckStatusVNPay();
                                    return;
                                }

                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
                else
                {
                    countInv = countInv - 1;
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
            QRCodeData qrData = qrGenertor.CreateQrCode(_Content, (QRCodeGenerator.ECCLevel)int.Parse(Configuration.getConfig["ECCLevel"]));

            QRCode qrCode = new QRCode(qrData);

            bmQR = qrCode.GetGraphic(int.Parse(Configuration.getConfig["PixelsPerModule"]), Color.Black, Color.White, null, 0, 6, false);

            Bitmap bmQRLogo = qrCode.GetGraphic(int.Parse(Configuration.getConfig["PixelsPerModule"]), Color.Black, Color.White, GetIconBitmap(Application.StartupPath + "\\" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + @".png"), int.Parse(Configuration.getConfig["QRLogoSize"]), 1, false);

            MemoryStream ms = new MemoryStream();

            if (bmQRLogo.Size.Width > int.Parse(Configuration.getConfig["ResizeMax"]))
                new Bitmap(bmQRLogo, int.Parse(Configuration.getConfig["ResizeMax"]), int.Parse(Configuration.getConfig["ResizeMax"])).Save(ms, ImageFormat.Png);
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
            txtTransact.Text = Guid.NewGuid().ToString();
        }

        private void cbWallet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (indexWallet != cbWallet.SelectedIndex)
            {
                indexWallet = cbWallet.SelectedIndex;
                tmrCountDown.Stop();
                wbQRCode.Navigate(Application.StartupPath + "\\QRCode\\index.html");
            }
        }

    }
}
