using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Memory;
using Leaf.xNet;
using System.Threading;
using XBL_IS_GAY.Helper;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace AIO_Tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public Mem m = new Mem();
        public Config Config = new Config();
        public void KillGamebar()
        {
            foreach (Process pr in Process.GetProcesses())
            {
                if (pr.ProcessName.Contains("xbox") || pr.ProcessName.Contains("Xbox")) pr.Kill();
                if (pr.ProcessName.Contains("gamebar") || pr.ProcessName.Contains("GameBar")) pr.Kill();
            }
        }
        public string AttachGamebar()
        {
            //MessageBox.Show("Make Sure xbox app is open when pressing this", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (!Config.IsAttached)
            {
                Process.Start("explorer.exe", "ms-gamingoverlay://");
                Thread.Sleep(1000);
                m.OpenProcess("gamebar");
                return "Attached to: GameBar (" + m.GetProcIdFromName("GameBar").ToString() + ")";

            }
           // MessageBox.Show("Make Sure xbox app is open when pressing this", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return "Failed To Attach Process!";
        }
        public async void GetToken()
        {
            try
            {
                #region HideThisShit
                var XauthStartAddress = (await m.AoBScan("41 75 74 68 6F 72 69 7A 61 74 69 6F 6E 3A 20 58 42 4C 33 2E 30 20 78 3D", true, true)).FirstOrDefault();
                var XauthStartAddressHex = (XauthStartAddress + 15).ToString("X");
                IEnumerable<long> XauthEndScanList = await m.AoBScan("0D 0A 43 6F 6E 74 65 6E 74 2D 4C 65 6E 67 74 68 3A 20", true, true);
                #endregion

                foreach (var XauthAddress in XauthEndScanList.ToArray())
                {
                    if (XauthAddress > XauthStartAddress) Config.XauthLength = (XauthAddress - XauthStartAddress - 15); break;
                }
                Config.AuthToken = Encoding.ASCII.GetString(m.ReadBytes(XauthStartAddressHex, Config.XauthLength));
                MessageBox.Show($"XBL Token Found!\n{Config.AuthToken}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("XBL Token Not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OverflowException Error)
            {
                MessageBox.Show(Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            tabPage1.Show();
            tabPage2.Hide();
            tabPage3.Hide();
        }

        private void bunifuButton9_Click(object sender, EventArgs e)
        {
            try
            {
                HttpRequest xb = new HttpRequest();
                xb.ReadWriteTimeout = 10000;
                xb.AddHeader("Connection", "Keep Alive");
                xb.AddHeader("xbltoken", Config.AuthToken);
                string response = xb.Get("https://partyhax.club/handler/api/api.php?key=&action=xbox_party_connecting").ToString();
                try
                {
                    var json = JObject.Parse(response);

                    string success = (string)json["success"];
                    string message = (string)json["message"];

                    if (success == "true")
                    {
                        MessageBox.Show(message);
                    }
                    else if (success == "false")
                    {
                        MessageBox.Show(message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There Was An Error Getting API Response Please join the discord\ndiscord.gg/modder");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There Was An Error Executing API Request Please join the Server\ndiscord.gg/modder");

            }
        }

        private void bunifuButton10_Click(object sender, EventArgs e)
        {
            try
            {
                HttpRequest xb = new HttpRequest();
                xb.ReadWriteTimeout = 10000;
                xb.AddHeader("Connection", "Keep Alive");
                xb.AddHeader("xbltoken", Config.AuthToken);
                string response = xb.Get("https://partyhax.club/handler/api/api.php?key=&action=xbox_party_disconnected").ToString();
                try
                {
                    var json = JObject.Parse(response);

                    string success = (string)json["success"];
                    string message = (string)json["message"];

                    if (success == "true")
                    {
                        MessageBox.Show(message);
                    }
                    else if (success == "false")
                    {
                        MessageBox.Show(message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There Was An Error Getting API Response Please join the discord\ndiscord.gg/modder");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There Was An Error Executing API Request Please join the Server\ndiscord.gg/modder");

            }
        }

        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            try
            {
                HttpRequest xb = new HttpRequest();
                xb.ReadWriteTimeout = 10000;
                xb.AddHeader("Connection", "Keep Alive");
                xb.AddHeader("xbltoken", Config.AuthToken);
                string response = xb.Get("https://partyhax.club/handler/api/api.php?key=&action=xbox_party_connected").ToString();
                try
                {
                    var json = JObject.Parse(response);

                    string success = (string)json["success"];
                    string message = (string)json["message"];

                    if (success == "true")
                    {
                        MessageBox.Show(message);
                    }
                    else if (success == "false")
                    {
                        MessageBox.Show(message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There Was An Error Getting API Response Please join the discord\ndiscord.gg/modder");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There Was An Error Executing API Request Please join the Server\ndiscord.gg/modder");

            }
        }

        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton13_Click(object sender, EventArgs e)
        {
            AttachGamebar();
        }

        private void bunifuButton14_Click(object sender, EventArgs e)
        {
            GetToken();
            System.Threading.Thread.Sleep(1000);
        }

        private void bunifuButton15_Click(object sender, EventArgs e)
        {
            KillGamebar();
        }


        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            
            try
            {
                HttpRequest xb = new HttpRequest();
                xb.ReadWriteTimeout = 10000;
                xb.AddHeader("Connection", "Keep Alive");
                xb.AddHeader("xbltoken", Config.AuthToken);
                string response = xb.Get("https://partyhax.club/handler/api/api.php?key=&action=xbox_party_unkickable").ToString();

                
                try
                {
                    var json = JObject.Parse(response);

                    string success = (string)json["success"];
                    string message = (string)json["message"];

                    if (success == "true")
                    {
                        MessageBox.Show(message);
                    }
                    else if (success == "false")
                    {
                        MessageBox.Show(message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There Was An Error Getting API Response Please join the discord\ndiscord.gg/modder");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There Was An Error Executing API Request Please join the Server\ndiscord.gg/modder");

            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://partyhax.club/");
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            try
            {
                HttpRequest xb = new HttpRequest();
                xb.ReadWriteTimeout = 10000;
                xb.AddHeader("Connection", "Keep Alive");
                xb.AddHeader("xbltoken", Config.AuthToken);
                string response = xb.Get("https://partyhax.club/handler/api/api.php?key=&action=xbox_party_enable_kick_host").ToString();
                try
                {
                    var json = JObject.Parse(response);

                    string success = (string)json["success"];
                    string message = (string)json["message"];

                    if (success == "true")
                    {
                        MessageBox.Show(message);
                    }
                    else if (success == "false")
                    {
                        MessageBox.Show(message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There Was An Error Getting API Response Please join the discord\ndiscord.gg/modder");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There Was An Error Executing API Request Please join the Server\ndiscord.gg/modder");

            }
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            try
            {
                HttpRequest xb = new HttpRequest();
                xb.ReadWriteTimeout = 10000;
                xb.AddHeader("Connection", "Keep Alive");
                xb.AddHeader("xbltoken", Config.AuthToken);
                string response = xb.Get("https://partyhax.club/handler/api/api.php?key=&action=xbox_hide_party").ToString();
                try
                {
                    var json = JObject.Parse(response);

                    string success = (string)json["success"];
                    string message = (string)json["message"];

                    if (success == "true")
                    {
                        MessageBox.Show(message);
                    }
                    else if (success == "false")
                    {
                        MessageBox.Show(message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There Was An Error Getting API Response Please join the discord\ndiscord.gg/modder");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There Was An Error Executing API Request Please join the Server\ndiscord.gg/modder");

            }
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            try
            {
                HttpRequest xb = new HttpRequest();
                xb.ReadWriteTimeout = 10000;
                xb.AddHeader("Connection", "Keep Alive");
                xb.AddHeader("xbltoken", Config.AuthToken);
                string response = xb.Get("https://partyhax.club/handler/api/api.php?key=&action=xbox_party_joinable").ToString();
                try
                {
                    var json = JObject.Parse(response);

                    string success = (string)json["success"];
                    string message = (string)json["message"];

                    if (success == "true")
                    {
                        MessageBox.Show(message);
                    }
                    else if (success == "false")
                    {
                        MessageBox.Show(message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There Was An Error Getting API Response Please join the discord\ndiscord.gg/modder");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There Was An Error Executing API Request Please join the Server\ndiscord.gg/modder");

            }
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            try
            {
                HttpRequest xb = new HttpRequest();
                xb.ReadWriteTimeout = 10000;
                xb.AddHeader("Connection", "Keep Alive");
                xb.AddHeader("xbltoken", Config.AuthToken);
                string response = xb.Get("https://partyhax.club/handler/api/api.php?key=&action=xbox_kick_gamebar_users").ToString();
                try
                {
                    var json = JObject.Parse(response);

                    string success = (string)json["success"];
                    string message = (string)json["message"];

                    if (success == "true")
                    {
                        MessageBox.Show(message);
                    }
                    else if (success == "false")
                    {
                        MessageBox.Show(message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There Was An Error Getting API Response Please join the discord\ndiscord.gg/modder");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There Was An Error Executing API Request Please join the Server\ndiscord.gg/modder");

            }
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            try
            {
                HttpRequest xb = new HttpRequest();
                xb.ReadWriteTimeout = 10000;
                xb.AddHeader("Connection", "Keep Alive");
                xb.AddHeader("xbltoken", Config.AuthToken);
                string response = xb.Get("https://partyhax.club/handler/api/api.php?key=&action=xbox_party_invite_only").ToString();
                try
                {
                    var json = JObject.Parse(response);

                    string success = (string)json["success"];
                    string message = (string)json["message"];

                    if (success == "true")
                    {
                        MessageBox.Show(message);
                    }
                    else if (success == "false")
                    {
                        MessageBox.Show(message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There Was An Error Getting API Response Please join the discord\ndiscord.gg/modder");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There Was An Error Executing API Request Please join the Server\ndiscord.gg/modder");

            }
        }

        private void bunifuButton11_Click(object sender, EventArgs e)
        {
            tabPage1.Hide();
            tabPage2.Show();
            tabPage3.Hide();
            MessageBox.Show("EVERYTHING IN TESTING HAS THE POTENTIAL OF GETTING YOU BANNED\nUSE AT YOUR OWN RISK!!!\ndiscord.gg/modder");
        }

        private void bunifuButton12_Click(object sender, EventArgs e)
        {
            tabPage1.Hide();
            tabPage2.Hide();
            tabPage3.Show();
        }

        private void bunifuButton18_Click(object sender, EventArgs e)
        {
            try
            {
                HttpRequest xb = new HttpRequest();
                xb.ReadWriteTimeout = 10000;
                xb.AddHeader("Connection", "Keep Alive");
                xb.AddHeader("xbltoken", Config.AuthToken);
                string response = xb.Get("https://partyhax.club/handler/api/api.php?key=&action=xbox_party_lock").ToString();
                try
                {
                    var json = JObject.Parse(response);

                    string success = (string)json["success"];
                    string message = (string)json["message"];

                    if (success == "true")
                    {
                        MessageBox.Show(message);
                    }
                    else if (success == "false")
                    {
                        MessageBox.Show(message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There Was An Error Getting API Response Please join the discord\ndiscord.gg/modder");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There Was An Error Executing API Request Please join the Server\ndiscord.gg/modder");

            }
        }

        private void bunifuButton17_Click(object sender, EventArgs e)
        {
            try
            {
                HttpRequest xb = new HttpRequest();
                xb.ReadWriteTimeout = 10000;
                xb.AddHeader("Connection", "Keep Alive");
                xb.AddHeader("xbltoken", Config.AuthToken);
                string response = xb.Get("https://partyhax.club/handler/api/api.php?key=&action=xbox_unhide_party").ToString();
                try
                {
                    var json = JObject.Parse(response);

                    string success = (string)json["success"];
                    string message = (string)json["message"];

                    if (success == "true")
                    {
                        MessageBox.Show(message);
                    }
                    else if (success == "false")
                    {
                        MessageBox.Show(message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There Was An Error Getting API Response Please join the discord\ndiscord.gg/modder");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There Was An Error Executing API Request Please join the Server\ndiscord.gg/modder");

            }
        }

        private void bunifuButton16_Click(object sender, EventArgs e)
        {
            try
            {
                HttpRequest xb = new HttpRequest();
                xb.ReadWriteTimeout = 10000;
                xb.AddHeader("Connection", "Keep Alive");
                xb.AddHeader("xbltoken", Config.AuthToken);
                string response = xb.Get("https://partyhax.club/handler/api/api.php?key=&action=xbox_party_killgp").ToString();
                try
                {
                    var json = JObject.Parse(response);

                    string success = (string)json["success"];
                    string message = (string)json["message"];

                    if (success == "true")
                    {
                        MessageBox.Show(message);
                    }
                    else if (success == "false")
                    {
                        MessageBox.Show(message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There Was An Error Getting API Response Please join the discord\ndiscord.gg/modder");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There Was An Error Executing API Request Please join the Server\ndiscord.gg/modder");

            }
        }

        private void bunifuButton19_Click(object sender, EventArgs e)
        {
            try
            {
                HttpRequest xb = new HttpRequest();
            xb.ReadWriteTimeout = 10000;
            xb.AddHeader("Connection", "Keep Alive");
            xb.AddHeader("xbltoken", Config.AuthToken);
            string response = xb.Get("https://partyhax.club/handler/api/api.php?key=&action=xbox_add_friend&targetgamertag=run%20expioit").ToString();

            try
            {
                var json = JObject.Parse(response);

                string success = (string)json["success"];
                string message = (string)json["message"];

                if (success == "true")
                {
                   // MessageBox.Show(message);
                }
                else if (success == "false")
                {
                    //MessageBox.Show(message);
                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show("There Was An Error Getting API Response Please join the discord\ndiscord.gg/modder");
            }
        }
            catch (Exception ex)
            {
             //   MessageBox.Show("There Was An Error Executing API Request Please join the Server\ndiscord.gg/modder");

            }
}

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            
        }

        private void bunifuButton20_Click(object sender, EventArgs e)
        {
            try
            {
                HttpRequest xb = new HttpRequest();
                xb.ReadWriteTimeout = 10000;
                xb.AddHeader("Connection", "Keep Alive");
                xb.AddHeader("xbltoken", Config.AuthToken);
                string response = xb.Get("https://partyhax.club/handler/api/api.php?key=&action=xbox_spoof_game_activity&gametitleid="+bunifuTextBox1.Text).ToString();
                try
                {
                    var json = JObject.Parse(response);

                    string success = (string)json["success"];
                    string message = (string)json["message"];

                    if (success == "true")
                    {
                        MessageBox.Show(message);
                    }
                    else if (success == "false")
                    {
                        MessageBox.Show(message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There Was An Error Getting API Response Please join the discord\ndiscord.gg/modder");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There Was An Error Executing API Request Please join the Server\ndiscord.gg/modder");

            }
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/modder");
        }
    }
}