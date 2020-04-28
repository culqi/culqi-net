using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi.Services.Interfaces
{
    public interface IAnyOf
    {
        object Value { get; }
        Type Type { get; }
    }
}
