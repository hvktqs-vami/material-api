using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi
{
    public static class JsonHelper
    {
        public static Lazy<JsonSerializerSettings> Tolerant = new Lazy<JsonSerializerSettings>(InitTolerantSetting, true);
        public static Lazy<JsonSerializerSettings> Strict = new Lazy<JsonSerializerSettings>(InitStrictSetting, true);

        public const bool UseStrictAsDefault = false;

        private static JsonSerializerSettings InitTolerantSetting()
        {
            var setting = new JsonSerializerSettings();
            setting.NullValueHandling = NullValueHandling.Ignore;
            setting.MissingMemberHandling = MissingMemberHandling.Ignore;
            setting.TypeNameHandling = TypeNameHandling.None;
            setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            setting.PreserveReferencesHandling = PreserveReferencesHandling.None;
            setting.DefaultValueHandling = DefaultValueHandling.Ignore;

            return setting;
        }

        private static JsonSerializerSettings InitStrictSetting()
        {
            var setting = new JsonSerializerSettings();
            setting.NullValueHandling = NullValueHandling.Ignore;
            setting.MissingMemberHandling = MissingMemberHandling.Error;
            setting.TypeNameHandling = TypeNameHandling.None;
            setting.ReferenceLoopHandling = ReferenceLoopHandling.Error;
            setting.PreserveReferencesHandling = PreserveReferencesHandling.None;
       
            return setting;
        }

        public static void ConfigApiSetting(this JsonSerializerSettings setting)
        {
            setting.NullValueHandling = NullValueHandling.Ignore;
            setting.MissingMemberHandling = MissingMemberHandling.Ignore;
            setting.TypeNameHandling = TypeNameHandling.None;
            setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            setting.PreserveReferencesHandling = PreserveReferencesHandling.None;
            setting.DefaultValueHandling = DefaultValueHandling.Ignore;
        }

        /// <summary>
        ///   Converts an object to its JSON representation (extension method for Stringify)</summary>
        /// <param name="value">
        ///   Object</param>
        /// <returns>
        ///   JSON representation string.</returns>
        /// <remarks>
        ///   null, Int32, Boolean, DateTime, Decimal, Double, Guid types handled automatically.
        ///   If object has a ToJson method it is used, otherwise value.ToString() is used as last fallback.</remarks>
        public static string ToJson(this object value, bool strict = UseStrictAsDefault, bool format = false)
        {
            return Stringify(value, strict, format);
        }

        public static string Stringify(object value, bool strict = UseStrictAsDefault, bool format = false)
        {
            var settings = strict ? Strict : Tolerant;

            if (format)
            {
                return JsonConvert.SerializeObject(value, Formatting.Indented, settings.Value);
            }
            else
            {
                return JsonConvert.SerializeObject(value, settings.Value);
            }
        }

        public static T Parse<T>(string input, bool strict = UseStrictAsDefault)
        {
            var settings = strict ? Strict : Tolerant;
            return JsonConvert.DeserializeObject<T>(input, settings.Value);
        }

        public static object Parse(string input, Type targetType, bool strict = UseStrictAsDefault)
        {
            var settings = strict ? Strict : Tolerant;
            return JsonConvert.DeserializeObject(input, targetType, settings.Value);
        }

        public static object ParseFromToken(this JToken obj, Type type, JsonSerializerSettings setting)
        {
            var serializer = JsonSerializer.Create(setting);
            return obj.ToObject(type, serializer);
        }

        public static T ParseFromToken<T>(this JToken obj, JsonSerializerSettings setting)
        {
            var serializer = JsonSerializer.Create(setting);
            return obj.ToObject<T>(serializer);
        }

        public static T ParseFromToken<T>(this JToken obj, bool strict = UseStrictAsDefault)
        {
            var setting = strict ? Strict : Tolerant;
            return ParseFromToken<T>(obj, setting.Value);
        }

        public static object ParseFromToken(this JToken obj, Type type, bool strict = UseStrictAsDefault)
        {
            var setting = strict ? Strict : Tolerant;
            return ParseFromToken(obj, type, setting.Value);
        }

    }
}
