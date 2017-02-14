using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Seq.App.Asana
{
    public class AsanaWorkspace
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool is_organization { get; set; }

        private const string endpoint = "workspaces";

        public AsanaWorkspace Retreive(string workspaceId,Authentication authentication)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (var cli = new HttpClient())
            {
                cli.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authentication.AccessToken);

                var uri = string.Format("https://app.asana.com/api/1.0/{0}/{1}", endpoint, workspaceId);

                var resp = cli.GetAsync(uri).Result;

                var strrsp = resp.Content.ReadAsStringAsync().Result;
                var j = JObject.Parse(strrsp);

                var jobj = j.First.First.ToObject<AsanaWorkspace>();

                return jobj;
            }
        }
    }
}