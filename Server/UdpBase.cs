using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace UPDServer {
    abstract class UdpBase {
        protected UdpClient Client;

        protected UdpBase() {
            Client = new UdpClient();
        }

        public async Task<Received> Receive() {
            var result = await Client.ReceiveAsync();
            return new Received() {
                Message = Encoding.ASCII.GetString(result.Buffer, 0, result.Buffer.Length),
                Sender = result.RemoteEndPoint
            };
        }
    }
}
