using Leaf.xNet;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json.Linq;
using PartyHax.Helper;
using PartyHax.Helper.Xbox;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PartyHax
{
    public partial class MainFRM : Form
    {
        #region "Mouse Move Events"
        private bool _dragging = false;
        private Point _start_point = new Point(0, 0);
        private void Object_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;  // _dragging is your variable flag
            _start_point = new Point(e.X, e.Y);
        }
        private void Object_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }
        private void Object_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }
        private void ExitBTN_Click(object sender, EventArgs e) => Application.Exit();
        #endregion
        #region "ShitTimedLoop"
        private void Flash_Logo_Tick(object sender, EventArgs e)
        {
            L15.ForeColor = Color.Red;
            L1.ForeColor = Color.White;
            Flash_Logo.Stop();
            Thread.Sleep(50);
            Flash_Logo1.Start();
        }
        private void Flash_Logo1_Tick(object sender, EventArgs e)
        {
            L1.ForeColor = Color.Red;
            L2.ForeColor = Color.White;
            Flash_Logo1.Stop();
            Thread.Sleep(50);
            Flash_Logo2.Start();
        }
        private void Flash_Logo2_Tick(object sender, EventArgs e)
        {
            L2.ForeColor = Color.Red;
            L3.ForeColor = Color.White;
            Flash_Logo2.Stop();
            Thread.Sleep(50);
            Flash_Logo3.Start();
        }
        private void Flash_Logo3_Tick(object sender, EventArgs e)
        {
            L3.ForeColor = Color.Red;
            L4.ForeColor = Color.White;
            Flash_Logo3.Stop();
            Thread.Sleep(50);
            Flash_Logo4.Start();
        }
        private void Flash_Logo4_Tick(object sender, EventArgs e)
        {
            L4.ForeColor = Color.Red;
            LB_P.ForeColor = Color.White;
            Flash_Logo4.Stop();
            Thread.Sleep(50);
            Flash_Logo5.Start();
        }
        private void Flash_Logo5_Tick(object sender, EventArgs e)
        {
            LB_P.ForeColor = Color.Red;
            L5.ForeColor = Color.White;
            Flash_Logo5.Stop();
            Thread.Sleep(50);
            Flash_Logo6.Start();
        }
        private void Flash_Logo6_Tick(object sender, EventArgs e)
        {
            L5.ForeColor = Color.Red;
            L6.ForeColor = Color.White;
            Flash_Logo6.Stop();
            Thread.Sleep(50);
            Flash_Logo7.Start();
        }
        private void Flash_Logo7_Tick(object sender, EventArgs e)
        {
            L6.ForeColor = Color.Red;
            L7.ForeColor = Color.White;
            Flash_Logo7.Stop();
            Thread.Sleep(50);
            Flash_Logo8.Start();
        }
        private void Flash_Logo8_Tick(object sender, EventArgs e)
        {
            L7.ForeColor = Color.Red;
            L8.ForeColor = Color.White;
            Flash_Logo8.Stop();
            Thread.Sleep(50);
            Flash_Logo9.Start();
        }
        private void Flash_Logo9_Tick(object sender, EventArgs e)
        {
            L8.ForeColor = Color.Red;
            L9.ForeColor = Color.White;
            Flash_Logo9.Stop();
            Thread.Sleep(50);
            Flash_Logo10.Start();
        }
        private void Flash_Logo10_Tick(object sender, EventArgs e)
        {
            L9.ForeColor = Color.Red;
            L10.ForeColor = Color.White;
            Flash_Logo10.Stop();
            Thread.Sleep(50);
            Flash_Logo11.Start();
        }
        private void Flash_Logo11_Tick(object sender, EventArgs e)
        {
            L10.ForeColor = Color.Red;
            L11.ForeColor = Color.White;
            Flash_Logo11.Stop();
            Thread.Sleep(50);
            Flash_Logo12.Start();
        }
        private void Flash_Logo12_Tick(object sender, EventArgs e)
        {
            L11.ForeColor = Color.Red;
            L12.ForeColor = Color.White;
            Flash_Logo12.Stop();
            Thread.Sleep(50);
            Flash_Logo13.Start();
        }
        private void Flash_Logo13_Tick(object sender, EventArgs e)
        {
            L12.ForeColor = Color.Red;
            L13.ForeColor = Color.White;
            Flash_Logo13.Stop();
            Thread.Sleep(50);
            Flash_Logo14.Start();
        }
        private void Flash_Logo14_Tick(object sender, EventArgs e)
        {
            L13.ForeColor = Color.Red;
            L14.ForeColor = Color.White;
            Flash_Logo14.Stop();
            Thread.Sleep(50);
            Flash_Logo15.Start();
        }
        private void Flash_Logo15_Tick(object sender, EventArgs e)
        {
            L14.ForeColor = Color.Red;
            L15.ForeColor = Color.White;
            Flash_Logo15.Stop();
            Thread.Sleep(50);
            Flash_Logo.Start();
        }
        #endregion
        #region "RemoveLater"
        public int i = 0;
        private static Random random = new Random();
        public static string RandomString(int length) => new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length).Select(s => s[random.Next(s.Length)]).ToArray());
        #endregion

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        private Utils Utils = new Utils();
        private API XBLAPI = new API();
        private phAPI AuthAPI = new phAPI();
        private List<Label> Labels = new List<Label>();
        private List<System.Windows.Forms.Timer> Timers = new List<System.Windows.Forms.Timer>();
        public bool HasAuthed, isOAuthAPI = false;
        public MainFRM()
        {
            InitializeComponent();

            WebClient webClient = new WebClient();

            try
            {
                if (!webClient.DownloadString("https://pastebin.com/raw/dJPHHRLm").Contains("1.0.1"))
                {
                    if (MessageBox.Show("Looks like there is an update! Do you want to download it?", "PartyHax", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        using (var client = new WebClient())
                        {
                            Process.Start("Updater.exe");
                            this.Close();
                        }
                }
            }
            catch
            {

            }

            PartyMenu.Renderer = new Helper.MenuStrip.Overrides();
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 6, 6));
            pictureBox1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 6, 6));
            ExitBTN.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 6, 6));

            Timers.Add(Flash_Logo);
            Timers.Add(Flash_Logo1); Timers.Add(Flash_Logo2); Timers.Add(Flash_Logo3);
            Timers.Add(Flash_Logo4); Timers.Add(Flash_Logo5); Timers.Add(Flash_Logo6);
            Timers.Add(Flash_Logo7); Timers.Add(Flash_Logo8); Timers.Add(Flash_Logo9);
            Timers.Add(Flash_Logo10); Timers.Add(Flash_Logo11); Timers.Add(Flash_Logo12);
            Timers.Add(Flash_Logo13); Timers.Add(Flash_Logo14); Timers.Add(Flash_Logo15);
            Labels.Add(L1); Labels.Add(L2); Labels.Add(L3);
            Labels.Add(L4); Labels.Add(L5); Labels.Add(L6);
            Labels.Add(L7); Labels.Add(L8); Labels.Add(L9);
            Labels.Add(L10); Labels.Add(L11); Labels.Add(L12);
            Labels.Add(L13); Labels.Add(L14); Labels.Add(L15);
            savetokenbtn.Enabled = false;
        }
        private void UpdateProfilePic()
        {
            using (var wc = new WebClient())
            {
                if (!File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\pfp.png"))
                {
                    wc.DownloadFile(XBLAPI.Config.imageUrl, $"{AppDomain.CurrentDomain.BaseDirectory}\\pfp.png");
                }
                Thread.Sleep(200);
                Image pfp = new Bitmap($"{AppDomain.CurrentDomain.BaseDirectory}\\pfp.png");
                AviBox.BackgroundImage = pfp;
            }
        }
        private async void InitializeWebService()
        {
            if (!HasAuthed)
            {
                var WVE = await CoreWebView2Environment.CreateAsync(null, $"{AppDomain.CurrentDomain.BaseDirectory}\\BrowserCache\\");
                await wv.EnsureCoreWebView2Async(WVE);
                wv.Source = new Uri(XBLAPI.Config.Host);
                HasAuthed = true;
            }
        }
        public void UserInfoUpdate()
        {
            if (XBLAPI.Config.UserID != string.Empty || XBLAPI.Config.GamerTag != string.Empty || XBLAPI.Config.Email != string.Empty)
            {
                if (HideInfo.Toggled)
                {
                    xuid.Text = $" • XUID: ***********";
                    gamertag.Text = $" • GAMERTAG: ***********";
                    email.Text = $" • EMAIL: ***********";
                    astatus.Text = $" • STATUS: Online";
                    status.Text = $"Status: Authenticated!";
                    UpdateProfilePic();
                }
                else
                {
                    xuid.Text = $" • XUID: {XBLAPI.Config.UserID}";
                    gamertag.Text = $" • GAMERTAG: {XBLAPI.Config.GamerTag}";
                    email.Text = $" • EMAIL: {XBLAPI.Config.Email}";
                    astatus.Text = $" • STATUS: Online";
                    status.Text = $"Status: Authenticated!";
                    UpdateProfilePic();
                }
            }
        }
        public string Between(string STR, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = STR.IndexOf(LastString);
            FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }
        public void ChangePage(string page)
        {

            Point visible = new Point(1, 73);
            Point hide1 = new Point(6666, 6666);
            Point hide2 = new Point(7777, 7777);
            Point hide3 = new Point(8888, 8888);

            Point hider1 = new Point(-6666, -6666);
            Point hider2 = new Point(-7777, -7777);
            Point hider3 = new Point(-8888, -8888);
            switch (page)
            {
                case "0":
                    Page1.Location = visible;
                    Page2.Location = hide2;
                    Page3.Location = hide3;
                    break;
                case "1":
                    Page1.Location = hide1;
                    Page2.Location = visible;
                    Page3.Location = hide3;
                    break;
                case "2":
                    Page1.Location = hide1;
                    Page2.Location = hide2;
                    Page3.Location = visible;
                    break;
                case "r1":
                    RiskyPage1.Location = visible;
                    RiskyPage2.Location = hider2;
                    RiskyPage3.Location = hider3;
                    break;
                case "r2":
                    RiskyPage1.Location = hider1;
                    RiskyPage2.Location = visible;
                    RiskyPage3.Location = hider3;
                    break;
                case "r3":
                    RiskyPage1.Location = hider1;
                    RiskyPage2.Location = hider2;
                    RiskyPage3.Location = visible;
                    break;


            }
        }
        public void ChangeScene(string scene)
        {
            Point visible = new Point(8, 88);

            Point hide1 = new Point(1111, 1111);
            Point hide2 = new Point(2222, 2222);
            Point hide3 = new Point(3333, 3333);
            Point hide4 = new Point(4444, 4444);
            Point hide5 = new Point(5555, 5555);

            switch (scene)
            {
                case "0":
                    Home.Location = visible;
                    Main.Location = hide2;
                    Risky.Location = hide3;
                    Settings.Location = hide4;
                    Browser.Location = hide5;
                    break;
                case "1":
                    Home.Location = hide1;
                    Main.Location = visible;
                    Risky.Location = hide3;
                    Settings.Location = hide4;
                    Browser.Location = hide5;
                    break;
                case "2":
                    Home.Location = hide1;
                    Main.Location = hide2;
                    Risky.Location = visible;
                    Settings.Location = hide4;
                    Browser.Location = hide5;
                    break;
                case "3":
                    Home.Location = hide1;
                    Main.Location = hide2;
                    Risky.Location = hide3;
                    Settings.Location = visible;
                    Browser.Location = hide5;
                    break;
                case "4":
                    Home.Location = hide1;
                    Main.Location = hide2;
                    Risky.Location = hide3;
                    Settings.Location = hide4;
                    Browser.Location = visible;
                    InitializeWebService();
                    break;
            }
        }
        public async Task<string> GetTokenFromPage()
        {
            try
            {
                const string quote = "\"";
                var html = await wv.CoreWebView2.ExecuteScriptAsync("document.body.outerHTML");
                var remove_html = Between(html, "{", "}");
                var temp_token = remove_html.Replace($"\\{quote}Token\\{quote}:\\{quote}", "");
                string Token = temp_token.Replace($"\\{quote}", "");
                XBLAPI.Config.AuthToken = Token;
                return Token;
            }
            catch
            {
                return "Not Found";
            }
        }
        private void homebtn_Click(object sender, EventArgs e) => ChangeScene("0");
        private void mainbtn_Click(object sender, EventArgs e) => ChangeScene("1");
        private void riskybtn_Click(object sender, EventArgs e) => ChangeScene("2");
        private void settingsbtn_Click(object sender, EventArgs e) => ChangeScene("3");
        private void button1_Click(object sender, EventArgs e) => ChangePage("0");
        private void button2_Click(object sender, EventArgs e) => ChangePage("1");
        private void button3_Click(object sender, EventArgs e) => ChangePage("2");
        private void button25_Click(object sender, EventArgs e) => ChangePage("r1");
        private void button24_Click(object sender, EventArgs e) => ChangePage("r2");
        private void button23_Click(object sender, EventArgs e) => ChangePage("r3");
        private void SiteBTN_Click(object sender, EventArgs e) => Process.Start("https://partyhax.club");
        private void DiscordBTN_Click(object sender, EventArgs e) => Process.Start("https://discord.gg/xboxmods");
        private void githubbtn_Click(object sender, EventArgs e) => Process.Start("https://github.com/God-SG/PartyHax");
        private void xboxfrbtn_Click(object sender, EventArgs e)
        {
            if (status.Text.Contains("Authenticated")) XBLAPI.SendFriendRequest();
        }
        private void GetTokenBTN_Click(object sender, EventArgs e)
        {
            if (isAPI.Toggled && isMemory.Toggled) return;
            if (!isAPI.Toggled && !isMemory.Toggled) return;

            if (isAPI.Toggled && !isMemory.Toggled)
            {
                ChangeScene("4");
            }
            else if (isMemory.Toggled && !isAPI.Toggled)
            {
                Utils.KillXboxApps();
                Thread.Sleep(200);
                XBLAPI.AttachGamebar();
                Thread.Sleep(2000);
                XBLAPI.GetTokenFromMemory();
            }
        }
        private void authbtn_Click(object sender, EventArgs e)
        {
            if (isAPI.Toggled && isMemory.Toggled) return;
            if (!isAPI.Toggled && !isMemory.Toggled) return;
            if (XBLAPI.Config.AuthToken == string.Empty) return;

            if (isAPI.Toggled && !isMemory.Toggled)
            {
                XBLAPI.GetUser();
                XBLAPI.GetEmail();
                XBLAPI.GetProfilePic();
                UserInfoUpdate();
                MessageBox.Show(
                    $"•- DeviceType: {Utils.CheckDevice(XBLAPI.GetDeviceType())}!\n" +
                    $"•- Limited Authentication: Successful!",
                    $"[Limited] Successful",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                riskybtn.Enabled = true;
                mainbtn.Enabled = true;

            }
            else if (isMemory.Toggled && !isAPI.Toggled)
            {
                XBLAPI.GetUser();
                XBLAPI.GetEmail();
                XBLAPI.GetProfilePic();
                UserInfoUpdate();
                MessageBox.Show(
                     $"•- DeviceType: {Utils.CheckDevice(XBLAPI.GetDeviceType())}!\n" +
                     $"•- Full Authentication: Successful!",
                     $"[FULL] Successful",
                     MessageBoxButtons.OK, MessageBoxIcon.Information);
                riskybtn.Enabled = true;
                mainbtn.Enabled = true;
            }
        }
        private async void savetokenbtn_Click(object sender, EventArgs e)
        {
            string token = await GetTokenFromPage();
            if (token.Contains("Not Found"))
            {
                MessageBox.Show("•- Please Wait Until You See Your Token!...", $"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ChangeScene("0");
        }
        private void savekeybtn_Click(object sender, EventArgs e)
        {

        }
        private void IsFlashing_ToggledChanged()
        {
            if (IsFlashing.Toggled)
            {
                Flash_Logo.Start();
            }
            else
            {
                foreach (var timer in Timers) timer.Enabled = false;

                foreach (var lbs in Labels)
                {
                    if (lbs.Name.Contains("L"))
                    {
                        lbs.ForeColor = Color.Red;
                    }
                }
            }
        }
        private void wv_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (e.IsSuccess && isOAuthAPI) savetokenbtn.Enabled = true;
        }
        private void wv_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            if (e.Uri.Contains("oauth/?code=")) isOAuthAPI = true;
        }

        /// <summary>
        /// Workspace: Everything Below Is Being Worked On!
        /// </summary>

        private void button26_Click(object sender, EventArgs e)
        {
           
        }
        private void button28_Click(object sender, EventArgs e)
        {
            //AuthAPI.GrabParty();
            MessageBox.Show("Feature isnt done yet, Please join the Server\ndiscord.gg/xboxmods");
        }
        private void p1_Click(object sender, EventArgs e)
        {
            if (PartyList.Rows.Count >= 0) Clipboard.SetText((string)PartyList.CurrentRow.Cells["UserID"].Value);
        }
        private void p2_Click(object sender, EventArgs e)
        {
            if (PartyList.Rows.Count >= 0) Clipboard.SetText((string)PartyList.CurrentRow.Cells["gt"].Value);
        }

        //private void hidepartytoggle_ToggledChanged()
        //{
        //    switch (hidepartytoggle.Toggled)
        //    {
        //        case true:
        //            MessageBox.Show(AuthAPI.MakeApiRequstCustom("xboxhideparty", "user_id", XBLAPI.Config.UserID, XBLAPI.Config.AuthToken), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            break;
        //        case false:
        //            MessageBox.Show(AuthAPI.MakeApiRequstCustom("xboxunhideparty", "user_id", XBLAPI.Config.UserID, XBLAPI.Config.AuthToken), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            break;
        //    }
        //}

        //private void inviteonlytoogle_ToggledChanged()
        //{
        //    switch (hidepartytoggle.Toggled)
        //    {
        //        case true:
        //            MessageBox.Show(AuthAPI.MakeApiRequst("xboxpartyinviteonly", XBLAPI.Config.UserID, XBLAPI.Config.AuthToken), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            break;
        //        case false:
        //            MessageBox.Show(AuthAPI.MakeApiRequst("xboxpartyjoinable", XBLAPI.Config.UserID, XBLAPI.Config.AuthToken), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            break;
        //    }
        //}


        private void sG_Toggle5_ToggledChanged()
        {
            MessageBox.Show(AuthAPI.MakeApiRequst("xboxpartyunkickable", XBLAPI.Config.UserID, XBLAPI.Config.AuthToken), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void hkicktoggle_ToggledChanged()
        {
            MessageBox.Show(AuthAPI.MakeApiRequst("xboxenablekickhost", XBLAPI.Config.UserID, XBLAPI.Config.AuthToken), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //private void lockpartytoggle_ToggledChanged()
        //{
        //    switch (hidepartytoggle.Toggled)
        //    {
        //        case true:
        //            MessageBox.Show(AuthAPI.MakeApiRequst("xboxpartylock", XBLAPI.Config.UserID, XBLAPI.Config.AuthToken), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            break;
        //        case false:
        //            MessageBox.Show(AuthAPI.MakeApiRequst("xboxpartyunlock", XBLAPI.Config.UserID, XBLAPI.Config.AuthToken), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            break;
        //    }
        //}

        //private void sG_Toggle6_ToggledChanged()
        //{
        //    switch (hidepartytoggle.Toggled)
        //    {
        //        case true:
        //            MessageBox.Show(AuthAPI.MakeApiRequstCustom("xboxprofileappearoffline", "userid", XBLAPI.Config.UserID, XBLAPI.Config.AuthToken), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            break;
        //        case false:
        //            MessageBox.Show(AuthAPI.MakeApiRequstCustom("xboxprofileappearonline", "userid", XBLAPI.Config.UserID, XBLAPI.Config.AuthToken), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            break;
        //    }
        //}

        private void button30_Click(object sender, EventArgs e)
        {
            try
            {
                HttpRequest xb = new HttpRequest();
                xb.ReadWriteTimeout = 10000;
                xb.AddHeader("Connection", "Keep Alive");
                xb.AddHeader("xbltoken", XBLAPI.Config.AuthToken);
                string response = xb.Get("https://partyhax.club/handler/api/api.php?key=public&action=xbox_party_killgp").ToString();
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
                    MessageBox.Show("There Was An Error Getting API Response Please join the discord\ndiscord.gg/xboxmods");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There Was An Error Executing API Request Please join the Server\ndiscord.gg/xboxmods");

            }
            // MessageBox.Show(AuthAPI.MakeApiRequst("xboxpartykillgp", XBLAPI.Config.UserID, XBLAPI.Config.AuthToken), "", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button31_Click(object sender, EventArgs e)
        {
            MessageBox.Show(AuthAPI.MakeApiRequst("kickgamebarusers", XBLAPI.Config.UserID, XBLAPI.Config.AuthToken), "", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button33_Click(object sender, EventArgs e)
        {
            MessageBox.Show(AuthAPI.MakeApiRequst("xboxpartyconnected", XBLAPI.Config.UserID, XBLAPI.Config.AuthToken), "", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button34_Click(object sender, EventArgs e)
        {
            MessageBox.Show(AuthAPI.MakeApiRequst("xboxpartyconnecting", XBLAPI.Config.UserID, XBLAPI.Config.AuthToken), "", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button35_Click(object sender, EventArgs e)
        {
            try
            {
                HttpRequest xb = new HttpRequest();
                xb.ReadWriteTimeout = 10000;
                xb.AddHeader("Connection", "Keep Alive");
                xb.AddHeader("xbltoken", XBLAPI.Config.AuthToken);
                string response = xb.Get("https://partyhax.club/handler/api/api.php?key=public&action=xbox_party_disconnected").ToString();
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
                    MessageBox.Show("There Was An Error Getting API Response Please join the discord\ndiscord.gg/xboxmods");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There Was An Error Executing API Request Please join the Server\ndiscord.gg/xboxmods");

            }
            //MessageBox.Show(AuthAPI.MakeApiRequst("xboxpartydisconneted", XBLAPI.Config.UserID, XBLAPI.Config.AuthToken), "", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button28_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(AuthAPI.MakeApiRequst("xboxpartyinviteonly", XBLAPI.Config.UserID, XBLAPI.Config.AuthToken), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button29_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(AuthAPI.MakeApiRequst("xboxpartyjoinable", XBLAPI.Config.UserID, XBLAPI.Config.AuthToken), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button32_Click(object sender, EventArgs e)
        {
            MessageBox.Show(AuthAPI.MakeApiRequst("xboxpartyunkickable", XBLAPI.Config.UserID, XBLAPI.Config.AuthToken), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button36_Click(object sender, EventArgs e)
        {
            try
            {
                HttpRequest xb = new HttpRequest();
                xb.ReadWriteTimeout = 10000;
                xb.AddHeader("Connection", "Keep Alive");
                xb.AddHeader("xbltoken", XBLAPI.Config.AuthToken);
                string response = xb.Get("https://partyhax.club/handler/api/api.php?key=public&action=xbox_hide_party").ToString();
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
                    MessageBox.Show("There Was An Error Getting API Response Please join the discord\ndiscord.gg/xboxmods");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There Was An Error Executing API Request Please join the Server\ndiscord.gg/xboxmods");

            }

            //  MessageBox.Show(AuthAPI.MakeApiRequstCustom("xboxhideparty", "user_id", XBLAPI.Config.UserID, XBLAPI.Config.AuthToken), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button37_Click(object sender, EventArgs e)
        {
            MessageBox.Show(AuthAPI.MakeApiRequst("xboxenablekickhost", XBLAPI.Config.UserID, XBLAPI.Config.AuthToken), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button38_Click(object sender, EventArgs e)
        {
            try
            {
                HttpRequest xb = new HttpRequest();
                xb.ReadWriteTimeout = 10000;
                xb.AddHeader("Connection", "Keep Alive");
                xb.AddHeader("xbltoken", XBLAPI.Config.AuthToken);
                string response = xb.Get("https://partyhax.club/handler/api/api.php?key=public&action=xbox_party_lock").ToString();
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
                    MessageBox.Show("There Was An Error Getting API Response Please join the discord\ndiscord.gg/xboxmods");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There Was An Error Executing API Request Please join the Server\ndiscord.gg/xboxmods");

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button39_Click(object sender, EventArgs e)
        {
            try
            {
                HttpRequest xb = new HttpRequest();
                xb.AddHeader("Connection", "Keep Alive");
                string response = xb.Get("https://xboxresolver.io/search?Gamertag="+textBox1.Text).ToString();
                try
                {
                    var json = JObject.Parse(response);

                    string message = (string)json["message"];

                        MessageBox.Show(message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There Was An Error Getting API Response Please join the discord\ndiscord.gg/xboxmods");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There Was An Error Executing API Request Please join the Server\ndiscord.gg/xboxmods");

            }

        }

        private void RiskyPage1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button41_Click(object sender, EventArgs e) => Process.Start("https://pastebin.com/EN4dHEm5");

        private void button40_Click(object sender, EventArgs e)
        {
            try
            {
                HttpRequest xb = new HttpRequest();
                xb.ReadWriteTimeout = 10000;
                xb.AddHeader("Connection", "Keep Alive");
                xb.AddHeader("xbltoken", XBLAPI.Config.AuthToken);
                string response = xb.Get("https://partyhax.club/handler/api/api.php?key=public&action=xbox_spoof_game_activity&gametitleid=" + richTextBox1.Text).ToString();
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
                    MessageBox.Show("There Was An Error Getting API Response Please join the discord\ndiscord..gg/xboxmods");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There Was An Error Executing API Request Please join the Server\ndiscord..gg/xboxmods");

            }
        }

        private void button42_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Feature isnt done yet, Please join the Server\ndiscord.gg/xboxmods");
        }

        private void sG_Toggle7_ToggledChanged()
        {

        }

        private void button29_Click(object sender, EventArgs e) { PartyList.Rows.Clear(); i = 0; }
    }
}