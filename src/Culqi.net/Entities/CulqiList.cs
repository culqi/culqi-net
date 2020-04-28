using Culqi.Entities.Base;
using Culqi.Entities.Interfaces;
using Culqi.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Culqi.Entities
{
    [JsonObject]
    public class CulqiList<T> : CulqiEntity<CulqiList<T>>, IEnumerable<T>
    {
        [JsonProperty("data", ItemConverterType = typeof(CulqiObjectConverter))]
        public List<T> Data { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        public void Reverse()
        {
            this.Data.Reverse();
        }
    }
}
