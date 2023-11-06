using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace _SPEED__EWalletSample
{
    public class Configuration
    {
        public static Dictionary<string, string> getConfig;

        public static void InitConfig()
        {
            getConfig = ReadConfigFile();
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
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

        public static Dictionary<string, string> ReadConfigFile()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            XmlDocument docXML = new XmlDocument();
            docXML.Load(Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile) + "\\" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + @".exe.config");
            XmlNodeList lsNode;
            lsNode = docXML.GetElementsByTagName("setting");
            foreach (XmlNode node in lsNode)
            {
                string outterXML = node.OuterXml;
                string[] lsName = outterXML.Split('"');
                string name = lsName[1];
                string value = node.InnerText;
                dic.Add(name, value);
            }
            return dic;
        }

        public static bool WriteConfigFile(Dictionary<string, string> dic)
        {
            XmlDocument docXML = new XmlDocument();
            docXML.Load(Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile) + "\\" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + @".exe.config");
            XmlNodeList lsNode;
            lsNode = docXML.GetElementsByTagName("setting");
            foreach (XmlNode node in lsNode)
            {
                string outterXML = node.OuterXml;
                string[] lsName = outterXML.Split('"');
                string name = lsName[1];
                node.InnerXml = "<value>" + dic[name] + "</value>";
            }
            docXML.Save(Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile) + "\\" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + @".exe.config");
            return true;
        }
    }
}
