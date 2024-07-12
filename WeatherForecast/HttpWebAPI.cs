using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.Json;

namespace WeatherForecast
{
    class HttpWebAPI
    {
        //私人Key,冒用必究
        private const string Key = @"63e1d3310c68482da4d2873d3ed20b7d";
        private const string NowTime = @"https://devapi.qweather.com/v7/weather/now?";
        private const string Warn = @"https://devapi.qweather.com/v7/warning/list?";
        private const string GetCityID = @"https://geoapi.qweather.com/v2/city/lookup?";

        private string City = "";
        private string CityId = "";
        private string UpdateTime = "";
        private string Temp = "";
        private string ICO = "";
        private string Text = "";
        private string Warns = "";

        private string HttpGet = "";

        public string GetCity() { return City; }
        public string GetCityId() { return CityId; }
        public string GetUpdateTime() { return UpdateTime; }
        public string GetICO() { return ICO; }
        public string GetText() { return Text; }
        public string GetTemp() { return Temp; }
        public string GetWarns() { return Warns; }

        public bool SetCityId(string City)
        {
            HttpGet = HttpWebRequest_Get(GetCityID + "location=" + City + "&key=" + Key);
            string value_Code = HttpGet.Substring(HttpGet.IndexOf("code"));
            string code = value_Code.Split("\"")[2];
            if (code != "200") return false;

            int index_1 = HttpGet.IndexOf("name");
            int index_2 = HttpGet.IndexOf("lat");
            string value_1 = HttpGet.Substring(index_1, index_2 - index_1 - 1);
            this.City = value_1.Split("\"")[2];
            this.CityId = value_1.Split("\"")[6];

            HttpGet = "";
            return true;
        }

        public bool SetTemp() 
        {
            HttpGet = HttpWebRequest_Get(NowTime + "location=" + CityId + "&key=" + Key);
            string value_Code = HttpGet.Substring(HttpGet.IndexOf("code"));
            string code = value_Code.Split("\"")[2];
            if (code != "200") return false;

            int index_T = HttpGet.IndexOf("temp");
            int index_T2 = HttpGet.IndexOf("feelsLike");
            string value_T = HttpGet.Substring(index_T, index_T2 - index_T - 1);
            this.Temp = value_T.Split("\"")[2];

            int index_I = HttpGet.IndexOf("icon");
            int index_I2 = HttpGet.IndexOf("text");
            string value_I = HttpGet.Substring(index_I, index_I2 - index_I - 1);
            this.ICO = value_I.Split("\"")[2];

            int index_Te = HttpGet.IndexOf("text");
            int index_Te2 = HttpGet.IndexOf("wind360");
            string value_Te = HttpGet.Substring(index_Te, index_Te2 - index_Te - 1);
            this.Text = value_Te.Split("\"")[2];

            int index_W = HttpGet.IndexOf("windDir");
            int index_W2 = HttpGet.IndexOf("windScale");
            string value_W = HttpGet.Substring(index_W, index_W2 - index_W - 1);
            this.Text += ("," + value_W.Split("\"")[2]);

            HttpGet = "";
            return true;
        }

        public bool SetWarn() 
        {
            HttpGet = HttpWebRequest_Get(Warn + "location=" + CityId + "&key=" + Key);
            string value_Code = HttpGet.Substring(HttpGet.IndexOf("code"));
            string code = value_Code.Split("\"")[2];
            if (code != "200") return false;

            int index_W = HttpGet.IndexOf("warning");
            string value_W = HttpGet.Substring(index_W);
            int index_W2 = value_W.IndexOf(",");
            string value_W2 = value_W.Substring(0, index_W2);
            if (value_W2.Split("\"").Length <= 1) return false;

            int index_Warn = HttpGet.IndexOf("title");
            int index_Warn2 = HttpGet.IndexOf("startTime");
            string value_Warn2 = HttpGet.Substring(index_Warn, index_Warn2 - index_Warn - 1);
            this.Warns = value_Warn2.Split("\"")[2];

            HttpGet = "";
            return true;
        }

        private static string HttpWebRequest_Get(string url)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            gZipWebClient wc = new gZipWebClient();
            wc.BaseAddress = url;
            wc.Encoding = System.Text.Encoding.UTF8;
            string ret = wc.DownloadString(url);
            return ret;
        }     
    }
}
