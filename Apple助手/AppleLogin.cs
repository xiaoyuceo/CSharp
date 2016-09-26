using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Apple
{
   public class AppleLogin
    {
        public CookieCollection responseCookies;
        public string Post(string url,string postData,CookieContainer myCookieContainer)
        {
            string outdata = "";
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            //设置请求头
            myHttpWebRequest.Accept = "application / json, text / javascript, */*; q=0.01";
            myHttpWebRequest.Host = "idmsa.apple.com";
            myHttpWebRequest.ContentType = "application/json";
            myHttpWebRequest.Referer = "https://idmsa.apple.com/appleauth/auth/signin?widgetKey=af1139274f266b22b68c2a3e7ad932cb3c0bbe854e13a79af78dcc73136882c3&language=zh_CN&rv=1";
            myHttpWebRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
            myHttpWebRequest.UserAgent = ":Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36";
            myHttpWebRequest.Headers.Add("Origin", "https://idmsa.apple.com");
            myHttpWebRequest.Headers.Add("X-Apple-Domain-Id", "1");
           // myHttpWebRequest.Headers.Add("X-Apple-I-FD-Client-Info", "{\"U\":\"Mozilla / 5.0(Windows NT 6.1; WOW64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 52.0.2743.116 Safari / 537.36\",\"L\":\"zh - CN\",\"Z\":\"GMT + 08:00\",\"V\":\"1.1\",\"F\":\"cta44j1e3NlY5BSo9z4ofjb75PaK4Vpjt.gEngMQEjZrVglE4YcA.0Yz3ccbbJYMLgiPFU77qZoOSix5ezdstlYysrhsui65KQnK94CaJ6hO3f9p_nH1u_eH3BhxUC550ialT0iakA2zGUMnGWFfwMHDCQyFA2wv4qnvtCsABIlNu1k.Nzl998tp7ppfAaZ6m1CdC5MQjGejuTDRNziCvTDfWl_LwpHWIO_0vLG9mhORoVijvw2WwjftckvIhIDLTK43xbJlpMpwoNSUC56MnGWpwoNHHACVZXnN95KBeZxYSFHejpidPNs0ojpVMZ90L5H6fqUdHz15tJV0TJ6Lxp9hYjSEPy.EKY.6ekcV.vgRKEpVMZBHgBZELKy3nwrLuVr9Z.AmeurNW5CfUXtStKjE4PIDxO9sPrsiMTKQnlLZnjxHfUhkY5BSmV5BNlan0Os5Apw.9o4\"}");
            myHttpWebRequest.Headers.Add("X-Apple-Locale", "zh_CN");
            myHttpWebRequest.Headers.Add("X-Apple-Widget-Key", "af1139274f266b22b68c2a3e7ad932cb3c0bbe854e13a79af78dcc73136882c3");
            myHttpWebRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
           // myHttpWebRequest.Headers.Add("X-Apple-I-FD-Client-Info", "{\"U\":\"Mozilla / 5.0(Windows NT 6.1; WOW64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 52.0.2743.116 Safari / 537.36\",\"L\":\"zh - CN\",\"Z\":\"GMT + 08:00\",\"V\":\"1.1\",\"F\":\"cta44j1e3NlY5BSo9z4ofjb75PaK4Vpjt.gEngMQEjZrVglE4YcA.0Yz3ccbbJYMLgiPFU77qZoOSix5ezdstlYysrhsui65KQnK94CaJ6hO3f9p_nH1u_eH3BhxUC550ialT0iakA2zGUMnGWFfwMHDCQyFA2wv4qnvtCsABIlNu1k.Nzl998tp7ppfAaZ6m1CdC5MQjGejuTDRNziCvTDfWl_LwpHWIO_0vLG9mhORoVijvw2WwjftckvIhIDLTK43xbJlpMpwoNSUC56MnGWpwoNHHACVZXnN95KBgcTeijJjLzLu_dYV6HzL0TFc4NO7TjOy_Aw7Q_H.4tFSQrJeqDxpSQs.xLB.Tf1X.2illJzL0TFc4NO7TjOyOAwELuVr9Z.AmeurNW5CfUXtStKjE4PIDxO9sPrsiMTKQnlLZnjxHfUhkY5BSmV5BNlan0Os5Apw.9Pg\"}");

            myHttpWebRequest.Method = "POST";
            myHttpWebRequest.CookieContainer = myCookieContainer;
            //设置HttpWebRequest的CookieContainer为刚才建立的那个myCookieContainer
            Stream myRequestStream = myHttpWebRequest.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
            myStreamWriter.Write(postData);
            //把数据写入HttpWebRequest的Request流
            myStreamWriter.Close();
            myRequestStream.Close();
            //关闭打开对象
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            System.Windows.Forms.MessageBox.Show(myHttpWebResponse.StatusCode+"");
            responseCookies =   myHttpWebResponse.Cookies;
            Console.WriteLine("==========================Cookie==========================================");
            foreach (Cookie item in responseCookies)
            {
                Console.WriteLine("{0}:{1}  {2}",item.Name,item.Value,item.Domain);
            }
           WebHeaderCollection responseHeaders=  myHttpWebResponse.Headers;
            Console.WriteLine("==========================ResponseHeader==========================================");
            foreach (string item in responseHeaders.AllKeys)
            {
                Console.WriteLine("{0}:{1}", item, responseHeaders.Get(item));
            }
            //新建一个HttpWebResponse
            myHttpWebResponse.Cookies = myCookieContainer.GetCookies(myHttpWebRequest.RequestUri);
            //获取一个包含url的Cookie集合的CookieCollection
            Stream myResponseStream = myHttpWebResponse.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            outdata = myStreamReader.ReadToEnd();
            //把数据从HttpWebResponse的Response流中读出
            myStreamReader.Close();
            myResponseStream.Close();
            return outdata;
        }
    }
}
