using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Infrastructure
{
    public class SerlaizationStradegy
    {
        private static readonly IContractResolver _contractResolver = new CamelCasePropertyNamesContractResolver();

        public static IContractResolver ContractResolver => _contractResolver;

        public static string SerializeObject(object objectToJsonify )
        {
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = _contractResolver;
            return JsonConvert.SerializeObject(objectToJsonify, camelCaseFormatter);
        }
        public static T DeserializeObject<T>(string json)
        {
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = _contractResolver;
            return JsonConvert.DeserializeObject<T>(json, camelCaseFormatter);
        }
    }
}
