using HorseDbClient.Managers;
using HorseDbClient.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HorseDbClient
{
    public partial class Form1 : Form
    {
        SocketManager socketManager;

        public Form1(SocketManager socketManager)
        {
            InitializeComponent();

            this.socketManager = socketManager;
        }

        private void getDataButton_Click(object sender, EventArgs e)
        {
            List<Horse> horses = socketManager.GetHorses();
            BindingSource dataSource = DataFormatter.FormatData(horses);
            horseGridView.DataSource = dataSource;
        }
    }
}
