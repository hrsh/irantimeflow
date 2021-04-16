using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IranTimeFlow.BackService.Helpers
{
    public static class ClassProperties
    {
        public static Dictionary<string, FieldInfo> GetFields(Type obj) =>
            obj.GetFields().ToDictionary(f => f.Name);

        public static IEnumerable<string> Resolve(
            this Dictionary<string, FieldInfo> target,
            Func<KeyValuePair<string, FieldInfo>, string> keySelctor)
            => target.Select(keySelctor);

        public static Dictionary<string, PropertyInfo> GetProperties(Type obj) =>
            obj.GetProperties().ToDictionary(f => f.Name);
    }
}
