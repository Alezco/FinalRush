using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Sockets;
using System.Net;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace FinalRush
{
    class Multi
    {
        TcpClient client;
        string IP = "127.0.0.1";
        int port = 1490;
        int buffer_size = 2048;
        byte[] readBuffer;
        MemoryStream readStream;
        BinaryReader reader;
        public Player player, player2;

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
            readStream = new MemoryStream();
            reader = new BinaryReader(readStream);
            player = new Player();
        }

        private void StreamReceived(IAsyncResult ar)
        {
            int bytesRead = 0;

            try
            {
                lock (client.GetStream())
                    bytesRead = client.GetStream().EndRead(ar);
            }
            catch (Exception) { }

            if (bytesRead == 0)
            {
                client.Close();
                return;
            }

            byte[] data = new byte[bytesRead];

            for (int i = 0; i < bytesRead; i++)
                data[i] = readBuffer[i];

            ProcessData(data);

            client.GetStream().BeginRead(readBuffer, 0, buffer_size, StreamReceived, null);
        }

        public void ProcessData(byte[] data)
        {
            readStream.SetLength(0);
            readStream.Position = 0;

            readStream.Write(data, 0, data.Length);
            readStream.Position = 0;

            Protocol p;

            try
            {
                p = (Protocol)reader.ReadByte();

                if (p == Protocol.Connected)
                {
                    byte id = reader.ReadByte();
                    string ip = reader.ReadString();
                    Console.WriteLine(String.Format("Player has connected : {0} IP adress : {1}", id, ip));
                }
                else if (p == Protocol.Disconnected)
                {
                    byte id = reader.ReadByte();
                    string ip = reader.ReadString();
                    Console.WriteLine(String.Format("Player has disconnected : {0} IP adress : {1}", id, ip));
                }
            }
            catch (Exception)
            {

            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();
            if (player != null)
            {
                player.Draw(sb);
            }
            sb.End();
        }
    }
}
