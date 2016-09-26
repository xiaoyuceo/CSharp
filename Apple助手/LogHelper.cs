using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apple
{
    public class LogHelper
    {
        public static RichTextBox log;

        public static void OutLog(Color color, string format, params object[] msg)
        {
            format = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": "+format;
            try
            {
                if (log.InvokeRequired)
                {
                    log.Invoke(new MethodInvoker(() =>
                    {
                        int len = 0;
                        if (log.Text != null)
                        {
                            len = log.Text.Length;
                        }
                        if (msg.Length == 0)
                        {
                            log.AppendText(format);
                        }
                        else
                        {
                            log.AppendText(string.Format(format, msg));
                        }
                        log.AppendText("\n");
                        log.Select(len, log.Text.Length - len);
                        log.SelectionColor = color;
                        log.Select(0, 0);

                    }));
                }
                else
                {
                    int len = 0;
                    if (log.Text != null)
                    {
                        len = log.Text.Length;
                    }
                    if (msg.Length == 0)
                    {
                        log.AppendText(format);
                    }
                    else
                    {
                        log.AppendText(string.Format(format, msg));
                    }
                    log.AppendText("\n");
                    log.Select(len, log.Text.Length - len);
                    log.SelectionColor = color;
                    log.Select(0, 0);
                }
            }
            catch (Exception e)
            {
            }
        }

        public static void OutLog(string format, params object[] msg)
        {
            try
            {
                format = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + format+"\n";
                if (log.InvokeRequired)
                {
                    log.Invoke(new MethodInvoker(() =>
                    {
                        if (msg.Length == 0)
                        {
                            log.AppendText(format);
                        }
                        else
                        {
                            log.AppendText(string.Format(format, msg));
                        }
                    }));
                }
                else
                {
                    if (msg.Length == 0)
                    {
                        log.AppendText(format);
                    }
                    else
                    {
                        log.AppendText(string.Format(format, msg));
                    }
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
