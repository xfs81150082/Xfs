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
using XfsClient;
using XfsServer;

namespace XfsTestForms
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
            richTextBox1.AppendText(XfsTimerTool.CurrentTime() + " ... " + "\r\n");
            Thread.Sleep(1);

            new XfsServerInit().Init();
       
            Thread.CurrentThread.Name = "TumoWorld";
            richTextBox1.AppendText(XfsTimerTool.CurrentTime() + " ThreadName: " + Thread.CurrentThread.Name + "\r\n");
            richTextBox1.AppendText(XfsTimerTool.CurrentTime() + " ThreadId: " + Thread.CurrentThread.ManagedThreadId + "\r\n");

            richTextBox1.AppendText(XfsTimerTool.CurrentTime() + " 退出监听，并关闭程序。"+"\r\n");

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

        }

        private void button6_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += 1;
            richTextBox1.Text += "\r\n";
            richTextBox1.Text += 2;
            richTextBox1.Text += "\r";
            richTextBox1.Text += 3;
            richTextBox1.Text += "\r\n";

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

            richTextBox1.AppendText(XfsTimerTool.CurrentTime() + " ... " + "\r\n");
            Thread.Sleep(2000);

            new XfsClientInit().Init();

            /////服务器加载组件
            //XfsGame.XfsSence.AddComponent(new XfsNode(XfsNodeType.Client));                        ///服务器加载组件 : 服务器类型组件
            //XfsGame.XfsSence.AddComponent(new XfsTcpClient());                                     ///服务器加载组件 : 通信组件Server
            //XfsGame.XfsSence.AddComponent(new XfsControllers());                                     ///服务器加载组件 : 通信组件Server

            /////服务器加载组件驱动程序
            //XfsGame.XfsSence.AddComponent(new XfsTcpClientSystem());        ///服务器加载组件 : 套接字 外网 传输数据组件

            ////XfsGame.XfsSence.AddComponent(new XfsTest());                 ///客户端加载组件 : 测试组件1


            Thread.CurrentThread.Name = "TumoWorld";
            richTextBox1.AppendText(XfsTimerTool.CurrentTime() + " ThreadName:" + Thread.CurrentThread.Name + "\r\n");
            richTextBox1.AppendText(XfsTimerTool.CurrentTime() + " ThreadId:" + Thread.CurrentThread.ManagedThreadId + "\r\n");

            richTextBox1.AppendText(XfsTimerTool.CurrentTime() + " 退出联接，并关闭程序。" + "\r\n");



        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
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

        private void button17_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText(richTextBox2.Text + "\r\n");
            richTextBox1.ScrollToCaret();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            string tt = richTextBox2.Text;
            XfsParameter parameter = XfsParameterTool.ToParameter(TenCode.Code0001, ElevenCode.Code0001, ElevenCode.Code0001.ToString(),tt);

            XfsGame.XfsSence.GetComponent<XfsTcpClient>().Send(parameter);
        }
    }
}
