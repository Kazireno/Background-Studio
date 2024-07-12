using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast
{
#pragma warning disable SYSLIB0014 // 类型或成员已过时
    public class gZipWebClient : WebClient
    {

        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = base.GetWebRequest(address) as HttpWebRequest;
            request.AutomaticDecompression = DecompressionMethods.GZip;
            return request;
        }
    }
#pragma warning restore SYSLIB0014 // 类型或成员已过时
}
