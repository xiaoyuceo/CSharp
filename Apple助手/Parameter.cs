
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apple
{
   public class Parameter
    {

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="additionalDetails"></param>
        /// <param name="projectTitle"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static string GetParameter(string appleId, string email, string firstName,string lastName,string answerId, string country,string additionalDetails,string projectTitle,string orderId)
        {
            
            PageData pd = new PageData();
            pd.Add("lastName", lastName);
            pd.Add("firstName", firstName);
            pd.Add("email", email);
            pd.Add("appleId", appleId);
            pd.Add("additionalDetails", additionalDetails);
            pd.Add("solution", "GEN");
            List<object> triggerDetails = new List<object>();
            Question q1 = new Question();
            q1.questionId = "Q60777-T600333";
            q1.questionText = "选取您所在国家/地区的 iTunes Store 或 App Store：";
            q1.answerId = answerId;
            q1.answerText = country;
            q1.repairType = " ";
            q1.comptiaCode = "";
            q1.partGroup = "";
            q1.childQuestions = new List<string>();

            Question q2 = new Question();
            q2.questionId = "Q60778-T600333";
            q2.questionText = "项目标题：";
            q2.answerId = "";
            q2.answerText = projectTitle;
            q2.repairType = "";
            q2.comptiaCode = "";
            q2.partGroup = "";
            q2.childQuestions = new List<string>();

            Question q3 = new Question();
            q3.questionId = "Q60779-T600333";
            q3.questionText = "订单号：";
            q3.answerId = "";
            q3.answerText = orderId;
            q3.repairType = "";
            q3.comptiaCode = "";
            q3.partGroup = "";
            q3.childQuestions = new List<string>();
            List<object> parentQuestions = new List<object>();
            parentQuestions.Add(q1);
            parentQuestions.Add(q2);
            parentQuestions.Add(q3);
            PageData parentQuestionsPd = new PageData();
            parentQuestionsPd.Add("parentQuestions", parentQuestions);
            parentQuestionsPd.Add("triggerName", "");
            triggerDetails.Add(parentQuestionsPd);
            pd.Add("triggerDetails", triggerDetails);
            string data = JsonConvert.SerializeObject(pd);
            return data;
        }
    }
}
