using Culqi.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi.Entities
{
    public class ExpandableField<T> : IExpandableField<T> where T : IHasId
    {
        private string id;
        public string Id
        {
            get => ExpandedObject?.Id ?? id;
            set
            {
                if (ExpandedObject != null)
                {
                    throw new InvalidOperationException("Cannot set Id when ExpandedObject is already set.");
                }

                id = value;
            }
        }

        public T ExpandedObject { get; set; }

        object IExpandableField.ExpandedObject
        {
            get => ExpandedObject;
            set => ExpandedObject = (T)value;
        }

        bool IExpandableField.IsExpanded => ExpandedObject != null;
    }
}
