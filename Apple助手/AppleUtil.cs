using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Apple
{
    public  class AppleUtil
    {

        /// <summary>
        /// 获取Cookie
        /// </summary>
        /// <param name="setCookieStr"></param>
        /// <returns></returns>
        public static CookieContainer GetCookiesByHeader(string host,string setCookieStr)
        {
            CookieContainer cookies = new CookieContainer();
            LogHelper.OutLog("获取ResponseCookies");
             string[] cookieArr = setCookieStr.Split(',');
            foreach (string item in cookieArr)
            {
                try
                {
                    string[] keyArr = item.Split(';');
                    string name = keyArr[0].Split('=')[0];
                    string value = keyArr[0].Split('=')[1];
                    string domain = host;
                    string path = "/";
                    if (item.Contains("Domain"))
                    {
                        domain = keyArr[1].Split('=')[1];
                    }
                   // LogHelper.outLog("{0}={1},domain={2}", name, value, domain);
                    Cookie cookie = new Cookie(name, value, path, domain);
                    cookies.Add(cookie);
                }
                catch (Exception e)
                {
                  //  LogHelper.OutLog(Color.Red,e.ToString());
                }
            }
            return cookies;
        }
    }
}
