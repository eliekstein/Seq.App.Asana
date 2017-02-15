using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Seq.App.Asana
{
    public class AsanaWorkspace : AsanaBaseObject<string>
    {
        public override string id { get; set; }
        public string name { get; set; }
        public bool is_organization { get; set; }
        public override string endpoint { get { return "workspaces"; } }

        //public T Retreive<T>(string workspaceId,Authentication authentication) where T : IAsanaObject<string>
        //{
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //    using (var cli = new HttpClient())
        //    {
        //        cli.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authentication.AccessToken);
        //        var uri = string.Format("https://app.asana.com/api/1.0/{0}/{1}", endpoint, workspaceId);

        //        var strrsp = cli.GetStringAsync(uri).Result;
        //        var j = JObject.Parse(strrsp);

        //        var jobj = j.First.First.ToObject<T>();

        //        return jobj;
        //    }
        //}
    }
}