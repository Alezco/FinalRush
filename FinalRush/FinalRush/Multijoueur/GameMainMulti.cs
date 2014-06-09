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

namespace FinalRush
{
    class GameMainMulti
    {
        // FIELDS

        //public Multi multi;
        public List<Wall> Walls;
        public List<Bonus> bonus;
        public List<Enemy> enemies;
        public List<Enemy2> enemies2;
        List<Bullets> playerBullets, player2Bullets;
        Random random = new Random();
        MainMenu menu;
        Texture2D background = Resources.Environnment;
        Texture2D foreground = Resources.Foreground;
        public Bullets bullets;

        // CONSTRUCTOR

        public GameMainMulti()
        {
            menu = new MainMenu(Global.Handler, 0f);
            Walls = new List<Wall>();
            bonus = new List<Bonus>();
            enemies = new List<Enemy>();
            enemies2 = new List<Enemy2>();
            //multi = Global.Multi;
            Global.GameMainMulti = this;
            player = new Player();
            player2 = new Player();
            readStream = new MemoryStream();
            writeStream = new MemoryStream();
            reader = new BinaryReader(readStream);
            writer = new BinaryWriter(writeStream);
            bullets = new Bullets(Resources.bullet);

            #region Ennemis
            //Ennemis

            enemies.Add(new Enemy(1100, 350, Resources.Zombie));
            enemies.Add(new Enemy(1150, 350, Resources.Zombie));
            enemies.Add(new Enemy(1180, 350, Resources.Zombie));
            enemies.Add(new Enemy(2550, 250, Resources.Zombie));
            enemies.Add(new Enemy(2650, 250, Resources.Zombie));

            enemies2.Add(new Enemy2(2400, 376, Resources.Zombie));

            #endregion

            #region Plateformes
            //Plateformes

            Walls.Add(new Wall(425, 245, Resources.Platform, 50, 16, Color.Green));
            Walls.Add(new Wall(130, 360, Resources.Platform, 92, 16, Color.IndianRed));
            Walls.Add(new Wall(280, 300, Resources.Platform, 92, 16, Color.IndianRed));
            Walls.Add(new Wall(680, 365, Resources.Platform, 92, 16, Color.IndianRed));
            Walls.Add(new Wall(520, 300, Resources.Platform, 92, 16, Color.IndianRed));
            Walls.Add(new Wall(1160, 200, Resources.Platform, 92, 16, Color.IndianRed));
            Walls.Add(new Wall(1266, 415, Resources.Platform, 1, 1, Color.IndianRed));
            Walls.Add(new Wall(1664, 350, Resources.Platform, 92, 16, Color.IndianRed));
            Walls.Add(new Wall(1828, 280, Resources.Platform, 92, 16, Color.IndianRed));
            Walls.Add(new Wall(1828, 140, Resources.Platform, 92, 16, Color.IndianRed));
            Walls.Add(new Wall(1664, 210, Resources.Platform, 92, 16, Color.IndianRed));
            Walls.Add(new Wall(1984, 140, Resources.Platform, 92, 16, Color.IndianRed));
            Walls.Add(new Wall(2100, 240, Resources.Platform, 92, 16, Color.IndianRed));
            Walls.Add(new Wall(2200, 340, Resources.Platform, 92, 16, Color.IndianRed));
            Walls.Add(new Wall(1984, 340, Resources.Platform, 50, 16, Color.IndianRed));
            Walls.Add(new Wall(2500, 340, Resources.Platform, 200, 16, Color.IndianRed));
            Walls.Add(new Wall(3520, 350, Resources.Platform, 92, 16, Color.IndianRed));
            Walls.Add(new Wall(3620, 300, Resources.Platform, 40, 16, Color.IndianRed));
            Walls.Add(new Wall(3680, 250, Resources.Platform, 20, 16, Color.IndianRed));
            Walls.Add(new Wall(3720, 200, Resources.Platform, 20, 16, Color.IndianRed));
            Walls.Add(new Wall(3860, 400, Resources.Platform, 92, 16, Color.IndianRed));
            Walls.Add(new Wall(4000, 340, Resources.Platform, 92, 16, Color.IndianRed));

            #endregion

            #region Terrain
            //Colonnes et sol

            Walls.Add(new Wall(896, 416, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(896, 352, Resources.Herbe, 64, 64, Color.White));
            Walls.Add(new Wall(960, 416, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(960, 352, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(960, 288, Resources.Herbe, 64, 64, Color.White));
            Walls.Add(new Wall(1024, 416, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(1024, 352, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(1024, 288, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(1024, 224, Resources.Herbe, 64, 64, Color.White));
            Walls.Add(new Wall(1600, 0, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(1600, 64, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(1600, 128, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(1600, 192, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(1600, 256, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(1600, 320, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(1920, 416, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(1920, 352, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(1920, 288, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(1920, 224, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(1920, 160, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(1920, 96, Resources.Herbe, 64, 64, Color.White));
            Walls.Add(new Wall(2816, 416, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(2816, 352, Resources.Herbe, 64, 64, Color.White));
            Walls.Add(new Wall(2944, 416, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(2944, 352, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(2944, 288, Resources.Herbe, 64, 64, Color.White));
            Walls.Add(new Wall(3072, 416, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(3072, 352, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(3072, 288, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(3072, 224, Resources.Herbe, 64, 64, Color.White));
            Walls.Add(new Wall(3200, 416, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(3200, 352, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(3200, 288, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(3200, 224, Resources.Ground, 64, 64, Color.White));
            Walls.Add(new Wall(3200, 160, Resources.Herbe, 64, 64, Color.White));



            //Sol

            for (int i = 0; i < 80; i++)
                if (i != 2 & i != 3 & i != 4 & i != 5 & i != 6 & i != 7 & i != 8 & i != 9
                  & i != 14 & i != 15 & i != 16 & i != 20 & i != 21 & i != 30
                  & i != 44 & i != 45 & i != 46 & i != 47 & i != 48 & i != 49 & i != 50 & i != 51
                  & i != 55 & i != 56 & i != 57 & i != 58 & i != 59 & i != 60 & i != 61 & i != 62)
                    Walls.Add(new Wall(64 * i, 416, Resources.Herbe, 64, 64, Color.White));
            #endregion

            #region Bonus
            //Bonus

            bonus.Add(new Bonus(700, 345, Resources.Coin, 20, 20, Color.White));
            bonus.Add(new Bonus(1200, 100, Resources.Coin, 20, 20, Color.White));
            bonus.Add(new Bonus(1940, 76, Resources.Coin, 20, 20, Color.White));
            bonus.Add(new Bonus(1984, 320, Resources.Coin, 20, 20, Color.White));
            bonus.Add(new Bonus(1984, 396, Resources.Coin, 20, 20, Color.White));
            bonus.Add(new Bonus(2520, 320, Resources.Coin, 20, 20, Color.White));
            bonus.Add(new Bonus(2650, 320, Resources.Coin, 20, 20, Color.White));
            bonus.Add(new Bonus(2816, 332, Resources.Coin, 20, 20, Color.White));
            bonus.Add(new Bonus(3200, 140, Resources.Coin, 20, 20, Color.White));
            bonus.Add(new Bonus(4020, 320, Resources.Coin, 20, 20, Color.White));
            #endregion
        }

        #region Multi

        TcpClient client;
        string IP = "127.0.0.1";
        int port = 1490;
        int buffer_size = 2048;
        byte[] readBuffer;
        MemoryStream readStream, writeStream;
        BinaryReader reader;
        BinaryWriter writer;
        public Player player, player2;
        bool player2Connected;



        public void Initialize()
        {
            client = new TcpClient();
            client.NoDelay = true;
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

            Protocol p;

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

                        writeStream.Position = 0;
                        writer.Write((byte)Protocol.Connected);
                        SendData(GetDataFromMemoryStream(writeStream));
                    }
                }
                else if (p == Protocol.Disconnected)
                {
                    byte id = reader.ReadByte();
                    string ip = reader.ReadString();
                    player2Connected = false;
                }
                else if (p == Protocol.PlayerMoved)
                {
                    float px = reader.ReadSingle();
                    float py = reader.ReadSingle();
                    byte id = reader.ReadByte();
                    string ip = reader.ReadString();
                    //player2.Animate(1, 15, 2);

                    player2.Hitbox = new Rectangle(player2.Hitbox.X + (int)px, player2.Hitbox.Y + (int)py, player2.Hitbox.Width, player2.Hitbox.Height);
                }
                //else if (p == Protocol.BulletCreated)
                //{
                //    float px = reader.ReadSingle();
                //    float py = reader.ReadSingle();
                //    byte id = reader.ReadByte();
                //    string ip = reader.ReadString();
                //    player2.Hitbox = new Rectangle(player2.Hitbox.X + (int)px, player2.Hitbox.Y + (int)py, player2.Hitbox.Width, player2.Hitbox.Height);
                //}
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
            //if (deltap != Vector2.Zero)
            //{
            //    writeStream.Position = 0;
            //    writer.Write((byte)Protocol.BulletCreated);
            //    SendData(GetDataFromMemoryStream(writeStream));
            //}

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
            //LocalPlayer.Draw(spritebatch);

            foreach (Enemy enemy in enemies)
                enemy.Draw(spritebatch);

            foreach (Enemy2 enemy2 in enemies2)
                enemy2.Draw(spritebatch);

            foreach (Wall wall in Walls)
                wall.Draw(spritebatch);

            foreach (Bonus b in bonus)
                b.Draw(spritebatch);

            if (player != null)
            {
                player.Draw(spritebatch);
                foreach (Bullets b in playerBullets)
                    b.Draw(spritebatch);
            }
            if (player2Connected)
            {
                player2.Draw(spritebatch);
                bullets.Draw(spritebatch);
            }
        }
    }
}