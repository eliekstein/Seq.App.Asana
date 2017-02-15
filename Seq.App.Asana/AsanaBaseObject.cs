using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Seq.App.Asana
{
    public abstract class AsanaBaseObject<TId> : IAsanaObject<TId>
    {
        public AsanaBaseObject()
        {
            JsonParse = (js,t) =>
            {
                var j = JObject.Parse(js);
                return j.First.First.ToObject(t);
            };
        }
        public abstract string endpoint { get; }
        public abstract TId id { get; set; }
        public virtual Func<string,Type,object> JsonParse { get; set; }

        public T Retreive<T>(TId id, Authentication authentication) where T : IAsanaObject<TId>
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (var cli = new HttpClient())
            {
                cli.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authentication.AccessToken);
                var uri = string.Format("https://app.asana.com/api/1.0/{0}/{1}", endpoint, id);

                var responseStr = cli.GetStringAsync(uri).Result;

                var jsonObj = (T)JsonParse(responseStr,typeof(T));

                return jsonObj;
            }
        }
    }
}
