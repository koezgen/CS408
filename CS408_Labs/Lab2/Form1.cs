using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form1 : Form
    {

        bool terminating = false;
        bool connected = false;
        int token = 0;
        Socket clientSocket;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP = textBox_ip.Text;
            int portNum;

            if (Int32.TryParse(textBox_port.Text, out portNum))
            {
                try
                {
                    clientSocket.Connect(IP, portNum);
                    button1.Enabled = false;
                    textBox_message.Enabled = true;
                    connected = true;
                    logs.AppendText("Connected to the server!\n");

                    Thread receiveThread = new Thread(Receive);
                    receiveThread.Start();
                }
                catch
                {
                    logs.AppendText("Could not connect to the server!\n");
                }
            }
            else
            {
                logs.AppendText("Check the port\n");
            }

        }

        private void Receive()
        {
            while (connected)
            {
                try
                {
                    Byte[] buffer = new Byte[64];
                    clientSocket.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

                    logs.AppendText(incomingMessage + "\n");

                    if (token != 0)
                    {
                        logs.AppendText("The server has disconnected\n");
                        clientSocket.Close();
                        connected = false;
                        textBox_message.Enabled = false;
                    }

                    int messageLength = incomingMessage.Length;

                    int serverToken;

                    if (Int32.TryParse(incomingMessage, out serverToken))
                    {
                        string message = textBox_message.Text;
                        int ascii = 0;

                        foreach (char ch in message)
                        {
                            if (ch != ' ')
                            {
                                ascii += (int)ch;
                            }
                        }

                        token = ascii * serverToken;
                        string tok = token.ToString();

                        logs.AppendText("Server Token : " + incomingMessage + "\n");

                        message = tok + " " + textBox_message.Text;

                        if (message != "" && message.Length <= 64)
                        {
                            Byte[] buf = Encoding.Default.GetBytes(message);
                            clientSocket.Send(buf);
                            logs.AppendText("Message Sent : " + message + "\n");
                        }
                    }
                }

                catch
                {
                    if (!terminating)
                    {
                        logs.AppendText("The server has disconnected\n");
                        textBox_message.Enabled = false;
                    }

                    clientSocket.Close();
                    connected = false;
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
