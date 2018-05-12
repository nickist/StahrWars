using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class frmUniverse : Form
    {
        int boxCount = 16;
        UdpUser client = null;
        Pen gridPen = new Pen(System.Drawing.Color.White, 1);
        int gridSize = 0;
        Image universe = Image.FromFile("universe.jpg");


        #region ====================================================================================== UDP Class Setup

        // Structure
        public struct Received
        {
            public IPEndPoint Sender;
            public string Message;
        }

        // UDP Abstract class
        abstract class UdpBase
        {
            protected UdpClient Client;

            protected UdpBase()
            {
                Client = new UdpClient();
            }

            public async Task<Received> Receive()
            {
                var result = await Client.ReceiveAsync();
                return new Received()
                {
                    Message = Encoding.ASCII.GetString(result.Buffer, 0, result.Buffer.Length),
                    Sender = result.RemoteEndPoint
                };
            }
        }

        // Client
        class UdpUser : UdpBase
        {
            private UdpUser() { }

            public static UdpUser ConnectTo(string hostname, int port)
            {
                var connection = new UdpUser();
                connection.Client.Connect(hostname, port);
                return connection;
            }

            public void Send(string message)
            {
                var datagram = Encoding.ASCII.GetBytes(message);
                Client.Send(datagram, datagram.Length);
            }
        }

        #endregion

        public frmUniverse()
        {
            InitializeComponent();


            // set up the grid look - 1 dot, 4 spaces, 1 dot, 4 spaces
            gridPen.DashPattern = new float[] { 1, 4 };
            gridSize = panCanvas.Width / boxCount;



        }


        private void btnQuit_Click(object sender, EventArgs e) {
            throw new NotImplementedException();
        }




        private void panCanvas_Paint(object sender, PaintEventArgs e) {
             e.Graphics.DrawImage(universe, 0, 0);

            // Draw the grid
            for (int i = gridSize; i < panCanvas.Height; i += gridSize) {
                client.Send("hello world");
                e.Graphics.DrawLine(gridPen, 0, i, panCanvas.Width, i);
                e.Graphics.DrawLine(gridPen, i, 0, i, panCanvas.Height);
            }



        }


    }
}

