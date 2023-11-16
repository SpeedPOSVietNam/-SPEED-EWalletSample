using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace _Speed__WebAPISample
{
    public static class FunctionHelper
    {
        public static string Encrypt(string encryptionKey, string textToEncrypt)
        {
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = Encoding.ASCII.GetBytes(encryptionKey);
            tdes.IV = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            byte[] stringBytes = Encoding.UTF8.GetBytes(textToEncrypt);
            string st = Convert.ToBase64String(tdes.CreateEncryptor().TransformFinalBlock(stringBytes, 0, stringBytes.Length));
            byte[] toBytes = Encoding.ASCII.GetBytes(st);
            string s = BitConverter.ToString(toBytes).Replace("-", "").ToLower();
            Decrypt(encryptionKey, s);
            return s;
        }
        public static string Decrypt(string encryptionKey, string textToDecrypt)
        {
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = Encoding.ASCII.GetBytes(encryptionKey);
            des.IV = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            byte[] fromByte = StringToByteArray(textToDecrypt);
            string st = Encoding.ASCII.GetString(fromByte);
            byte[] stringBytes = Convert.FromBase64String(st);
            return Encoding.ASCII.GetString(des.CreateDecryptor().TransformFinalBlock(stringBytes, 0, stringBytes.Length));
        }

        private static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
            .Where(x => x % 2 == 0)
            .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
            .ToArray();
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

        public static void WriteLogs(string LogType, string LogFile, string LogText)
        {
            try
            {
                string LogPath = Directory.GetCurrentDirectory() + "/Logs/" + LogType;
                if (!Directory.Exists(LogPath))
                    Directory.CreateDirectory(LogPath);
                StreamWriter sw = new StreamWriter(LogPath + "/" + LogFile + ".log");
                sw.Write(LogText);
                sw.Close();
            }
            catch (Exception)
            {

            }
        }
    }
}
