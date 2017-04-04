using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YTMVPN_Server
{
    public class Config
    {
        public string IP_Address;
        public UInt16 IP_AuthPort;
        public UInt16 IP_DataPort;

        public int Logic_AddrLength;
        public int Logic_PortLength;
        public byte[] Logic_LocalAddr;

        /// <summary>
        /// 读取 config.json ，返回 Config 实例
        /// </summary>
        public static Config GetConfig(string filePath = "config.json")
        {
            return JsonConvert.DeserializeObject<Config>(System.IO.File.ReadAllText(filePath, Encoding.UTF8));
        }
    }
}
