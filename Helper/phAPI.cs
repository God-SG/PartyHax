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
                    var response = request.Get($"https://test.partyhax.club/api/grabxboxparty/?myxuid=2535422092633497&token=); //&key={key}");
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
