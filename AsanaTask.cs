using System;
using System.Collections.Generic;

namespace Seq.App.Asana
{
    public class AsanaTask : AsanaBaseObject<string>
    {
        public override string id { get; set; }
        public AsanaUser assignee { get; set; }
        public string name { get; set; }
        public string notes { get; set; }
        public IEnumerable<AsanaProject> projects { get; set; }
        public AsanaWorkspace workspace { get; set; }

        public override string endpoint { get { return "tasks"; } }
    }
}
