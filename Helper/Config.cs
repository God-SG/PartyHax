using PartyHax.Helper.Xbox;
using PartyHax.Properties;

namespace PartyHax.Helper.Config
{
    public class AppSettings
    {
        
        public bool IsAttached = false;
        public long XauthLength { get; set; }
        public string AuthToken { get; set; }
        public string UserID { get; set; }
        public string GamerTag { get; set; }
        public string Email { get; set; }
        public string imageUrl { get; set; }

        public string Host = "https://test.partyhax.club/api/authorize/";

        public void UpdateSettings(string settingvalue, string value)
        {
            Settings Config = new Settings();
            switch (settingvalue)
            {
                case "Token":
                    Config.AuthToken = value;
                    Config.Save();
                    break;
                case "Email":
                    Config.Email = value;
                    Config.Save();
                    break;
                case "UserID":
                    Config.UserID = value;
                    Config.Save();
                    break;
                case "GamerTag":
                    Config.GamerTag = value;
                    Config.Save();
                    break;
                case "imageUrl":
                    Config.imageUrl = value;
                    Config.Save();
                    break;
            }
        }

    }
}
