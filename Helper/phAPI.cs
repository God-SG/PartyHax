using System;
using Leaf.xNet;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace PartyHax.Helper
{
    public class phAPI
    {

        public string MakeApiRequstCustom(string type = "", string param = "", string xuid = "", string xbltoken = "", string key = "") //
        {
            try
            {
                using (var request = new HttpRequest())
                {
                    var response = request.Get($"https://test.partyhax.club/api/{type}/?{param}={xuid}&token={xbltoken}"); //&key={key}");
                    JObject Json = JObject.Parse(response.ToString());
                    if (response.IsOK)
                    {
                        if (response.ToString().Contains("success")) return Json["message"].ToString();
                    }
                    return Json["error"].ToString();
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
            return "Unknown Error!\nJoin discord.gg/modder for help.";
        }

        public void GrabParty()
        {
            try
            {
                using (var request = new HttpRequest())
                {
                    var response = request.Get($"https://test.partyhax.club/api/grabxboxparty/?myxuid=2535422092633497&token=XBL3.0%20x%3D13445396667068502154%3BeyJlbmMiOiJBMTI4Q0JDK0hTMjU2IiwiYWxnIjoiUlNBLU9BRVAiLCJjdHkiOiJKV1QiLCJ6aXAiOiJERUYiLCJ4NXQiOiIxZlVBejExYmtpWklFaE5KSVZnSDFTdTVzX2cifQ.Z_fGBrXiiOIIINau2dT33Mfx0XvuwfoK_wJWueICd5FciH6H9TB8GokEjc6b2nwYaW_OmO2nrp0o0ZCsCQz9K5CDHt5toM6ON46qqNVti5F_-v5su54tFRpyD5WQpTUi6RlMx-0l9KNELzCII2basonAyo560RgR59y25kQwJFI.sfJQvoL58XL--WE7M2En2Q.9hB_oHINyVwcRusDIw0Uttt38oSg-z2tl8GsEWrCZx1cCOhfF4s-OlFt2hMa8NsnkA170rOLFxbfLO4hGtXTyPWKoHsq4gMseRQETnLkIOYYqyBUcbv7bawdmscuDqdvMBvu94EH1KQOipov4XJtVdrGKlcyXC4v_4x2o8MNefuMxVFeukfXjwzM8BIG2ZVkQQdU8hkYv53T_FRhLqIjKKlFg8Qjhdk36Nrjj1c9ZiZXzJtOpg7D2EqbkkZzNTT5W5JGGIJuXWHhjgIJpcUCO8Upaqx7kOXuTHtZnUb0Y4s-Lc0VxIkiPq9gdjsjwddyUIb5GVUMN3DFbH4ZTq4CgJoo-UfDdsXZdXN6LgxjqnFFI27PR3wIpfWoU0ruIFl5f_WqD7OEiZPz89JQUKXbHSj2bnjgRWIOSdWeshu-q9JoQwd6-URRCQNMBg7kaWE6wkwRkidgmFilR0JVBTDsDpDnOgm0cyKibZMUNuqizr6YAGsoJhyJsbSUrCiTcm_euTllcELexIjk0ynkz5lcB_khakD8oWNUwD-EaJ9npPbTUHPQu1uTGS0DIKaGBgY3fuoxc0cbHiauJUtn39jK-W40tPum_mZcFWkOTyvM3onb5jvtBeIqwT2fkxB7em6bFLm9RQDwcrxMHaKIYytJ4XjTMEqrf_kQUTEl6owYYOo-bZoIWH5OTfLuzTc4tcctc4-ZDwONuVgLW8g8X0q38fv8ISaqaireR_wAEpF4BChbKaGgIVfoMFcr99STiz-TmbeOcubDDjol3GK_VWO2sgMEzUAQl4jQeWl3X6Odx4dnHATzQCk0V0MJ2SSmTFBIy5UOmN9B_obn9gM3HzRaOfi31InVaBKOabfed3uhzof4YVXkm7XIcITKa7o-fTMtxsC_KqAzso1ZhzmDBciirHaAo486kQD-x1eYXj1ujzvxNx5EmKd5vIKKgrvbTbsi0NgPDfefzC2La8r4Y2fissFpFfBmrAEPn-fc5NNicU9vq5hOnAu_QHRejENb04avE2t6wUKbLW3GGrW3cbUjdkKhCr-bthOMT2P9ZEZ3m9Vx9OLHaa_m_OxjWOE1JR4eIk3taoMmSZrSGh2eFcc-GKD6uAd4F2yr4GAqzJ0HvWYzyip6J-m1gQ1KoGH4qyRpqqMzpZ8VkhvbTjP6z2wDz-n2Svkrd3yyEaMb6XcVl6V_EnsBDwQaepPCZGoDsqlHrV7xPn_tA_48mnqc5iFbtEIeQ2ST4N10_5fDXhOy4glYnYHKJLmQ3xZ_EhUMy094gkyawqWWsc-gwFIxvj2U5iHYWluYMXgpyqjUjXLkB7qC8bCkQWGBHvSsK-foDoAkUwcP6DmJsKN50MAjZUqYR-7eqhVIM4feFXzEX7n2FMR7qAkFAmQaSIpw6BdpY368a9_u0jqr0yavwGtZKwySenyHTUN07cArCWCzxJU2rRMfMyk5lJKEPiyOVZ8AsqvR6jHrlUxcKKtzS7JbJgZGEOQTryUP-Uyv1mlSZr5pdjI6IdOfzs4t8H2zzTVj_KGP82KYbv2L66L5ApzsZqa9ml9o3NbvYVvoU73vAMSheFvjCTI_OZ6fJgjAfqWpLbJcKgJhErA_LUUFUtumCAdWeyEuqDE8GS8rPcXHj4viclKfRrW_BTUSBm2cAPsFb1rJSFH6knEfah6DvK3aqYJwYJLSkRL5Dce69jF6hdrjHdg372nYe84rmbj9HKrIUiyk4uH5fGPqp32TgKSI4EHT6zYvNG9CBoEvneWmUnexOUCJ5GIB9CZEmAmC3OgkotkY2y8WyvKcoyc48GREcPI0CMdz8r2UPoU2HvJyRHEdzgOUnE7r6DPfrjasStEIiH7eUdaBuExOjPSzQ1KRZcfkuC3WmFrTFR4sz6m4B5a2fYLN0zKSdr0I3NnscBEihQtpIWaJjgGU1DxF3f-E4sAi8w.TfA6n5mDC8W02UxfcSC0O5jmfJqCj0E8Zkm4qUzHHqY"); //&key={key}");
                    Console.WriteLine(response.ToString());
                    Console.Read();
                    dynamic cleanedjson = response.ToString().Replace(Environment.NewLine, "").Replace("\\", "").Replace("n", "").Replace("    ", "").Replace(" ", "");
                    Console.WriteLine(cleanedjson.ToString());
                    Console.Read();
                    JObject json = JObject.Parse(cleanedjson);
                    if (response.IsOK)
                    {
                        Console.WriteLine(json.ToString());
                        Console.Read();
                    }
                    //return Json["error"].ToString();
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
            //return "Unknown Error!\nJoin discord.gg/modder for help.";
        }


        public string MakeApiRequst(string type = "", string xuid = "", string xbltoken = "", string key = "") //
        {
            try
            {
                using (var request = new HttpRequest())
                {
                    var response = request.Get($"https://test.partyhax.club/api/{type}/?xuid={xuid}&token={xbltoken}"); //&key={key}");
                    JObject Json = JObject.Parse(response.ToString());
                    if (response.IsOK)
                    {
                        if (response.ToString().Contains("success")) return Json["message"].ToString();    
                    }
                    return Json["error"].ToString();
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
            return "Unknown Error!\nJoin discord.gg/modder for help.";
        }
    }
}
