using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seq.App.Asana
{
    public class AsanaUser : AsanaBaseObject<string>
    {
        public override string endpoint
        {
            get
            {
                return "users";
            }
        }

        public override string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }

    }
}
