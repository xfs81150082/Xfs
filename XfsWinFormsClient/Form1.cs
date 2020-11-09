using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xfs;
using XfsConsoleClient;

namespace XfsWinFormsClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText(XfsTimeHelper.CurrentTime() + " ... " + "\r\n");
            Thread.Sleep(1);

            //new XfsServerInit().Init();

            Thread.CurrentThread.Name = "TumoWorld";
            richTextBox1.AppendText(XfsTimeHelper.CurrentTime() + " ThreadName: " + Thread.CurrentThread.Name + "\r\n");
            richTextBox1.AppendText(XfsTimeHelper.CurrentTime() + " ThreadId: " + Thread.CurrentThread.ManagedThreadId + "\r\n");

            richTextBox1.AppendText(XfsTimeHelper.CurrentTime() + " 退出监听，并关闭程序。" + "\r\n");


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText(XfsTimeHelper.CurrentTime() + " ... " + "\r\n");
            Thread.Sleep(2000);

            new XfsClientInit().Init();
           
            Thread.CurrentThread.Name = "TumoWorld";
            richTextBox1.AppendText(XfsTimeHelper.CurrentTime() + " ThreadName:" + Thread.CurrentThread.Name + "\r\n");
            richTextBox1.AppendText(XfsTimeHelper.CurrentTime() + " ThreadId:" + Thread.CurrentThread.ManagedThreadId + "\r\n");

            richTextBox1.AppendText(XfsTimeHelper.CurrentTime() + " 退出联接，并关闭程序。" + "\r\n");



        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (XfsModelObjects.Tests.Count > 0)
            {
                string test = XfsModelObjects.Tests[0];
                richTextBox1.AppendText(test + "\r\n");
                richTextBox1.ScrollToCaret();
                XfsModelObjects.Tests.Remove(XfsModelObjects.Tests[0]);
            }
            else
            {
                string nt = XfsModelObjects.Tests.Count.ToString();
                richTextBox1.AppendText(nt + "\r\n");
                richTextBox1.ScrollToCaret();
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            DialogResult dr = openFileDialog1.ShowDialog();

            //获取打开文件的文件名
            string filename = openFileDialog1.FileName;

            if (dr == DialogResult.OK && !string.IsNullOrEmpty(filename))
            {
                richTextBox1.LoadFile(filename, RichTextBoxStreamType.PlainText);

                richTextBox1.Text += "\r\n";
            }

            richTextBox1.AppendText(richTextBox2.Text + "\r\n");
            richTextBox1.ScrollToCaret();


        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("我点击了按钮", "提示框", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (result == DialogResult.OK)
            {
                MessageBox.Show(result.ToString());
            }

            richTextBox1.AppendText(richTextBox1.Text + "\r\n");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string tt = richTextBox2.Text;
            XfsParameter parameter = XfsParameterTool.ToParameter(TenCode.Code0001, ElevenCode.Code0001, ElevenCode.Code0001.ToString(), tt);

            //XfsGame.XfsSence.GetComponent<XfsTcpClientNodeNet>().Send(parameter);
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }
    }
}
