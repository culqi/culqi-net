using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi.Entities.Interfaces
{
    public interface IHasMetadata
    {
        Dictionary<string, string> Metadata { get; set; }
    }
}
