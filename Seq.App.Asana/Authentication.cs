using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seq.App.Asana
{
    public class Authentication
    {
        public readonly string AccessToken;// { get; set}

        public Authentication(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }
    }
}
