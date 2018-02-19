using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Etosha.Server.Common.Utils
{
    public static class JsonUtils
    {
        private static readonly JsonSerializerSettings settings = new JsonSerializerSettings
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public static string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value, settings);
        }
    }
}
