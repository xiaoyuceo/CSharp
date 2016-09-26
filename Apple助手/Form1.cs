using Newtonsoft.Json;
using NPOI.HSSF.Record;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Apple
{
    public partial class Form1 : Form
    {
        public MyProcessBar progressBar1 = new MyProcessBar();

        public Thread th = null;
        public Form1()
        {
            InitializeComponent();
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 389);
            this.progressBar1.Maximum = 100;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(784, 23);
            this.progressBar1.TabIndex = 3;
            this.progressBar1.Value = 0;
            this.Controls.Add(progressBar1);
        }

        private void selectInFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "Excel(*.xls;*.xlsx)|*.xls;*.xlsx";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = fileDialog.FileName;
                this.inFileTxt.Text = fileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.startBtn.Enabled = false;
            if (string.IsNullOrEmpty(this.inFileTxt.Text))
            {
                MessageBox.Show(this, "请先选择账号信息表格!", "警告");
                return;
            }
            AppleHelper.AccountDT = NPOIHelper.Import(this.inFileTxt.Text);
            AppleHelper.ContentDT = NPOIHelper.Import(this.inFileTxt.Text, 1);
            this.dataGridView1.DataSource = AppleHelper.AccountDT;

            if (AppleHelper.AccountDT == null || AppleHelper.ContentDT == null)
            {
                MessageBox.Show(this, "数据不存在或数据格式不正确，请检查数据文件!", "警告");
                return;
            }
            if (AppleHelper.AccountDT.Rows.Count == 0 || AppleHelper.ContentDT.Rows.Count == 0)
            {
                MessageBox.Show(this, "数据不存在或数据格式不正确，请检查数据文件!", "警告");
                return;
            }
            if (!string.IsNullOrEmpty(this.textBox1.Text))
            {
                this.startBtn.Enabled = true;
            }
            Const.ImportContent();
            LogHelper.OutLog("导入数据成功!");
            for (int i = 0; i < AppleHelper.AccountDT.Rows.Count; i++)
            {
                this.comboBox1.Items.Add(i+1);
                this.comboBox2.Items.Add(i+1);
            }

            this.comboBox1.SelectedIndex = 0;
            this.comboBox2.SelectedIndex = AppleHelper.AccountDT.Rows.Count - 1;
            endIndex = this.comboBox2.SelectedIndex;
        }
        int startIndex = 0;
        int endIndex = 0;
        private void startBtn_Click(object sender, EventArgs e)
        {
            startIndex = this.comboBox1.SelectedIndex;
            if (AppleHelper.IsRunning == true)
            {
                IsRunning = false;
                this.startBtn.Text = "开始执行";
                this.randCountryChk.Enabled = true;
                this.randContextChk.Enabled = true;
                this.comboBox1.Enabled = true;
                this.comboBox2.Enabled = true;
                this.comboBox3.Enabled = true;
                AppleHelper.IsRunning = false;
            }
            else
            {
                IsRunning = true;
                this.startBtn.Text = "停止";
                //this.startBtn.Enabled = false;
                this.randCountryChk.Enabled = false;
                this.randContextChk.Enabled = false;
                this.comboBox1.Enabled = false;
                this.comboBox2.Enabled = false;
                this.comboBox3.Enabled = false;
                th = new Thread(execute);
                th.Start();
            }
        }

        private void SetGridViewSelectIndex(int index)
        {
            if (this.dataGridView1.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    this.dataGridView1.Rows[index].Selected = true;
                }));
            }
            else
            {
                this.dataGridView1.Rows[index].Selected = true;
            }
        }
        private void SetFormText(string account, string email, string country, string issue)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    this.Text = string.Format("当前账号:{0},邮箱:{1}", account, email);
                }));
            }
            else
            {
                this.Text = string.Format("当前账号:{0},邮箱:{1}", account, email);
            }
        }
        private bool IsRunning = true;
        public void execute()
        {
            AppleHelper.Record = new PageData();
            for (int i = startIndex; i <= endIndex; i++)
            {
                if (IsRunning)
                {
                    try
                    {
                        AppleHelper.State = ExecuteState.Sucess;
                        SetGridViewSelectIndex(i);
                        Random delayRand = new Random(319);
                        AppleHelper.IsRunning = true;
                        string account = AppleHelper.AccountDT.Rows[i]["账号"] == null ? "" : AppleHelper.AccountDT.Rows[i]["账号"].ToString();
                        string pwd = AppleHelper.AccountDT.Rows[i]["密码"] == null ? "" : AppleHelper.AccountDT.Rows[i]["密码"].ToString();
                        string email = AppleHelper.AccountDT.Rows[i]["邮箱"] == null ? "" : AppleHelper.AccountDT.Rows[i]["邮箱"].ToString();
                        string orderNo = AppleHelper.AccountDT.Rows[i]["单号"] == null ? "" : AppleHelper.AccountDT.Rows[i]["单号"].ToString();
                        string title = AppleHelper.AccountDT.Rows[i]["游戏名"] == null ? "" : AppleHelper.AccountDT.Rows[i]["游戏名"].ToString();
                        string result = AppleHelper.AccountDT.Rows[i]["执行结果"] == null ? "" : AppleHelper.AccountDT.Rows[i]["执行结果"].ToString();
                        if (!string.IsNullOrEmpty(result) && result.IndexOf("失败") > -1)
                        {
                            continue;
                        }
                        string content = Const.GetContent();
                        LogHelper.OutLog(Color.Yellow, "当前账户信息:Apple:{0},Email:{1}", account, email);
                        LogHelper.OutLog(Color.Yellow, "当前表单信息:OrderNo:{0},ProjectTitle:{1},Content:{2}", orderNo, title, content);

                        string widgetKey = "";
                        string token = "";
                        string postData = "";
                        string retStr = "";

                        string firstName = "";
                        string lastName = "";

                        PageData issue = Const.GetIssue();
                        PageData country = Const.GetCountry();

                        LogHelper.OutLog(postData);
                        // return;
                        LoginModel loginModel = new LoginModel(account, pwd);
                        int k = 0;
                        do
                        {
                            k++;
                            CookieContainer cookie = AppleAuto.OpenWeb(out widgetKey);
                            //强制终止执行
                            if (AppleHelper.IsRunning == false)
                            {
                                break;
                            }
                            if (AppleHelper.State != ExecuteState.Sucess)
                            {
                                LogHelper.OutLog(Color.Red, "打开苹果网页失败！");
                                Recod(i, "打开苹果网页失败", false);
                                continue;
                            }
                            cookie.Add(new Uri("https://idmsa.apple.com"), new Cookie("dslang", "CH-ZN"));
                            cookie.Add(new Uri("https://idmsa.apple.com"), new Cookie("site", "CHN"));
                            // cookie = AppleAuto.OpenLoginPage(cookie, widgetKey);

                            postData = JsonConvert.SerializeObject(loginModel);
                            AppleAuto.OpenLoginPage(cookie, widgetKey);
                            //强制终止执行
                            if (AppleHelper.IsRunning == false)
                            {
                                break;
                            }
                            if (AppleHelper.State != ExecuteState.Sucess)
                            {
                                LogHelper.OutLog(Color.Red, "打开登录页面失败！");
                                Recod(i, "打开登录页面失败", false);
                                continue;
                            }
                            AppleAuto.Login(postData, widgetKey, cookie);
                            //强制终止执行
                            if (AppleHelper.IsRunning == false)
                            {
                                break;
                            }
                            if (AppleHelper.State != ExecuteState.Sucess)
                            {
                                LogHelper.OutLog(Color.Red, "登录失败！");
                                Recod(i, "登录失败", false);
                                continue;
                            }
                            Thread.Sleep(delayRand.Next(4000));
                            token = AppleAuto.GetToken(cookie);
                            //强制终止执行
                            if (AppleHelper.IsRunning == false)
                            {
                                break;
                            }
                            if (AppleHelper.State != ExecuteState.Sucess)
                            {
                                LogHelper.OutLog(Color.Red, "获取密钥失败！");
                                Recod(i, "获取密钥失败", false);
                                continue;
                            }
                            // cookie.Add(new Uri("https://getsupport.apple.com"), new Cookie("POD", "cn~zh"));
                            retStr = AppleAuto.Gethierarchy(cookie);
                            //强制终止执行
                            if (AppleHelper.IsRunning == false)
                            {
                                break;
                            }
                            if (AppleHelper.State != ExecuteState.Sucess)
                            {
                                LogHelper.OutLog(Color.Red, "获取产品列表失败！");
                                Recod(i, "获取产品列表失败", false);
                                continue;
                            }
                            retStr = AppleAuto.Getmyproducts(token, cookie);
                            //强制终止执行
                            if (AppleHelper.IsRunning == false)
                            {
                                break;
                            }
                            if (AppleHelper.State != ExecuteState.Sucess)
                            {
                                LogHelper.OutLog(Color.Red, "获取我的产品列表失败！");
                                Recod(i, "获取我的产品列表失败", false);
                                continue;
                            }
                            retStr = AppleAuto.Gettopics(token, cookie);
                            //强制终止执行
                            if (AppleHelper.IsRunning == false)
                            {
                                break;
                            }

                            if (AppleHelper.State != ExecuteState.Sucess)
                            {
                                LogHelper.OutLog(Color.Red, "获取帮助主题失败！");
                                Recod(i, "获取帮助主题失败", false);
                                continue;
                            }
                            postData = Const.GetTriggersData(issue);

                            LogHelper.OutLog(Color.Yellow, postData);
                            Thread.Sleep(delayRand.Next(4000));
                            retStr = AppleAuto.Gettriggers(token, postData, cookie);
                            //强制终止执行
                            if (AppleHelper.IsRunning == false)
                            {
                                break;
                            }
                            if (AppleHelper.State != ExecuteState.Sucess)
                            {
                                LogHelper.OutLog(Color.Red, "获取解决方案失败！");
                                Recod(i, "获取解决方案失败", false);
                                continue;
                            }
                            postData = Const.GetSolutionData(issue);

                            LogHelper.OutLog(Color.Yellow, postData);
                            Thread.Sleep(delayRand.Next(3000));
                            retStr = AppleAuto.Geteligibility(token, postData, cookie);
                            //强制终止执行
                            if (AppleHelper.IsRunning == false)
                            {
                                break;
                            }
                            if (AppleHelper.State != ExecuteState.Sucess)
                            {
                                LogHelper.OutLog(Color.Red, "选择解决方案失败！");
                                Recod(i, "选择解决方案失败", false);
                                continue;
                            }


                            cookie.Add(new Cookie("CAS", "zh~CN", "/", "getsupport.apple.com"));
                            // postData = Const.GetSolutionData(issue);
                            Thread.Sleep(delayRand.Next(5000));
                            LogHelper.OutLog(Color.Yellow, postData);
                            PageData user = AppleAuto.Getsolutions(token, postData, cookie);
                            //强制终止执行
                            if (AppleHelper.IsRunning == false)
                            {
                                break;
                            }
                            if (AppleHelper.State != ExecuteState.Sucess)
                            {
                                LogHelper.OutLog(Color.Red, "获取用户信息失败！");
                                Recod(i, "获取用户信息失败", false);
                                continue;
                            }

                            firstName = user.GetString("firstName");
                            lastName = user.GetString("lastName");
                            Thread.Sleep(delayRand.Next(10000));
                            postData = Const.GetEML(issue);

                            AppleAuto.GetEML(token, postData, cookie);
                            //强制终止执行
                            if (AppleHelper.IsRunning == false)
                            {
                                break;
                            }
                            if (AppleHelper.State != ExecuteState.Sucess)
                            {
                                LogHelper.OutLog(Color.Red, "获取问题表单失败！");
                                Recod(i, "获取问题表单失败", false);
                                continue;
                            }
                            postData = Const.GetExecuteData(country, firstName, lastName, account, email, title, orderNo, content);

                            Thread.Sleep(delayRand.Next(20000));
                            retStr = AppleAuto.Execute(token, postData, cookie);
                            if (AppleHelper.State != ExecuteState.Sucess)
                            {
                                LogHelper.OutLog(Color.Red, retStr);
                                Recod(i, retStr, false);
                                continue;
                            }
                            else
                            {
                                try
                                {
                                    PageData retPd = JsonConvert.DeserializeObject<PageData>(retStr);
                                    PageData data = JsonConvert.DeserializeObject<PageData>(retPd.GetString("data"));
                                    string caseId = data.GetString("caseId");
                                    string retTitle = data.GetString("caseTitle");
                                    PageData userEnteredDetails = JsonConvert.DeserializeObject<PageData>(data.GetString("userEnteredDetails"));
                                    string retEmail = userEnteredDetails.GetString("email");
                                    retStr = string.Format("案例编号:{0},支持主题:{1},邮箱:{2}", caseId, retTitle, retEmail);
                                }
                                catch (Exception e)
                                {

                                }
                                Recod(i, retStr, true);
                            }
                        } while (k <= AppleHelper.ReplayCount && AppleHelper.State != ExecuteState.Sucess);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.OutLog(Color.Red, ex.ToString());
                        Recod(i, ex.ToString(), false);
                    }
                }
                else
                {
                    LogHelper.OutLog(Color.Orange, "强制终止执行。。");
                }

            }
            if(IsRunning == false)
            {
                LogHelper.OutLog(Color.Orange, "强制终止执行。。");
            }
            try
            {
                FileStream file = new FileStream(this.inFileTxt.Text, FileMode.Open, FileAccess.Read);
                HSSFWorkbook workbook = new HSSFWorkbook(file);
                HSSFSheet ws = workbook.GetSheetAt(0) as HSSFSheet;
                foreach (KeyValuePair<string, object> item in AppleHelper.Record)
                {
                    int index = int.Parse(item.Key);
                    ICell cell = ws.GetRow(index).GetCell(5);
                    if (cell == null)
                    {
                        cell = ws.GetRow(index).CreateCell(5);
                    }
                    ICellStyle style = workbook.CreateCellStyle();
                    style.FillPattern = FillPatternType.SOLID_FOREGROUND;
                    style.BorderBottom = CellBorderType.THIN;
                    style.BorderLeft = CellBorderType.THIN;
                    style.BorderRight = CellBorderType.THIN;
                    style.BorderTop = CellBorderType.THIN;

                    if (!item.Value.ToString().Contains("成功"))
                    {
                        style.FillForegroundColor = HSSFColor.RED.index;
                    }
                    else
                    {
                        style.FillForegroundColor = HSSFColor.WHITE.index;
                    }
                    cell.CellStyle = style;
                    cell.SetCellValue(item.Value.ToString());

                }
                string fileName = this.textBox1.Text + "\\执行结果.xls";
                int s = 0;

                while (File.Exists(fileName))
                {
                    s++;
                    fileName = this.textBox1.Text + "\\执行结果(" + s + ").xls";
                }

                using (FileStream filess = File.Create(fileName))
                {
                    workbook.Write(filess);
                }
                file.Close();
                if (MessageBox.Show("执行结束!是否打开日志目录?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string path = this.textBox1.Text;
                    System.Diagnostics.Process.Start("explorer.exe", path);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "写入Excel失败!");
                return;
            }

            if (this.startBtn.InvokeRequired)
            {
                this.startBtn.Invoke(new MethodInvoker(() =>
                {
                    this.startBtn.Text = "开始执行";
                }));
            }
            else
            {
                this.startBtn.Text = "开始执行";
            }
        }
        private void SetDataGridViewStatus(int index, string text, Color backColor)
        {
            if (this.dataGridView1.InvokeRequired)
            {
                this.dataGridView1.Invoke(new MethodInvoker(() =>
                {
                    this.dataGridView1.Rows[index].Cells[5].Style.BackColor = backColor;
                    this.dataGridView1.Rows[index].Cells[5].Value = text;
                }));
            }
            else
            {
                this.dataGridView1.Rows[index].Cells[5].Style.BackColor = backColor;
                this.dataGridView1.Rows[index].Cells[5].Value = text;
            }
        }
        private void Recod(int index, string retStr, bool isOk = true)
        {
            if (isOk)
            {
                sr.WriteLine("{0}|{1}|{2}", index, "OK", retStr);
                SetDataGridViewStatus(index, "成功:" + retStr, Color.Green);
                AppleHelper.Record.Add(index + "", "成功:" + retStr);
            }
            else
            {
                sr.WriteLine("{0}|{1}|{2}", index, "Faild", retStr);
                SetDataGridViewStatus(index, "失败:" + retStr, Color.Red);
                AppleHelper.Record.Add(index + "", "失败:" + retStr);
            }
        }


        StreamWriter sr = File.AppendText("/log.dat");
        private void Form1_Load(object sender, EventArgs e)
        {
            this.comboBox3.SelectedIndex = 0;
            this.startBtn.Enabled = false;
            LogHelper.log = this.richTextBox1;
            AppleHelper.Init();
            sr.AutoFlush = true;

            //byte[] data = Properties.Resources.data;
            //string str = UTF8Encoding.UTF8.GetString(data);
            //this.richTextBox1.AppendText(str);
            //this.richTextBox1.AppendText("共:" + str.Split('\n').Length + "行");

            //Stream stream = new MemoryStream(data);
            //StreamReader sr = new StreamReader(stream);
            //while (!sr.EndOfStream)
            //{
            //    this.richTextBox1.AppendText(sr.ReadLine());
            //    this.richTextBox1.AppendText("\n");
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string account = AppleHelper.AccountDT.Rows[0]["账号"] == null ? "" : AppleHelper.AccountDT.Rows[0]["账号"].ToString();
            //string pwd = AppleHelper.AccountDT.Rows[0]["密码"] == null ? "" : AppleHelper.AccountDT.Rows[0]["密码"].ToString();
            //string email = AppleHelper.AccountDT.Rows[0]["邮箱"] == null ? "" : AppleHelper.AccountDT.Rows[0]["邮箱"].ToString();
            //string orderId = AppleHelper.AccountDT.Rows[0]["单号"] == null ? "" : AppleHelper.AccountDT.Rows[0]["单号"].ToString();
            //string title = AppleHelper.AccountDT.Rows[0]["游戏名"] == null ? "" : AppleHelper.AccountDT.Rows[0]["游戏名"].ToString();
            //string content = AppleHelper.ContentDT.Rows[0]["内容"].ToString();
            //LoginModel loginModel = new LoginModel(account, pwd);

            //MessageBox.Show(account);

            StreamWriter file = File.AppendText("data.json");
            file.AutoFlush = true;
            file.WriteLine(this.richTextBox1.Text);
            file.WriteLine("");
            file.Flush();
            file.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //DataTable dt = NPOIHelper.Import(this.inFileTxt.Text, 1);
            //this.dataGridView1.DataSource = dt;
            this.dataGridView1.Rows[0].Cells[0].Style.BackColor = Color.Red;
            this.dataGridView1.Rows[0].Cells[0].Value = "00000000000000";

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.textBox1.Text))
            {
                sr.Close();
                sr = File.AppendText(this.textBox1.Text + "\\Record.log");
                sr.AutoFlush = true;
                if (AppleHelper.AccountDT != null && AppleHelper.ContentDT != null)
                {
                    if (AppleHelper.AccountDT.Rows.Count > 0 && AppleHelper.ContentDT.Rows.Count > 0)
                    {
                        this.startBtn.Enabled = true;
                        return;
                    }
                }
            }
            this.startBtn.Enabled = false;
        }

        private void inFileTxt_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.textBox1.Text))
            {
                if (AppleHelper.AccountDT != null && AppleHelper.ContentDT != null)
                {
                    if (AppleHelper.AccountDT.Rows.Count > 0 && AppleHelper.ContentDT.Rows.Count > 0)
                    {
                        this.startBtn.Enabled = true;
                        return;
                    }
                }
            }
            this.startBtn.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string path = "";
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = false;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                path = folderBrowserDialog.SelectedPath;
                this.textBox1.Text = path;
            }

        }

        private void countryChk_CheckedChanged(object sender, EventArgs e)
        {
            AppleHelper.RandCountry = this.randContextChk.Checked;

        }

        private void randContextChk_CheckedChanged(object sender, EventArgs e)
        {
            AppleHelper.RandContent = this.randContextChk.Checked;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //判断是否已经导入数据
            for (int i = 0; i < 5; i++)
            {
                string appleId = this.dataGridView1.Rows[i].Cells[0].Value.ToString();
                string pwd = this.dataGridView1.Rows[i].Cells[1].Value.ToString();
                string email = this.dataGridView1.Rows[i].Cells[2].Value.ToString();
                string orderNo = this.dataGridView1.Rows[i].Cells[3].Value.ToString();
                string projectTitle = this.dataGridView1.Rows[i].Cells[4].Value == null ? "" : this.dataGridView1.Rows[i].Cells[4].Value.ToString();
                PageData country = Const.GetCountry();
                PageData issue = Const.GetIssue();
                string content = Const.GetContent();
                LogHelper.OutLog(Color.Red, country.ToString());
                LogHelper.OutLog(Color.Red, issue.ToString());
                LogHelper.OutLog("=====================TempTriggers================================");
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

                LogHelper.OutLog("=====================TempSolutions================================");

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
                LogHelper.OutLog(TempSolutions);

                LogHelper.OutLog("=====================TempEML================================");
                string TempEML = Const.TempEML;
                // LogHelper.OutLog(TempEML);
                foreach (KeyValuePair<string, object> item in Const.Topics)
                {
                    TempEML = TempEML.Replace("#{" + item.Key + "}", item.Value.ToString());
                }
                foreach (KeyValuePair<string, object> item in issue)
                {
                    TempEML = TempEML.Replace("#{" + item.Key + "}", item.Value.ToString());
                }
                LogHelper.OutLog(TempEML);
                LogHelper.OutLog("=====================TempExecute================================");
                string TempExecute = Const.TempExecute;
                // LogHelper.OutLog(TempExecute);
                TempExecute = TempExecute.Replace("#{appleId}", appleId);
                TempExecute = TempExecute.Replace("#{email}", email);
                TempExecute = TempExecute.Replace("#{projectTitle}", projectTitle);
                TempExecute = TempExecute.Replace("#{orderNo}", orderNo);
                TempExecute = TempExecute.Replace("#{additionalDetails}", content);
                foreach (KeyValuePair<string, object> item in country)
                {
                    TempExecute = TempExecute.Replace("#{" + item.Key + "}", item.Value.ToString());
                }
                this.dataGridView1.Rows[i].Cells[5].Style.BackColor = Color.Red;
                this.dataGridView1.Rows[i].Cells[5].Value = "失败";
                LogHelper.OutLog(TempExecute);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string m = DateTime.Now.ToShortTimeString();

            MessageBox.Show(m);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                sr.Flush();
                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "警告");
            }

            try
            {
                if (th!=null)
                {
                    th.Abort();
                }
            }
            catch (Exception )
            {
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            startIndex = this.comboBox1.SelectedIndex;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            endIndex = this.comboBox2.SelectedIndex;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            AppleHelper.ReplayCount = this.comboBox3.SelectedIndex;
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X,
            e.RowBounds.Location.Y,
            dataGridView1.RowHeadersWidth - 8,
            e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
            dataGridView1.RowHeadersDefaultCellStyle.Font,
            rectangle,
            dataGridView1.RowHeadersDefaultCellStyle.ForeColor,
            TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
        }
    }
}
