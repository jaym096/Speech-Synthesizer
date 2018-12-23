using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class player : Form
    {
        public player()
        {
            InitializeComponent();
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }
        string[] f,audfiles;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Wave Files|*.wav";
            open.InitialDirectory = @"C:\Users\Jay\Documents\Visual Studio 2012\Projects\WindowsFormsApplication1\WindowsFormsApplication1\AudioFiles";
            if (open.ShowDialog() == DialogResult.OK)
            {
                f = open.SafeFileNames;
                audfiles = open.FileNames;
            }
            for (int i = 0; i < f.Length; i++)
            {
                comboBox_collection.Items.Add(f[i]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = audfiles[comboBox_collection.SelectedIndex];
        }
    }
}
