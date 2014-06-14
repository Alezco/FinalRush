using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Net.Sockets;
using System.IO;
using FinalRush.Game.Levels;

namespace FinalRush
{
    class GameMainMulti
    {
        // FIELDS

        public List<Wall> Walls;
        public List<Bonus> bonus;
        public List<Enemy> enemies;
        public List<Enemy2> enemies2;
        List<Bullets> playerBullets, player2Bullets;
        public List<VitesseBonus> speedbonus;
        Random random = new Random();
        MainMenu menu;
        Texture2D background = Resources.Environnment;
        Texture2D foreground = Resources.Foreground;
        public Bullets bullets;
        public static Editeur edit = new Editeur();
        public int[,] map = edit.Edition(7);
        public int size = 64;
        public string IP;

        // CONSTRUCTOR

        public GameMainMulti(string IP)
        {
            menu = new MainMenu(Global.Handler, 0f);
            this.IP = Global.MainMenu.ip;
            Walls = new List<Wall>();
            bonus = new List<Bonus>();
            enemies = new List<Enemy>();
            enemies2 = new List<Enemy2>();
            Global.GameMainMulti = this;
            player = new Player();
            player2 = new Player();
            readStream = new MemoryStream();
            writeStream = new MemoryStream();
            reader = new BinaryReader(readStream);
            writer = new BinaryWriter(writeStream);
            bullets = new Bullets(Resources.bullet);
            speedbonus = new List<VitesseBonus>();

            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];
                    if (number == 1)
                        Walls.Add(new Wall(x * size, y * size + size / 2, Resources.Herbe, size, size, Color.White));
                    if (number == 2)
                        Walls.Add(new Wall(x * size, y * size + size / 2, Resources.Ground, size, size, Color.White));
                    if (number == 3)
                        Walls.Add(new Wall(x * size, y * size + size / 2, Resources.Platform, 100, 16, Color.Gray));
                    if (number == 4)
                        speedbonus.Add(new VitesseBonus(x * size + 40, y * size + 74, Resources.Speed, 20, 20, Color.White));
                }
            }

        }

        #region Multi

        public TcpClient client;
        int port = 1490;
        int buffer_size = 2048;
        byte[] readBuffer;
        MemoryStream readStream, writeStream;
        BinaryReader reader;
        BinaryWriter writer;
        public Player player, player2;
        bool player2Connected;
        public Protocol p;

        public void Initialize(string IP)
        {
            client = new TcpClient();
            client.NoDelay = true;
            this.IP = IP;
            client.Connect(IP, port);
            readBuffer = new byte[buffer_size];
            client.GetStream().BeginRead(readBuffer, 0, buffer_size, StreamReceived, null);
            playerBullets = new List<Bullets>();
            player2Bullets = new List<Bullets>();
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

            try
            {
                p = (Protocol)reader.ReadByte();

                if (p == Protocol.Connected)
                {
                    byte id = reader.ReadByte();
                    string ip = reader.ReadString();
                    if (!player2Connected)
                    {
                        player2Connected = true;
                        player2.Hitbox = new Rectangle(player2.Hitbox.X, player2.Hitbox.Y, player2.Hitbox.Width, player2.Hitbox.Height);
                        player2.Marco = Resources.Marco;
                        player2.marco_color = Color.Red;
                        player2.bullets = new List<Bullets>();

                        writeStream.Position = 0;
                        writer.Write((byte)Protocol.Connected);
                        SendData(GetDataFromMemoryStream(writeStream));
                    }
                }
                else if (p == Protocol.Disconnected)
                {
                    byte id = reader.ReadByte();
                    string ip = reader.ReadString();
                    player2.bullets = null;
                    player2Connected = false;
                }
                else if (p == Protocol.PlayerMoved)
                {
                    float px = reader.ReadSingle();
                    float py = reader.ReadSingle();
                    byte id = reader.ReadByte();
                    string ip = reader.ReadString();
                    player2.Animate(1, 15, 2);

                    player2.Hitbox = new Rectangle(player2.Hitbox.X + (int)px, player2.Hitbox.Y + (int)py, player2.Hitbox.Width, player2.Hitbox.Height);
                }
                else if (p == Protocol.BulletCreated)
                {
                    float px = reader.ReadSingle();
                    float py = reader.ReadSingle();
                    byte id = reader.ReadByte();
                    string ip = reader.ReadString();
                    player2.Hitbox = new Rectangle(player2.Hitbox.X + (int)px, player2.Hitbox.Y + (int)py, player2.Hitbox.Width, player2.Hitbox.Height);
                }
            }
            catch (Exception)
            {

            }
        }

        private byte[] GetDataFromMemoryStream(MemoryStream ms)
        {
            byte[] result;

            lock (ms)
            {
                int bytesWritten = (int)ms.Position;
                result = new byte[bytesWritten];

                ms.Position = 0;
                ms.Read(result, 0, bytesWritten);
            }

            return result;
        }

        public void SendData(byte[] b)
        {
            try
            {
                lock (client.GetStream())
                {
                    client.GetStream().BeginWrite(b, 0, b.Length, null, null);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Client {0}:  {1}", IP, e.ToString());
            }
        }

        #endregion

        // UPDATE & DRAW

        public void Update(MouseState souris, KeyboardState clavier)
        {
            Vector2 iPosition = new Vector2(player.Hitbox.X + player.Hitbox.Width / 2, player.Hitbox.Y + player.Hitbox.Height / 2);
            Vector2 movement = Vector2.Zero;

            player.Update(souris, clavier, Walls, bonus);

            Vector2 nPosition = new Vector2(player.Hitbox.X + player.Hitbox.Width / 2, player.Hitbox.Y + player.Hitbox.Height / 2);
            Vector2 deltap = Vector2.Subtract(nPosition, iPosition);

            if (deltap != Vector2.Zero)
            {
                writeStream.Position = 0;
                writer.Write((byte)Protocol.PlayerMoved);
                writer.Write(deltap.X);
                writer.Write(deltap.Y);
                SendData(GetDataFromMemoryStream(writeStream));
            }

            GameTime gametime = new GameTime();

            menu.Update(gametime);
            for (int i = 0; i < enemies2.Count; i++)
            {
                for (int j = 0; j < Global.Player.bullets.Count; j++)
                    if (Global.Enemy2.Hitbox.Intersects(new Rectangle((int)Global.Player.bullets[j].position.X, (int)Global.Player.bullets[j].position.Y, 10, 10)))
                    {
                        enemies2.RemoveAt(i);
                        i--;
                    }
            }
            foreach (Enemy enemy in enemies)
                enemy.Update(Walls, random.Next(10, 1000));
            foreach (Enemy2 enemy2 in enemies2)
                enemy2.Update(Walls);

        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (player.Hitbox.X <= 400)
                spritebatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            else if (player.Hitbox.X >= 4200)
                spritebatch.Draw(background, new Rectangle(3800, 0, 800, 480), Color.White);
            else
                spritebatch.Draw(background, new Rectangle(player.Hitbox.X + player.Hitbox.Width / 2 - 400, 0, 800, 480), Color.White);
            for (int i = 0; i <= 2; i++)
                spritebatch.Draw(foreground, new Rectangle(1600 * i, 0, 1600, 480), Color.White);

            foreach (Enemy enemy in enemies)
                enemy.Draw(spritebatch);

            foreach (Enemy2 enemy2 in enemies2)
                enemy2.Draw(spritebatch);

            foreach (Wall wall in Walls)
                wall.Draw(spritebatch);

            foreach (Bonus b in bonus)
                b.Draw(spritebatch);

            foreach (VitesseBonus sb in speedbonus)
                sb.Draw(spritebatch);

            if (player != null)
            {
                player.Draw(spritebatch);
            }
            if (player2Connected)
            {
                player2.Draw(spritebatch);
            }
        }
    }
}