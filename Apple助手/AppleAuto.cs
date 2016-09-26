using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Apple
{
    public class AppleAuto
    {
        public static CookieCollection responseCookies;

        public static string UserAgent ="Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36";
        public static CookieContainer OpenWeb(out string widgetKey)
        {
            try
            {
                widgetKey = "";
                LogHelper.OutLog("开始打开https://appleid.apple.com/网站");
                CookieContainer cookies = new CookieContainer();
                string url = "https://appleid.apple.com/";
                string outdata = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                //设置请求头
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");
                request.Headers.Add(HttpRequestHeader.KeepAlive, "true");
                request.Host = "appleid.apple.com";
                request.UserAgent = UserAgent;
                request.Method = "GET";
                request.CookieContainer = cookies;
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)request.GetResponse();
                if (myHttpWebResponse.StatusCode != HttpStatusCode.OK)
                {
                    LogHelper.OutLog(Color.Red, "打开https://appleid.apple.com/网站失败");
                    AppleHelper.State = ExecuteState.Error;
                    return null;
                }
                LogHelper.OutLog("成功打开https://appleid.apple.com/");
                responseCookies = myHttpWebResponse.Cookies;
                WebHeaderCollection responseHeaders = myHttpWebResponse.Headers;
                foreach (string item in responseHeaders.AllKeys)
                {
                    if ("Set-Cookie".Equals(item))
                    {
                        cookies = AppleUtil.GetCookiesByHeader(request.Host, responseHeaders.Get(item));
                    }
                }

                Stream myResponseStream = myHttpWebResponse.GetResponseStream();

                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                outdata = myStreamReader.ReadToEnd();

                myStreamReader.Close();
                myResponseStream.Close();
                Regex reg = new Regex("serviceKey:(.*?)',");
                Match m = reg.Match(outdata);
                if (m.Success)
                {
                    widgetKey = m.Groups[1].Value.Trim();
                    widgetKey = widgetKey.Replace("'", "");
                    LogHelper.OutLog("获取到WidgetKey:{0}", widgetKey);
                }
                else
                {
                    LogHelper.OutLog(Color.Red, "获取到WidgetKey失败}");
                    AppleHelper.State = ExecuteState.Error;
                    return null;
                }
                AppleHelper.State = ExecuteState.Sucess;
                return cookies;
            }
            catch (Exception ex)
            {
                LogHelper.OutLog(Color.Red,ex.Message);
                AppleHelper.State = ExecuteState.Error;
                widgetKey = "";
                return null;
            }
        }

        public static CookieContainer OpenLoginPage(CookieContainer cookies, string widgetKey)
        {
            try
            {
                LogHelper.OutLog("开始打开登录页面");
                string url = "https://idmsa.apple.com/appleauth/auth/signin?widgetKey=" + widgetKey + "&language=zh_CN&rv=1";
                string outdata = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                //设置请求头
                request.Accept = "application / json, text / javascript, */*; q=0.01";
                request.Host = "idmsa.apple.com";
                request.ContentType = "application/json";
                request.Referer = "https://appleid.apple.com";
                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh - CN,zh; q = 0.8");
                
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "utf-8");
                request.UserAgent = UserAgent;
                request.Headers.Add("Origin", "https://idmsa.apple.com");
                request.Headers.Add("Upgrade-Insecure-Requests", "1");
                request.Headers.Add("X-Apple-Locale", "zh_CN");

                request.Method = "GET";
                request.CookieContainer = cookies;


                HttpWebResponse myHttpWebResponse = (HttpWebResponse)request.GetResponse();
                if (myHttpWebResponse.StatusCode != HttpStatusCode.OK)
                {
                    LogHelper.OutLog(Color.Red, "打开登录页面失败");
                    AppleHelper.State = ExecuteState.Error;
                    return null;
                }
                else
                {
                    LogHelper.OutLog("成功打开登录页面");
                }
                WebHeaderCollection responseHeaders = myHttpWebResponse.Headers;
                foreach (string item in responseHeaders.AllKeys)
                {
                    if ("Set-Cookie".Equals(item))
                    {
                        cookies = AppleUtil.GetCookiesByHeader(request.Host, responseHeaders.Get(item));
                    }
                }
                AppleHelper.State = ExecuteState.Sucess;
                return cookies;
            }
            catch (Exception ex)
            {
                LogHelper.OutLog(Color.Red,ex.Message);
                AppleHelper.State = ExecuteState.Error;
                return null;
            }
        }


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="postData"></param>
        /// <param name="widgetKey"></param>
        /// <param name="myCookieContainer"></param>
        /// <returns></returns>
        public static CookieContainer Login(string postData, string widgetKey, CookieContainer cookies)
        {
            try
            {
                LogHelper.OutLog("开始登录");
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

                string url = "https://idmsa.apple.com/appleauth/auth/signin";
                string outdata = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                
                request.ProtocolVersion = HttpVersion.Version11;
                //设置请求头
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Accept = "application / json, text / javascript, */*; q=0.01";
                request.Host = "idmsa.apple.com";
                request.ContentType = "application/json";
                request.Referer = "https://idmsa.apple.com/appleauth/auth/signin?widgetKey=widgetKey&language=zh_CN&rv=1";
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "utf-8");
                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");
                request.UserAgent = UserAgent;
                request.Headers.Add("Origin", "https://idmsa.apple.com");
                request.Headers.Add("X-Apple-Domain-Id", "1");
                request.Headers.Add("X-Apple-Locale", "zh_CN");
                request.Headers.Add("X-Apple-Widget-Key", widgetKey);
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                request.KeepAlive = true;
                // request.Headers.Add("X-Apple-I-FD-Client-Info", "{\"U\":\"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36\",\"L\":\"zh-CN\",\"Z\":\"GMT+08:00\",\"V\":\"1.1\",\"F\":\"Vta44j1e3NlY5BSo9z4ofjb75PaK4Vpjt.gEngMQEjZrVglE4YcA.0Yz3ccbbJYMLgiPFU77qZoOSix5ezdstlYysrhsui65KQnK94CaJ6hO3f9p_nH1u_eH3BhxUC550ialT0iakA2zGUMnGWFfwMHDCQyFA2wv4qnvtCsABIlNu1k.Nzl998tp7ppfAaZ6m1CdC5MQjGejuTDRNziCvTDfWl_LwpHWIO_0vLG9mhORoVijvw2WwjftckvIhIDLTK43xbJlpMpwoNSUC56MnGWpwoNHHACVZXnN95J8TrOG.odfxQeLaD.SAxN4t1VKWZWuxbuJjkWiK7.M_0pjSHfNvrJay.EKY.6ekcV.vgRMfxN4t1VKWZWumjkVUWASFQ_H.4tKSQgFY5pjlRiwerbXh8bTg_RCQwMAj9htsfHOrydKjqsGY5DtF25BNnOVgw24uy.4Sz\"}");

                request.Method = "POST";
                request.CookieContainer = cookies;
                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
                myStreamWriter.Write(postData);
                myStreamWriter.Close();
                myRequestStream.Close();
                //关闭打开对象
                HttpWebResponse myHttpWebResponse = null;

                try
                {
                    myHttpWebResponse = (HttpWebResponse)request.GetResponse();
                    cookies.Add(myHttpWebResponse.Cookies);
                }
                catch (WebException e)
                {
                    myHttpWebResponse = (HttpWebResponse)e.Response;
                    LogHelper.OutLog(Color.Red, e.Message);
                    AppleHelper.State = ExecuteState.Error;
                    return null;
                }


             
                Stream myResponseStream = myHttpWebResponse.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                outdata = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                // LogHelper.OutLog(Color.Yellow, outdata);
                LogHelper.OutLog("登录成功");
                AppleHelper.State = ExecuteState.Sucess;
                return cookies;
            }
            catch (Exception ex)
            {
                LogHelper.OutLog(Color.Red,ex.Message);
                AppleHelper.State = ExecuteState.Error;
                return null;
            }
        }


        public static string GetToken(CookieContainer cookies)
        {

            try
            {
                LogHelper.OutLog("与网站协商访问秘钥");
                string token = "";
                string url = "https://getsupport.apple.com/?caller=home&PRKEYS=";
                string outdata = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                //设置请求头
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.Host = "getsupport.apple.com";
                request.ContentType = "application/json";
                request.Referer = "https://support.apple.com/zh-cn";
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "utf-8");
                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");
                request.UserAgent = UserAgent;

                request.Headers.Add("Upgrade-Insecure-Requests", "1");
                request.Headers.Add("X-Apple-Locale", "zh_CN");

                request.Method = "GET";
                request.CookieContainer = cookies;

                //关闭打开对象
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)request.GetResponse();

                responseCookies = myHttpWebResponse.Cookies;

                WebHeaderCollection responseHeaders = myHttpWebResponse.Headers;
                foreach (string item in responseHeaders.AllKeys)
                {
                    if (item.IndexOf("X-Apple-CSRF-Token") > -1)
                    {
                        token = responseHeaders.Get(item);
                        LogHelper.OutLog("获取到访问密钥:{0}", token);
                        break;
                    }
                }
                foreach (string item in responseHeaders.AllKeys)
                {
                    if ("Set-Cookie".Equals(item))
                    {
                        cookies = AppleUtil.GetCookiesByHeader(request.Host, responseHeaders.Get(item));
                    }
                }
                if (string.IsNullOrEmpty(token))
                {
                    LogHelper.OutLog(Color.Red, "获取访问密钥失败");
                    AppleHelper.State = ExecuteState.Error;
                    return null;
                }
                Stream myResponseStream = myHttpWebResponse.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                outdata = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                //LogHelper.OutLog(Color.Yellow,outdata);
                AppleHelper.State = ExecuteState.Sucess;
                return token;
            }
            catch (Exception ex)
            {
                LogHelper.OutLog(Color.Red,ex.Message.ToString());
                AppleHelper.State = ExecuteState.Error;
                return "";
            }
        }


        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public static string Gethierarchy(CookieContainer cookies)
        {

            try
            {
                LogHelper.OutLog("开始获取产品列表");
                string url = "https://getsupport.apple.com/web/v1/product/hierarchy";
                string outdata = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                //设置请求头
                request.Host = "getsupport.apple.com";
                request.ContentType = "application/json; charset=UTF-8";
                request.Referer = "https://getsupport.apple.com";
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");

                SetHeaderValue(request.Headers, "Connection", "keep-alive");
                request.UserAgent = UserAgent;

                request.Headers.Add("X-Requested-With", "XMLHttpRequest");

                request.Method = "GET";
                request.CookieContainer = cookies;

                //关闭打开对象
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)request.GetResponse();
                if (myHttpWebResponse.StatusCode != HttpStatusCode.OK)
                {
                    LogHelper.OutLog(Color.Red, "获取产品列表失败");
                    AppleHelper.State = ExecuteState.Error;
                    return null;
                }
                cookies.Add(myHttpWebResponse.Cookies);
                //WebHeaderCollection responseHeaders = myHttpWebResponse.Headers;
                //foreach (string item in responseHeaders.AllKeys)
                //{
                //    if ("Set-Cookie".Equals(item))
                //    {
                //        cookies = AppleUtil.GetCookiesByHeader(request.Host, responseHeaders.Get(item));
                //    }
                //}
                Stream myResponseStream = myHttpWebResponse.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                outdata = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                LogHelper.OutLog(outdata);
                AppleHelper.State = ExecuteState.Sucess;
                return outdata;
            }
            catch (Exception ex)
            {
                LogHelper.OutLog(Color.Red,ex.Message);
                AppleHelper.State = ExecuteState.Error;
                return "";
            }
        }


        public static string Getmyproducts(string token, CookieContainer cookies)
        {
            try
            {
                LogHelper.OutLog("开始我的设备列表");
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

                string url = "https://getsupport.apple.com/web/v1/product/myproducts";
                string outdata = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ProtocolVersion = HttpVersion.Version11;
                //设置请求头
                request.Host = "getsupport.apple.com";
                request.ContentType = "application/json; charset=UTF-8";
                request.Headers.Add("Origin", "https://getsupport.apple.com");
                request.Referer = "https://getsupport.apple.com/";
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");

                SetHeaderValue(request.Headers, "Connection", "keep-alive");
                request.UserAgent = UserAgent;

                request.Headers.Add("X-Apple-CSRF-Token", token);
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");

                request.Method = "POST";
                request.CookieContainer = cookies;
                string postData = "{\"limit\":6,\"pageNumber\":1,\"superGroupId\":\"\",\"prodFamilyClassId\":\"\",\"prodGroupFamilyId\":\"\",\"userIntent\":false}";
                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
                myStreamWriter.Write(postData);
                myStreamWriter.Close();
                myRequestStream.Close();

                //关闭打开对象
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)request.GetResponse();
                if (myHttpWebResponse.StatusCode != HttpStatusCode.OK)
                {
                    LogHelper.OutLog(Color.Red, "获取我的设备列表失败");
                    AppleHelper.State = ExecuteState.Error;
                    return null;
                }
                cookies.Add(myHttpWebResponse.Cookies);


                Stream myResponseStream = myHttpWebResponse.GetResponseStream();

                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                outdata = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                LogHelper.OutLog("我的设备列表:");
                LogHelper.OutLog(outdata);
                if (outdata.IndexOf("\"user\":{\"firstName\"")<0)
                {
                    AppleHelper.State = ExecuteState.Error;
                    return outdata;
                }
                AppleHelper.State = ExecuteState.Sucess;
                return outdata;
            }
            catch (Exception ex)
            {
                LogHelper.OutLog(Color.Red,ex.Message);
                AppleHelper.State = ExecuteState.Error;
                return "";
            }
        }

        public static string Gettopics(string token,  CookieContainer cookies)
        {
            try
            {
                LogHelper.OutLog("开始获取帮助主题"); ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

                string url = "https://getsupport.apple.com/web/v1/topics";
                string postData = "{\"superGroupId\":\"SG005\",\"prodGroupFamilyId\":\"PGF51002\"}";
                string outdata = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ProtocolVersion = HttpVersion.Version11;
                //设置请求头
                request.Host = "getsupport.apple.com";
                request.Headers.Add("Origin", "https://getsupport.apple.com");
                request.ContentType = "application/json; charset=UTF-8";
                request.Referer = "https://getsupport.apple.com/";
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");

                SetHeaderValue(request.Headers, "Connection", "keep-alive");
                request.UserAgent = UserAgent;

                request.Headers.Add("X-Apple-CSRF-Token", token);
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");

                request.Method = "POST";
                request.CookieContainer = cookies;
                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
                myStreamWriter.Write(postData);
                myStreamWriter.Close();
                myRequestStream.Close();

                //关闭打开对象
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)request.GetResponse();
                if (myHttpWebResponse.StatusCode != HttpStatusCode.OK)
                {
                    LogHelper.OutLog(Color.Red, "获取帮助主题失败");
                    AppleHelper.State = ExecuteState.Error;
                    return null;
                }
                cookies.Add(myHttpWebResponse.Cookies);
                WebHeaderCollection responseHeaders = myHttpWebResponse.Headers;
                foreach (string item in responseHeaders.AllKeys)
                {
                    if ("Set-Cookie".Equals(item))
                    {
                        cookies = AppleUtil.GetCookiesByHeader(request.Host, responseHeaders.Get(item));
                    }
                   // LogHelper.OutLog("{0}:{1}", item, responseHeaders.Get(item));
                }

                Stream myResponseStream = myHttpWebResponse.GetResponseStream();

                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                outdata = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                LogHelper.OutLog("帮助主题:");
                LogHelper.OutLog(outdata);
                AppleHelper.State = ExecuteState.Sucess;
                return outdata;
            }
            catch (Exception ex)
            {
                LogHelper.OutLog(Color.Red,ex.Message);
                AppleHelper.State = ExecuteState.Error;
                return "";
            }
        }


        public static string Gettriggers(string token, string postData, CookieContainer cookies)
        {
            try
            {
                LogHelper.OutLog("获取解决方案");
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

                string url = "https://getsupport.apple.com/web/v1/triggers";
                string outdata = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ProtocolVersion = HttpVersion.Version11;
                //设置请求头
                request.Host = "getsupport.apple.com";
                request.Headers.Add("Origin", "https://getsupport.apple.com");
                request.ContentType = "application/json; charset=UTF-8";
                request.Referer = "https://getsupport.apple.com/";
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");

                SetHeaderValue(request.Headers, "Connection", "keep-alive");
                request.UserAgent = UserAgent;

                request.Headers.Add("X-Apple-CSRF-Token", token);
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");

                request.Method = "POST";
                request.CookieContainer = cookies;
                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
                myStreamWriter.Write(postData);
                myStreamWriter.Close();
                myRequestStream.Close();

                //关闭打开对象
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)request.GetResponse();
                if (myHttpWebResponse.StatusCode != HttpStatusCode.OK)
                {
                    LogHelper.OutLog(Color.Red, "获取解决方案失败");
                    AppleHelper.State = ExecuteState.Error;
                    return null;
                }
                cookies.Add(myHttpWebResponse.Cookies);

                WebHeaderCollection responseHeaders = myHttpWebResponse.Headers;
                foreach (string item in responseHeaders.AllKeys)
                {
                    if ("Set-Cookie".Equals(item))
                    {
                        cookies = AppleUtil.GetCookiesByHeader(request.Host, responseHeaders.Get(item));
                    }
                   // LogHelper.OutLog("{0}:{1}", item, responseHeaders.Get(item));
                }

                Stream myResponseStream = myHttpWebResponse.GetResponseStream();

                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                outdata = myStreamReader.ReadToEnd();

                //把数据从HttpWebResponse的Response流中读出
                myStreamReader.Close();
                myResponseStream.Close();
                LogHelper.OutLog("解决方案:");
                LogHelper.OutLog(outdata);
                AppleHelper.State = ExecuteState.Sucess;
                return outdata;
            }
            catch (Exception ex)
            {
                LogHelper.OutLog(Color.Red,ex.Message);
                AppleHelper.State = ExecuteState.Error;
                return "";
            }
        }

        public static string Geteligibility(string token, string postData, CookieContainer cookies)
        {
            try
            {
                LogHelper.OutLog("开始选择解决方案");
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

                string url = "https://getsupport.apple.com/web/v1/diagnostics/eligibility";
                string outdata = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ProtocolVersion = HttpVersion.Version11;
                //设置请求头
                request.Host = "getsupport.apple.com";
                request.Headers.Add("Origin", "https://getsupport.apple.com");
                request.ContentType = "application/json; charset=UTF-8";
                request.Referer = "https://getsupport.apple.com/";
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");

                SetHeaderValue(request.Headers, "Connection", "keep-alive");
                request.UserAgent = UserAgent;

                request.Headers.Add("X-Apple-CSRF-Token", token);
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");

                request.Method = "POST";
                request.CookieContainer = cookies;
                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
                myStreamWriter.Write(postData);
                myStreamWriter.Close();
                myRequestStream.Close();

                //关闭打开对象
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)request.GetResponse();
                if (myHttpWebResponse.StatusCode != HttpStatusCode.OK)
                {
                    LogHelper.OutLog(Color.Red, "选取解决方案失败");
                    AppleHelper.State = ExecuteState.Error;
                    return null;
                }
                //cookies.Add(myHttpWebResponse.Cookies);

                WebHeaderCollection responseHeaders = myHttpWebResponse.Headers;
                foreach (string item in responseHeaders.AllKeys)
                {
                    if ("Set-Cookie".Equals(item))
                    {
                        cookies = AppleUtil.GetCookiesByHeader(request.Host, responseHeaders.Get(item));
                    }
                  //  LogHelper.OutLog("{0}:{1}", item, responseHeaders.Get(item));
                }

                Stream myResponseStream = myHttpWebResponse.GetResponseStream();

                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                outdata = myStreamReader.ReadToEnd();

                myStreamReader.Close();
                myResponseStream.Close();
                LogHelper.OutLog("选择解决方案成功");
                LogHelper.OutLog(outdata);
                AppleHelper.State = ExecuteState.Sucess;
                return outdata;
            }
            catch (Exception ex)
            {
                LogHelper.OutLog(Color.Red,ex.Message);
                AppleHelper.State = ExecuteState.Error;
                return "";
            }
        }

        public static PageData Getsolutions(string token, string postData, CookieContainer cookies)
        {
            try
            {
                LogHelper.OutLog("开始获取账户信息");
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

                string url = "https://getsupport.apple.com/web/v1/solutions";
                string outdata = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ProtocolVersion = HttpVersion.Version11;
                //设置请求头
                request.Host = "getsupport.apple.com";
                request.Headers.Add("Origin", "https://getsupport.apple.com");
                request.ContentType = "application/json; charset=UTF-8";
                request.Referer = "https://getsupport.apple.com/";
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
                request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
                request.Headers.Add("X-Apple-Locale", "zh_CN");

                SetHeaderValue(request.Headers, "Connection", "keep-alive");
                request.UserAgent = UserAgent;

                request.Headers.Add("X-Apple-CSRF-Token", token);
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");

                request.Method = "POST";
                request.CookieContainer = cookies;
                CookieCollection cookieCol = cookies.GetCookies(new Uri("https://getsupport.apple.com"));
                foreach (Cookie item in cookieCol)
                {
                    LogHelper.OutLog(Color.Yellow, "{0}:{1}", item.Name, item.Value);
                }
                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
                LogHelper.OutLog(Color.Yellow, postData);
                myStreamWriter.Write(postData);
                myStreamWriter.Close();
                myRequestStream.Close();

                //关闭打开对象
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)request.GetResponse();
                if (myHttpWebResponse.StatusCode != HttpStatusCode.OK)
                {
                    LogHelper.OutLog(Color.Red, "获取账户信息失败");
                    AppleHelper.State = ExecuteState.Error;
                    return null;
                }
                WebHeaderCollection responseHeaders = myHttpWebResponse.Headers;
                foreach (string item in responseHeaders.AllKeys)
                {
                    if ("Set-Cookie".Equals(item))
                    {
                        cookies = AppleUtil.GetCookiesByHeader(request.Host, responseHeaders.Get(item));
                    }
                  //  LogHelper.OutLog("{0}:{1}", item, responseHeaders.Get(item));
                }
                Stream myResponseStream = myHttpWebResponse.GetResponseStream();

                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                outdata = myStreamReader.ReadToEnd();
                if (outdata.IndexOf("error") > -1)
                {
                    LogHelper.OutLog(Color.Red, outdata);
                    AppleHelper.State = ExecuteState.Error;
                    return null;
                }
                PageData curUser = null;
                try
                {
                    PageData data = (PageData)JsonConvert.DeserializeObject(outdata, typeof(PageData));
                    PageData session = (PageData)JsonConvert.DeserializeObject(data["session"].ToString(), typeof(PageData));
                    PageData user = (PageData)JsonConvert.DeserializeObject(session["user"].ToString(), typeof(PageData));
                    string firstName = user["firstName"].ToString();
                    string lastName = user["lastName"].ToString();
                    curUser = new PageData();
                    curUser.Add("firstName", firstName);
                    curUser.Add("lastName", lastName);
                }
                catch (Exception e)
                {
                    LogHelper.OutLog(Color.Red, outdata);
                    AppleHelper.State = ExecuteState.Error;
                    return null;
                }

                myStreamReader.Close();
                myResponseStream.Close();
                LogHelper.OutLog("账户信息");
                LogHelper.OutLog(curUser.ToString());
                AppleHelper.State = ExecuteState.Sucess;
                return curUser;
            }
            catch (Exception ex)
            {
                LogHelper.OutLog(Color.Red,ex.Message);
                AppleHelper.State = ExecuteState.Error;
                return null;
            }
        }

        public static CookieContainer GetEML(string token, string postData,CookieContainer cookies)
        {
            try
            {
                LogHelper.OutLog("开始获取表单");
                string url = "https://getsupport.apple.com/web/v1/execute";
                string outdata = "";
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ProtocolVersion = HttpVersion.Version11;
                //设置请求头
                request.Host = "getsupport.apple.com";
                request.Headers.Add("Origin", "https://getsupport.apple.com");
                request.ContentType = "application/json; charset=UTF-8";
                request.Referer = "https://getsupport.apple.com/?caller=home&PRKEYS=";
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");
                request.Headers.Add("X-Apple-Locale", "zh_CN");

                SetHeaderValue(request.Headers, "Connection", "keep-alive");
                request.UserAgent = UserAgent;

                request.Headers.Add("X-Apple-CSRF-Token", token);
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");

                request.Method = "POST";
                request.CookieContainer = cookies;
                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
                myStreamWriter.Write(postData);
                myStreamWriter.Close();
                myRequestStream.Close();

                CookieCollection cookieCol = cookies.GetCookies(new Uri("https://getsupport.apple.com"));
                foreach (Cookie item in cookieCol)
                {
                    LogHelper.OutLog(Color.Yellow, "{0}:{1}", item.Name, item.Value);
                }

                //关闭打开对象
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)request.GetResponse();
                if (myHttpWebResponse.StatusCode != HttpStatusCode.OK)
                {
                    LogHelper.OutLog(Color.Red, "获取表单失败");
                    AppleHelper.State = ExecuteState.Error;
                    return null;
                }
                WebHeaderCollection responseHeaders = myHttpWebResponse.Headers;
                foreach (string item in responseHeaders.AllKeys)
                {
                    if ("Set-Cookie".Equals(item))
                    {
                        cookies = AppleUtil.GetCookiesByHeader(request.Host, responseHeaders.Get(item));
                    }
                }

                Stream myResponseStream = myHttpWebResponse.GetResponseStream();

                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                outdata = myStreamReader.ReadToEnd();


                //把数据从HttpWebResponse的Response流中读出
                myStreamReader.Close();
                myResponseStream.Close();
                LogHelper.OutLog("表单:");
                LogHelper.OutLog(outdata);
                AppleHelper.State = ExecuteState.Sucess;
                return cookies;
            }
            catch (Exception ex)
            {
                LogHelper.OutLog(Color.Red,ex.Message);
                AppleHelper.State = ExecuteState.Error;
                return null;
            }
        }

        public static string Execute(string token, string postData, CookieContainer cookies)
        {
            try
            {
                LogHelper.OutLog("开始提交表单");
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

                string url = "https://getsupport.apple.com/web/v1/execute";
                string outdata = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ProtocolVersion = HttpVersion.Version11;
                //设置请求头
                request.Host = "getsupport.apple.com";
                request.Headers.Add("Origin", "https://getsupport.apple.com");
                request.ContentType = "application/json";
                request.Referer = "https://getsupport.apple.com/?caller=home&PRKEYS=";
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");

                SetHeaderValue(request.Headers, "Connection", "keep-alive");
                request.UserAgent = UserAgent;

                request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
                request.Headers.Add("X-Apple-CSRF-Token", token);
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");

                request.Method = "POST";
                request.CookieContainer = cookies;
                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
                myStreamWriter.Write(postData);
                myStreamWriter.Close();
                myRequestStream.Close();

                CookieCollection cookieCol = cookies.GetCookies(new Uri("https://getsupport.apple.com"));
                foreach (Cookie item in cookieCol)
                {
                    LogHelper.OutLog(Color.Yellow, "{0}:{1}", item.Name, item.Value);
                }

                //关闭打开对象
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)request.GetResponse();
                if (myHttpWebResponse.StatusCode != HttpStatusCode.OK)
                {
                    LogHelper.OutLog(Color.Red, "提交表单失败");
                    return null;
                }
                responseCookies = myHttpWebResponse.Cookies;

                Stream myResponseStream = myHttpWebResponse.GetResponseStream();

                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                outdata = myStreamReader.ReadToEnd();

                
                myStreamReader.Close();
                myResponseStream.Close();
                LogHelper.OutLog("提交表单");
                LogHelper.OutLog(outdata);
                //出现错误
                if (outdata.IndexOf("errorDetail") > 0)
                {
                    AppleHelper.State = ExecuteState.Error;
                    return outdata;
                }
                else if (outdata.IndexOf("\"data\":{\"caseId\"") > 0)
                {
                   PageData result =  JsonConvert.DeserializeObject<PageData>(outdata);
                    PageData data = JsonConvert.DeserializeObject<PageData>(result.GetString("data"));
                    AppleHelper.State = ExecuteState.Sucess;
                    return outdata;
                }
                else {
                    AppleHelper.State = ExecuteState.Error;
                    return outdata;
                }
            }
            catch (Exception ex)
            {
                LogHelper.OutLog(Color.Red,ex.Message);
                AppleHelper.State = ExecuteState.Error;
                return null;
            }
        }
        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }

       
       


       

       

    }
}
