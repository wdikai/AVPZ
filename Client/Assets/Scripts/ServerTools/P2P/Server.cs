using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using LitJson;
using System.Collections.Generic;
using Assets.Scripts.ServerTools.Model;

namespace TCP.P2P
{
    public class Server
    {
        private IPAddress _localAddress;
        private TcpListener _listenSocket;
        private bool _running;
        private Thread _listenThread;
        private EventsController _eventControll;

        public Server(string ip, int port, EventsController eventer)
        {
            _eventControll = eventer;
            _localAddress = IPAddress.Parse(ip);
            _listenSocket = new TcpListener(_localAddress, port);
            _listenSocket.Start(20);

            _running = true;
            _listenThread = new Thread(new ThreadStart(ListenClients));
            _listenThread.Start();
        }

        private void ListenClients()
        {
            while (_running)
            {
                try
                {

                    TcpClient handler = _listenSocket.AcceptTcpClient();
                    Console.WriteLine("{0}:{1}",
                        ((IPEndPoint)handler.Client.RemoteEndPoint).Address.ToString(),
                        ((IPEndPoint)handler.Client.RemoteEndPoint).Port);
                    Console.WriteLine(((IPEndPoint)handler.Client.RemoteEndPoint).Port);
                    NetworkStream io = handler.GetStream();
                    string data = "";
                    byte[] bytes = new byte[1024];

                    data = Read(io);
                    Console.WriteLine("Massege:\n {0}", data);

                    ParseRequest(data);
                    //JsonData response = CreateResponse(request);
                    //byte[] msg = Encoding.UTF8.GetBytes(response.ToString());
                    //Write(io, "Сообщение доставленно!");

                    handler.Close();
                }
                catch(Exception)
                {
                
                }
            }
        }

        private void Write(NetworkStream networkStream, string massege)
        {
            if (networkStream.CanWrite)
            {

                byte[] myWriteBuffer = Encoding.UTF8.GetBytes(massege);
                networkStream.Write(myWriteBuffer, 0, myWriteBuffer.Length);
            }
            else
            {
                Console.WriteLine();
            }
        }

        private string Read(NetworkStream networkStream)
        {
            if (networkStream.CanRead)
            {
                byte[] myReadBuffer = new byte[1024];
                string data = "";
                int numberOfBytesRead = 0;
                do
                {
                    numberOfBytesRead = networkStream.Read(myReadBuffer, 0, myReadBuffer.Length);
                    data += Encoding.UTF8.GetString(myReadBuffer, 0, numberOfBytesRead); ;
                } while (networkStream.DataAvailable);
                return data;
            }
            else
            {
                Console.WriteLine();
                return "";
            }

        }

        private JsonData CreateResponse(JsonData request)
        {
            JsonData packet = new JsonData();
            packet["response"] = "Nothing";
            return packet;
        }

        public JsonData ParseRequest(string data)
        {
            JsonData packet = JsonMapper.ToJson(data);
            int Type = Int32.Parse(packet["type"].ToString());
            switch (Type)
            {

                case 0:
                    ParseInit(packet);
                    break;
                case 1:
                    _eventControll.IsMyStep = true;
                    break;
                case 2:
                    ParseMove(packet);
                    break;
                case 3:
                    ParseAttack(packet);
                    break;
                case 4:
                    _eventControll.Win = true;
                    break;
            }

            return packet;
        }

        public bool IsRunning()
        {
            return _listenThread.ThreadState.Equals(ThreadState.Running);
        }

        void ParseInit(JsonData _data)
        {
            string tp = _data["troopsPosition"].ToJson();
            List<Sold> troopsPosition = JsonMapper.ToObject<List<Sold>>(tp);
            _eventControll.InitEnemy(troopsPosition);

        }
        void ParseMove(JsonData _data)
        {
            int target = Int32.Parse(_data["turgetCellIndex"].ToString());
            int curent = Int32.Parse(_data["currentCellIndex"].ToString());
            _eventControll.Move(curent,target);
            _eventControll.IsMyStep = true;
        }
        void ParseAttack(JsonData _data)
        {
            int demage = Int32.Parse(_data["Damege"].ToString());
            int cellIndex = Int32.Parse(_data["turgetAttackCellIndex"].ToString());
            _eventControll.Atack(cellIndex,demage);
            _eventControll.IsMyStep = true;
        }
        
        public void Close()
        {
            _running = false;
        }
    }
}
