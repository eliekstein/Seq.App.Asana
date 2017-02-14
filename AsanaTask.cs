using System;
using System.Collections.Generic;

namespace Seq.App.Asana
{
    public class AsanaTask
    {
        public Guid id { get; set; }
        public string asignee { get; set; }
        public string name { get; set; }
        public IEnumerable<AsanaProject> projects { get; set; }
        public AsanaWorkspace workspace { get; set; }

    }
}
