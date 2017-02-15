using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System;

namespace Seq.App.Asana
{
    public class AsanaWorkspace : AsanaBaseObject<string>
    {
        public override string id { get; set; }
        public string name { get; set; }
        public bool is_organization { get; set; }
        public override string endpoint { get { return "workspaces"; } }
        public override string ToString()
        {
            return id;
        }
    }
}