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
                string LogPath = Directory.GetCurrentDirectory() +"/Logs/" + LogType;
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
