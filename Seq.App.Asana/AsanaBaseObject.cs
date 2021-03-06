﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Seq.App.Asana;
using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;

namespace Seq.App.Asana
{
    public abstract class AsanaBaseObject<TId> : IAsanaObject<TId>
    {
        public AsanaBaseObject()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            JsonParse = (js,t) =>
            {
                var j = JObject.Parse(js);
                return j.First.First.ToObject(t);
            };
        }
        [JsonIgnore]
        public abstract string endpoint { get; }
        public abstract TId id { get; set; }
        public virtual string name { get; set; }
        [JsonIgnore]
        public virtual Func<string,Type,object> JsonParse { get; set; }

        public static T Retreive<T>(TId id, Authentication authentication)
            where T : AsanaBaseObject<TId>, IAsanaObject<TId>, new()
        {
            var asanaObject = new T();
            using (var cli = new HttpClient())
            {
                cli.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authentication.AccessToken);
                var uri = string.Format("https://app.asana.com/api/1.0/{0}/{1}", asanaObject.endpoint, id);

                var responseStr = cli.GetStringAsync(uri).Result;

                var jsonObj = (T)asanaObject.JsonParse(responseStr, typeof(T));

                return jsonObj;
            }
        }
        public static AsanaBaseObject<TId> Retreive(Type ResourceType, TId id, Authentication authentication)
        {
            var methodResult = ResourceType
                .GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(m => m.Name == nameof(Retreive))
                .Single(mt => mt.IsGenericMethod)
                .MakeGenericMethod(ResourceType)
                .Invoke(null, new object[] { id, authentication });
            return methodResult as AsanaBaseObject<TId>;
        }

        public void Create(Authentication authentication)
        {
            using (var cli = new HttpClient())
            {
                cli.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authentication.AccessToken);

                var uri = string.Format("https://app.asana.com/api/1.0/{0}", endpoint);

                //create object for serielization
                var obj = new { data = this };

                var jobj = JObject.FromObject(obj, new JsonSerializer { NullValueHandling = NullValueHandling.Ignore });

                var objStr = jobj.ToString();


                var content = new StringContent(objStr);
                var response = cli.PostAsync(uri, content).Result;

                if (!response.IsSuccessStatusCode)
                    throw new WebException("Not success");
            }
        }
    }
}
