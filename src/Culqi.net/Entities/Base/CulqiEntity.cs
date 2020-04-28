using Culqi.Entities.Interfaces;
using Culqi.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Culqi.Entities.Base
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class CulqiEntity : ICulqiEntity
    {
        [JsonIgnore]
        private JObject rawJObject;

        [JsonIgnore]
        protected JObject RawJObject
        {
            get
            {
                if (rawJObject == null)
                {
                    if (CulqiResponse == null)
                    {
                        return null;
                    }

                    rawJObject = JObject.Parse(this.CulqiResponse.Content);
                }

                return rawJObject;
            }

            set
            {
                rawJObject = value;
            }
        }


        [JsonIgnore]
        public CulqiResponse CulqiResponse { get; set; }

        public static IHasObject FromJson(string value)
        {
            return JsonUtils.DeserializeObject<IHasObject>(value, CulqiConfiguration.SerializerSettings);
        }

        internal static T FromJson<T>(string value) where T : ICulqiEntity
        {
            return JsonUtils.DeserializeObject<T>(value, CulqiConfiguration.SerializerSettings);
        }

        public override string ToString()
        {
            return string.Format("<{0}@{1} id={2}> JSON: {3}", GetType().FullName, RuntimeHelpers.GetHashCode(this), GetIdString(), ToJson());
        }

        public string ToJson()
        {
            return JsonUtils.SerializeObject(this, Formatting.Indented, CulqiConfiguration.SerializerSettings);
        }

        protected static ExpandableField<T> SetExpandableFieldId<T>(string id, ExpandableField<T> expandable) where T : IHasId
        {
            if (expandable == null)
            {
                expandable = new ExpandableField<T>();
                expandable.Id = id;
            }
            else if (expandable.Id != id)
            {
                expandable.ExpandedObject = default;
                expandable.Id = id;
            }

            return expandable;
        }

        protected static ExpandableField<T> SetExpandableFieldObject<T>(T obj, ExpandableField<T> expandable) where T : IHasId
        {
            if (expandable == null)
            {
                expandable = new ExpandableField<T>();
            }

            expandable.ExpandedObject = obj;

            return expandable;
        }

        private object GetIdString()
        {
            foreach (var property in this.GetType().GetTypeInfo().DeclaredProperties)
            {
                if (property.Name == "Id")
                {
                    return property.GetValue(this);
                }
            }

            return null;
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Generic variant")]
    public abstract class CulqiEntity<T> : CulqiEntity where T : CulqiEntity<T>
    {
        public static new T FromJson(string value)
        {
            return FromJson<T>(value);
        }
    }
}
