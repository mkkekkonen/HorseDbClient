using HorseDbClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HorseDbClient.Managers
{
    public class SocketManager
    {
        Socket socket = null;

        int HEADER = 21; // 0b010101;

        public SocketManager(string server, int port)
        {
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(server), port);
            Console.WriteLine("IPEndPoint: " + ipe);

            Socket tempSocket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            tempSocket.Connect(ipe);

            if (tempSocket.Connected)
            {
                socket = tempSocket;
            }
        }

        public List<Horse> GetHorses()
        {
            if (socket == null)
            {
                Console.WriteLine("Socket is null!");
                return null;
            }

            int header = IPAddress.HostToNetworkOrder(HEADER);
            byte[] bytesSent = BitConverter.GetBytes(header);
            socket.Send(bytesSent, bytesSent.Length, 0);

            List<Horse> horses = new List<Horse>();

            ushort recvHeader = (ushort)Recv("ushort");
            Console.WriteLine("recvHeader: " + recvHeader);

            if (recvHeader == HEADER) // check for header (0b010101)
            {
                ushort nHorses = (ushort)Recv("ushort");

                for(int i = 0; i < nHorses; i++)
                {
                    Horse horse = new Horse();
                    horse.Name = RecvStr(256);
                    horse.DateOfBirth = new Date();
                    horse.DateOfBirth.Day = (byte)Recv("byte");
                    horse.DateOfBirth.Month = (byte)Recv("byte");
                    horse.DateOfBirth.Year = (ushort)Recv("ushort");
                    horse.Gender = (Gender)(byte)Recv("byte");
                    horse.Breed = (Breed)(byte)Recv("byte");
                    Console.WriteLine(horse);
                    horses.Add(horse);

                    //if (i < nHorses - 1)
                    //    RecvStr(250);
                }
            }

            return horses;
        }

        private object Recv(string type, int strLen = 0)
        {
            int len = 0;
            switch(type)
            {
                case "ushort":
                    len = 2;
                    break;
                case "byte":
                    len = 1;
                    break;
            }
            byte[] bytesReceived = new byte[len];
            int nBytes = socket.Receive(bytesReceived, bytesReceived.Length, 0);
            object res = null;
            switch(type)
            {
                case "ushort":
                    if(BitConverter.IsLittleEndian)
                        Array.Reverse(bytesReceived);
                    res = BitConverter.ToUInt16(bytesReceived, 0);
                    break;
                case "byte":
                    res = bytesReceived[0];
                    break;
            }
            return res;
        }

        private string RecvStr(int len)
        {
            byte[] bytesReceived = new byte[len];
            int nBytes = socket.Receive(bytesReceived, bytesReceived.Length, 0);
            string charArr = Encoding.ASCII.GetString(bytesReceived);
            string str = charArr.Split('\0')[0];
            return str;
        }
    }
}
