using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace TCP.P2P
{
    public class Client
    {
        

        private IPAddress _serverAddress;
        private int _port;
        private TcpClient _sender;

        public Client(string ip, int port)
        {
            _serverAddress = IPAddress.Parse(ip);
            _port = port;
        }

        public void SendMessageSocket(string _message)
        {
            try
            {
                String data = "";
                byte[] buffer = new byte[1024];
                _sender = new TcpClient();
                _sender.Connect(_serverAddress, _port);
                NetworkStream io = _sender.GetStream();

                Console.Write("Введите сообщение: ");
                Write(io, _message);

                data = Read(io);
                Console.WriteLine("Massege:\n {0}", data);
                _sender.Close();
            }
            catch (Exception)
            { 
            
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
                Console.WriteLine("Sorry.  You cannot write to this NetworkStream.");
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
                Console.WriteLine("Sorry.  You cannot read from this NetworkStream.");
                return "";
            }

        }

    }

}
