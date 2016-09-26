using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Apple
{
    public class Const
    {
        public static PageData Topics = new PageData();

        public static List<PageData> IssueLst = new List<PageData>();

        public static List<PageData> CountryLst = new List<PageData>();

        public static List<string> ContentLst = new List<string>();

        public static string TempTriggers = "";

        public static string TempSolutions = "";

        public static string TempEML = "";

        public static string TempExecute = "";

        public static void InitData()
        {
            Topics = new PageData();
            IssueLst = new List<PageData>();
            CountryLst = new List<PageData>();
            Topics.Add("superGroupId", "SG005");
            Topics.Add("prodGroupFamilyId", "PGF51002");
            Topics.Add("symptomCategoryId", "SC0060");
            Topics.Add("symptomCategoryDesc", "购买、账单与兑换");

            PageData issue1 = new PageData();
            issue1.Add("symptomId", "20031");
            issue1.Add("symptomDesc", "应用未按预期方式运行");

            PageData issue2= new PageData();
            issue2.Add("symptomId", "20250");
            issue2.Add("symptomDesc", "项目可以播放，但看起来或听起来有问题");

            PageData issue3 = new PageData();
            issue3.Add("symptomId", "21084");
            issue3.Add("symptomDesc", "未确认的费用");

            IssueLst.Add(issue1);
            IssueLst.Add(issue2);
            IssueLst.Add(issue3);

            PageData country1 = new PageData();
            country1.Add("countryId", "A40426");
            country1.Add("contryName", "中国");

            PageData country2 = new PageData();
            country2.Add("countryId", "A20343");
            country2.Add("contryName", "香港");

            CountryLst.Add(country1);
            CountryLst.Add(country2);

            GetResourceData();
        }

        private static int issueIndex =0;
        
        public static PageData GetIssue(bool rand=true)
        {

            if (rand == true)
            {
                Random rnd = new Random();
                issueIndex = rnd.Next(IssueLst.Count);
            }else 
            {
                issueIndex = issueIndex == (IssueLst.Count - 1) ? 0 : ++issueIndex;
            }
             return IssueLst[issueIndex];
         //   return IssueLst[2];
        }

        public static PageData GetCountry(bool rand = true)
        {
            int countryIndex = 0;
            if (rand == true)
            {
                Random rnd = new Random();
                countryIndex = rnd.Next(CountryLst.Count);
            }
            return CountryLst[countryIndex];
        }

        static int contentIndex = 0;

        public static string GetContent(bool rand =true)
        {
            if (rand == true)
            {
                Random rnd = new Random();
                contentIndex = rnd.Next(ContentLst.Count);
            }
            else
            {
                contentIndex = contentIndex == (ContentLst.Count - 1) ? 0 : ++contentIndex;
            }
            return ContentLst[contentIndex];
        }

        public static void ImportContent()
        {
            ContentLst = new List<string>();
            foreach (DataRow item in AppleHelper.ContentDT.Rows)
            {
                ContentLst.Add(item[0].ToString());
            }
        }

        public static string GetTriggersData(PageData issue)
        {
            string TempTriggers = Const.TempTriggers;
            // LogHelper.OutLog(TempTriggers);
            foreach (KeyValuePair<string, object> item in Const.Topics)
            {
                TempTriggers = TempTriggers.Replace("#{" + item.Key + "}", item.Value.ToString());
            }
            foreach (KeyValuePair<string, object> item in issue)
            {
                TempTriggers = TempTriggers.Replace("#{" + item.Key + "}", item.Value.ToString());
            }
            LogHelper.OutLog(TempTriggers);
            return TempTriggers;
        }

        public static string GetSolutionData(PageData issue)
        {
            string TempSolutions = Const.TempSolutions;
            // LogHelper.OutLog(TempSolutions);
            foreach (KeyValuePair<string, object> item in Const.Topics)
            {
                TempSolutions = TempSolutions.Replace("#{" + item.Key + "}", item.Value.ToString());
            }
            foreach (KeyValuePair<string, object> item in issue)
            {
                TempSolutions = TempSolutions.Replace("#{" + item.Key + "}", item.Value.ToString());
            }
            return TempSolutions;
        }

        public static string GetExecuteData(PageData country,string firstName,string lastName,string appleId,string email,string projectTitle,string orderNo, string content )
        {
            string TempExecute = Const.TempExecute;
            TempExecute = TempExecute.Replace("#{firstName}", firstName);
            TempExecute = TempExecute.Replace("#{lastName}", lastName);
            TempExecute = TempExecute.Replace("#{appleId}", appleId);
            TempExecute = TempExecute.Replace("#{email}", email);
            TempExecute = TempExecute.Replace("#{projectTitle}", projectTitle);
            TempExecute = TempExecute.Replace("#{orderNo}", orderNo);
            TempExecute = TempExecute.Replace("#{additionalDetails}", content);
            foreach (KeyValuePair<string, object> item in country)
            {
                TempExecute = TempExecute.Replace("#{" + item.Key + "}", item.Value.ToString());
            }
            return TempExecute;
        }

        public static string GetEML(PageData issue)
        {
            string TempEML = Const.TempEML;
            foreach (KeyValuePair<string, object> item in issue)
            {
                TempEML = TempEML.Replace("#{" + item.Key + "}", item.Value.ToString());
            }
            return TempEML;
        }
        static void GetResourceData()
        {
            byte[] data = Properties.Resources.data;
            string str = UTF8Encoding.UTF8.GetString(data);
            string[] dataArr = str.Split('\n');
            TempTriggers = dataArr[1];
            TempSolutions = dataArr[3];
            TempEML = dataArr[5];
            TempExecute = dataArr[7];
        }
    }
}
