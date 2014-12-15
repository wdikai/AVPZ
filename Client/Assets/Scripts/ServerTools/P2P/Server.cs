using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using LitJson;

namespace TCP.P2P
{
    class Server
    {
        private IPAddress _localAddress;
        private TcpListener _listenSocket;
        private bool _running;
        private Thread _listenThread;

        public Server(string ip, int port)
        {
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
                TcpClient handler = _listenSocket.AcceptTcpClient();
                Console.WriteLine("{0}:{1}",
                    ((IPEndPoint)handler.Client.RemoteEndPoint).Address.ToString(), 
                    ((IPEndPoint)handler.Client.RemoteEndPoint).Port);
                Console.WriteLine(((IPEndPoint)handler.Client.RemoteEndPoint).Port);
                NetworkStream io = handler.GetStream();
                string data = "";
                byte[] bytes = new byte[1024];

                data = Read( io);
                Console.WriteLine("Massege:\n {0}", data);

                //JsonData request = ParseRequest(data);
                //JsonData response = CreateResponse(request);
                //byte[] msg = Encoding.UTF8.GetBytes(response.ToString());
                Write(io, "Сообщение доставленно!");

                handler.Close();
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
                do{
                    numberOfBytesRead = networkStream.Read(myReadBuffer, 0, myReadBuffer.Length);
                    data += Encoding.UTF8.GetString(myReadBuffer, 0, numberOfBytesRead); ;			
                } while (networkStream.DataAvailable);
                return data;
            }
            else{
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
            return packet;
        }

        public bool IsRunning()
        {
            return _listenThread.ThreadState.Equals(ThreadState.Running);
        }

        public void Close()
        {
            _running = false;
        }
    }
}
