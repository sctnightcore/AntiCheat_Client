using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FTAntiCheatClient
{
    public partial class DirectoryChooser : Form
    {
        public DirectoryChooser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.DefaultExt = ".exe";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("ac.ft");
            sw.Write(Path.GetDirectoryName(openFileDialog1.FileName));
            sw.Close();

            this.Close();
            Application.Restart();
        }
    }
}
