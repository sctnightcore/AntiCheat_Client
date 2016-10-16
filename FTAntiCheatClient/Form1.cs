using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Remoting; //for the RemotingConfiguration Class
using System.Runtime.Remoting.Channels; //for the ChannelServices Class
using System.Runtime.Remoting.Channels.Http; //for the HttpChannel Class
using System.Drawing.Imaging;
using FTAntiCheatLibrary;
using ScreenShotDemo;
using System.Net;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace FTAntiCheatClient
{
    public partial class Form1 : Form
    {
        private IFTAntiCheat server;
        private string UserName = "";
        private string XUID = "";
        private string GamePath = "";

        public readonly uint DWM_EC_DISABLECOMPOSITION = 0;
        public readonly uint DWM_EC_ENABLECOMPOSITION = 1;

        [DllImport("dwmapi.dll", EntryPoint = "DwmEnableComposition")]

        protected static extern uint Win32DwmEnableComposition(uint uCompositionAction);

        public void Aero(bool a)
        {
            if (a)
            Win32DwmEnableComposition(DWM_EC_ENABLECOMPOSITION);
            if (!a)
            Win32DwmEnableComposition(DWM_EC_DISABLECOMPOSITION);
        }

        public Form1()
        {
            InitializeComponent();

            if (!System.IO.File.Exists("ac.ft"))
            {
                MessageBox.Show("Az sonra ilk kurulum için, bir defaya mahsus olarak Modern Warfare 3 oyununun kurulu olduğu" +
                " dizini seçeceksiniz. Açılacak olan pencereden 'Gözat' butonuna basın ve Modern Warfare 3 oyununun kurulu olduğu dizindeki" +
                " iw5mp.exe adlı dosyayı seçin. Daha sonra program tekrar açılana kadar bekleyin.");
                DirectoryChooser dc = new DirectoryChooser();
                dc.ShowDialog();
            }
            else
            {
                try
                {
                    Aero(false);
                }
                catch { }

                StreamReader sr1 = new StreamReader("ac.ft");
                GamePath = sr1.ReadLine();
                sr1.Close();

                GamePath += "/teknogods.ini";

                statuslbl.ForeColor = System.Drawing.Color.Red;
                statuslbl.Text = "Çevrimdışı";

                HttpClientChannel channel = new HttpClientChannel();
                ChannelServices.RegisterChannel(channel, false);

                if (File.Exists(GamePath))
                {
                    StreamReader sr = new StreamReader(GamePath);
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains("Name="))
                        {
                            UserName = Regex.Replace(line, "Name=", "");
                        }

                        if (line.Contains("ID="))
                        {
                            string tmpXUID = Regex.Replace(line, "ID=", "");
                            XUID = "01100001" + tmpXUID.ToLower();
                        }
                    }
                    sr.Close();

                    timer1.Start();
                }
                else
                {
                    MessageBox.Show("Anti hile sisteminin çalışması için gerekli olan Modern Warfare 3 dosyaları bulunamıyor."
                        + " (Hata: 1100SRF)");

                    this.Close();
                }

                Connect();
            }            
        }

        private void Connect()
        {
            server = (IFTAntiCheat)Activator.GetObject(typeof(IFTAntiCheat), "http://77.92.151.50:13101/FTAntiCheat.soap");
            statuslbl.Text = "Bağlandı";
            statuslbl.ForeColor = System.Drawing.Color.Green;

            if (!server.AddPlayerToList(XUID, UserName))
            {
                MessageBox.Show("FragTurk Anti Hile Sistemi aktifleştirilemedi. Aktif olmayan anti hile sistemi ile sunucu bağlantınız reddedilecektir.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("http://www.fragturk.com/");
            Process.Start(sInfo);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void CheckSSRequest()
        {
                if (server.SSRequested(XUID))
                {
                    SendRequestedSS();
                }
        }

        private void SendRequestedSS()
        {
            server.SSSended(XUID);
            
            ScreenCapture sc = new ScreenCapture();
            sc.CaptureScreenToFile(XUID+".png", ImageFormat.Png);

            ftpfile("/"+XUID+".png", XUID+".png"); 

        }

        public void ftpfile(string ftpfilepath, string inputfilepath)
        {
            string ftphost = "ftp.fragturk.com";
            //here correct hostname or IP of the ftp server to be given

            string ftpfullpath = "ftp://" + ftphost + ftpfilepath;
            WebRequest ftp = (WebRequest)WebRequest.Create(ftpfullpath);
            ftp.Credentials = new NetworkCredential("anticheatuser", "FragTurkAntiCheatUser2007");
            ftp.Proxy = null;
            //userid and password for the ftp server to given
            ftp.Method = WebRequestMethods.Ftp.UploadFile;
            FileStream fs = File.OpenRead(inputfilepath);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            fs.Close();
            Stream ftpstream = ftp.GetRequestStream();
            ftpstream.Write(buffer, 0, buffer.Length);
            ftpstream.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Aero(true);
            }
            catch { }

            if (server.RemovePlayerFromList(XUID))
            {
                this.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CheckSSRequest();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Çıkış_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void modernWarfare3DiziniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryChooser dc = new DirectoryChooser();
            dc.ShowDialog();
            if (dc.IsDisposed)
                Application.Restart();
        }

    }
}
