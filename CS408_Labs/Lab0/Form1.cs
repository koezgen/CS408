using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText(textBox1.Text);
            richTextBox1.AppendText("\n");
            textBox1.Text = "";
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (textBoxID.Text == "admin" && maskedTextBox1.Text == "admin")
            {
                buttonConnect.Enabled = false;
                buttonDisconnect.Enabled = true;
                richTextBox1.AppendText("Logged in as Admin @" + DateTime.Now + "\n");
                buttonSend.Enabled = true;
                textBox1.Enabled = true;
                textBoxID.Text = "";
                maskedTextBox1.Text = "";
            }

            else
            {
                richTextBox1.AppendText("Incorrect credentials. System admin has been notified." + "\n");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxID_TextChanged(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected_1(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            if (buttonDisconnect.Enabled == true)
            {
                buttonDisconnect.Enabled = false;
                textBox1.Enabled= false;
                buttonSend.Enabled= false;
                buttonConnect.Enabled= true;
                richTextBox1.AppendText("Logged off as Admin @" + DateTime.Now + "\n");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
