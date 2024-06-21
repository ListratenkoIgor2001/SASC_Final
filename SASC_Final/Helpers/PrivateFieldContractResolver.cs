using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;

namespace SASC_Final.Helpers
{
    public class PrivateFieldContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var props = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Select(f => base.CreateProperty(f, memberSerialization))
                .Union(type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Select(p => base.CreateProperty(p, memberSerialization)))
                .ToList();

            foreach (var prop in props)
            {
                prop.Writable = true;
                prop.Readable = true;
            }

            return props;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (member is FieldInfo)
            {
                FieldInfo field = (FieldInfo)member;
                property.PropertyName = field.Name;
                property.Writable = true;
                property.Readable = true;
            }
            else if (member is PropertyInfo)
            {
                PropertyInfo propertyInfo = (PropertyInfo)member;
                property.PropertyName = propertyInfo.Name;
                property.Writable = propertyInfo.CanWrite;
                property.Readable = propertyInfo.CanRead;
            }

            return property;
        }
    }

    public static partial class Extentions
    {
        public static JsonSerializerSettings GetPrivateSerializer()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new PrivateFieldContractResolver(),
                Formatting = Formatting.Indented
            };
            return settings;
        }
    }
}
