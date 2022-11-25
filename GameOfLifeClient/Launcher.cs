using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GameOfLifeClient
{
    public partial class Launcher : Form
    {
        public Launcher()
        {
            InitializeComponent();

        }

        private void singleplayer_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void multiplayerRadio_CheckedChanged(object sender, EventArgs e)
        {
            /*if (multiplayerRadio.Checked)
            {
                ipBox.Enabled = true;
                portBox.Enabled = true;
            }
            else
            {
                ipBox.Enabled = false;
                portBox.Enabled = false;
            }*/
        }
    }
}
