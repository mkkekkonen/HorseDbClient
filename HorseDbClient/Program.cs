using HorseDbClient.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HorseDbClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length != 2)
                Application.Exit();

            string host = args[0];
            Console.WriteLine("Host: " + host);
            int port = int.Parse(args[1]);
            Console.WriteLine("Port: " + port);

            SocketManager socketManager = new SocketManager(host, port);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(socketManager));
        }
    }
}
