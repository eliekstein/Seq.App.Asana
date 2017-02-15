using System;
using System.Collections.Generic;
using System.Linq;

namespace Seq.App.Asana
{
    public interface IAsanaObject<TId>
    {
        TId id { get; set; }
        string endpoint { get; }

        T Retreive<T>(TId id,Authentication authentication) where T : IAsanaObject<TId>;

    }
}
