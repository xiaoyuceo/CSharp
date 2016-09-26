using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Apple
{
   public class AppleHelper
    {
        public static string widgetKey = "";

        public static ExecuteState State = ExecuteState.Sucess;

        public static List<PageData> CountryLst = new List<PageData>();

        public static List<PageData> SolutionLst = new List<PageData>();

        public static DataTable AccountDT;

        public static DataTable ContentDT;

        public static PageData Record = new PageData();

        public static int ReplayCount = 0;

        public static bool RandCountry = true;

        public static bool RandContent = true;

        public static bool IsRunning = false;
        public static bool Init()
        {
            try
            {
                LogHelper.OutLog("开始初始化参数。。。");
                Const.InitData();
                LogHelper.OutLog("参数初始化完成。。。");
                return true;
            }
            catch (Exception e)
            {
                LogHelper.OutLog(Color.Red,"参数初始化失败,请检查输入文件格式是否正确！");
                return false;
            }
            
        }


        public static void Execute(LoginModel loginModel,PageData pd)
        {
           // CookieContainer  cookies =  AppleAuto.OpenLoginPage();
           // string postData = JsonConvert.SerializeObject(loginModel);
            
           // // cookie = new CookieContainer();
           // cookies.Add(new Uri("https://idmsa.apple.com"), new Cookie("dslang", "CH-ZN"));
           // cookies.Add(new Uri("https://idmsa.apple.com"), new Cookie("site", "CHN"));
           // AppleAuto.BeforeLogin(cookies, AppleHelper.widgetKey);
           // LogHelper.OutLog(Color.Red, "开始登陆。。");
           // cookies = AppleAuto.Login(postData, AppleHelper.widgetKey, cookies);
           // string token = AppleAuto.GetToken(cookies);
           //// postData = Parameter.GetParameter();
           // //postData = "{\"lastName\":\"王\",\"firstName\":\"思\",\"email\":\"lavegf188@163.com\",\"appleId\":\"3320730332@qq.com\",\"additionalDetails\":\"不知道\",\"solution\":\"GEN\",\"triggerDetails\":[{\"parentQuestions\":[{\"questionId\":\"Q60777 - T600333\",\"questionText\":\"选取您所在国家 / 地区的 iTunes Store 或 App Store：\",\"answerId\":\"A40426\",\"answerText\":\"中国\",\"repairType\":\" \",\"comptiaCode\":\"\",\"partGroup\":\"\",\"childQuestions\":[]},{\"questionId\":\"Q60778 - T600333\",\"questionText\":\"项目标题：\",\"answerId\":\"\",\"answerText\":\"你好\",\"repairType\":\"\",\"comptiaCode\":\"\",\"partGroup\":\"\",\"childQuestions\":[]},{\"questionId\":\"Q60779 - T600333\",\"questionText\":\"订单号：\",\"answerId\":\"\",\"answerText\":\"1311\",\"repairType\":\"\",\"comptiaCode\":\"\",\"partGroup\":\"\",\"childQuestions\":[]}],\"triggerName\":\"\"}]}";
           // //postData = "{\"lastName\":\"王\",\"firstName\":\"思\",\"email\":\"lavegf188@163.com\",\"appleId\":\"\",\"additionalDetails\":\"不好用\",\"solution\":\"GEN\",\"triggerDetails\":[{\"parentQuestions\":[{\"questionId\":\"Q60777-T600333\",\"questionText\":\"选取您所在国家/地区的 iTunes Store 或 App Store：\",\"answerId\":\"A20343\",\"answerText\":\"中国香港特别行政区\",\"repairType\":\" \",\"comptiaCode\":\"\",\"partGroup\":\"\",\"childQuestions\":[]},{\"questionId\":\"Q60778-T600333\",\"questionText\":\"项目标题：\",\"answerId\":\"\",\"answerText\":\"钱滚滚\",\"repairType\":\"\",\"comptiaCode\":\"\",\"partGroup\":\"\",\"childQuestions\":[]},{\"questionId\":\"Q60779-T600333\",\"questionText\":\"订单号：\",\"answerId\":\"\",\"answerText\":\"1235r\",\"repairType\":\"\",\"comptiaCode\":\"\",\"partGroup\":\"\",\"childQuestions\":[]}],\"triggerName\":\"\"}]}";

           // cookies.Add(new Uri("https://getsupport.apple.com"), new Cookie("POD", "cn~zh"));
           // CookieCollection cookCol = cookies.GetCookies(new Uri("https://getsupport.apple.com"));
           // CookieContainer exeCookies = new CookieContainer();
           // foreach (Cookie item in cookCol)
           // {
           //     Cookie cookie = new Cookie(item.Name, item.Value, item.Path, item.Domain);
           //     exeCookies.Add(cookie);
           // }
           // AppleAuto.Gethierarchy(cookies);
           // AppleAuto.Getmyproducts(token, cookies);
           // AppleAuto.Gettopics(token, postData, cookies);
           // AppleAuto.Gettriggers(token, postData, cookies);
           // AppleAuto.Geteligibility(token, postData, cookies);
           // cookies.Add(new Cookie("CAS", "zh~CN", "/", "getsupport.apple.com"));
           // AppleAuto.Getsolutions(token, postData, cookies);
           // string data = AppleAuto.GetEML( token, postData, cookies);

           // data = AppleAuto.Execute(postData, token, cookies);

        }
    }

    public enum ExecuteState
    {
        /// <summary>
        /// 执行成功
        /// </summary>
        Sucess,
        /// <summary>
        /// 出现异常
        /// </summary>
        Error
    }
}
