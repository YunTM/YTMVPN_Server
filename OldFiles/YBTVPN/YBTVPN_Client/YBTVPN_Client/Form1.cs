using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;


namespace YBTVPN_Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        //static Thread thd_Working = new Thread(WaitClient);

        private void Btn_Connect_Click(object sender, EventArgs e)
        {
            EndPoint remote = new IPEndPoint(IPAddress.Parse(txt_ip.Text), Convert.ToUInt16(txt_port.Text)); //用来保存发送方的ip和端口号
            byte[] buffer = new byte[Convert.ToUInt16(txt_buffer.Text)]; //!MTU这里需要修改

            for (int i = 0; i < Convert.ToUInt16(txt_buffer.Text); i++)
            {
                buffer[i] = (byte)'a';
            }

            //byte[] buffer = { 0x01, 0x97, 0x97, 0x97 };

            clientSocket.SendTo(buffer, remote);


        }






        
    }
}
