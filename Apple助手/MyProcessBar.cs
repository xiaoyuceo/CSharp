using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Apple
{
    [ToolboxItem(true)]
    public class MyProcessBar : System.Windows.Forms.ProgressBar
    {


        [System.Runtime.InteropServices.DllImport("user32.dll ")]

        static extern IntPtr GetWindowDC(IntPtr hWnd);

        [System.Runtime.InteropServices.DllImport("user32.dll ")]

        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        private System.Drawing.Color _TextColor = System.Drawing.Color.Black;

        private System.Drawing.Font _TextFont = new System.Drawing.Font("SimSun ", 12);

        public System.Drawing.Color TextColor

        {

            get { return _TextColor; }

            set { _TextColor = value; this.Invalidate(); }

        }

        public System.Drawing.Font TextFont

        {

            get { return _TextFont; }

            set { _TextFont = value; this.Invalidate(); }

        }

        protected override void WndProc(ref Message m)

        {

            base.WndProc(ref m);

            if (m.Msg == 0xf || m.Msg == 0x133)

            {

                IntPtr hDC = GetWindowDC(m.HWnd);

                if (hDC.ToInt32() == 0)

                {

                    return;

                }
                              System.Drawing.Graphics g = Graphics.FromHdc(hDC);
                SolidBrush brush = new SolidBrush(_TextColor);
                string s = string.Format("{0}%", this.Value * 100 / this.Maximum);

                SizeF size = g.MeasureString(s, _TextFont);

                float x = (this.Width - size.Width) / 2;

                float y = (this.Height - size.Height) / 2;

                g.DrawString(s, _TextFont, brush, x, y);
                m.Result = IntPtr.Zero;
                ReleaseDC(m.HWnd, hDC);
            }
        }
    }
}
