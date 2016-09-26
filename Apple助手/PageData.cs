using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Apple
{
   public class PageData:Dictionary<string,object>
    {
        public string GetString(string key)
        {
            if (!this.ContainsKey(key))
            {
                return "";
            }
           object obj = this[key];
            if (obj == null)
            {
                return "";
            }
            return obj.ToString();
        }

        public new void Add(string key, object value)
        {
            if (this.ContainsKey(key))
            {
                this[key] = value;
            }
            else
            {
                base.Add(key, value);
            }
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }   
    }

    public class Question
    {
        public string questionId { get; set; }
        public string questionText { get; set; }
        public string answerId { get; set; }
        public string answerText { get; set; }
        public string repairType { get; set; }
        public string comptiaCode { get; set; }
        public string partGroup { get; set; }
        public List<string> childQuestions { get; set; }
        
    }
}
