using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Sockets;
using System.Net;

namespace FinalRush
{
    class Multi
    {
        TcpClient client;
        string IP = "127.0.0.1";
        int port = 1490;
        int buffer_size = 2048;
        byte[] readBuffer;

        public Multi()
        {
            Global.Multi = this;
        }

        public void Initialize()
        {
            client = new TcpClient();
            client.NoDelay = true;
            client.Connect(IP, port);
            readBuffer = new byte[buffer_size];
            client.GetStream().BeginRead(readBuffer, 0, buffer_size, StreamReceived, null);
        }

        private void StreamReceived(IAsyncResult ar)
        {
            client.GetStream().BeginRead(readBuffer, 0, buffer_size, StreamReceived, null);
        }
    }
}
