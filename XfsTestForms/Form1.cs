using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XfsServer;

namespace XfsTestForms
{
    public partial class Form1 : Form
    {
        public Dictionary<string, XfsServerSocket> Servers = new Dictionary<string, XfsServerSocket>();

        public Form1()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        XfsServerSocket gateServer = null;

        private void button1_Click(object sender, EventArgs e)
        {
            gateServer = new XfsServerSocket();
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
        }

        private void button9_Click(object sender, EventArgs e)
        {

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

        }
    }
}
