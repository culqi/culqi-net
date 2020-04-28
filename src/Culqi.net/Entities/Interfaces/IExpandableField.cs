using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi.Entities.Interfaces
{
    public interface IExpandableField
    {
        string Id { get; set; }
        object ExpandedObject { get; set; }
        bool IsExpanded { get; }
    }

    public interface IExpandableField<T> : IExpandableField
        where T : IHasId
    {
        new T ExpandedObject { get; set; }
    }
}

