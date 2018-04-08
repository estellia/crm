using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jayrock.Json.Conversion;
using System.Xml.Serialization;
using System.Xml;
using Jayrock.Json;
using System.IO;

namespace cPos.Dex.Common
{
    public static class Json
    {
        public static string ToJson(this string[] array)
        {
            return JsonConvert.ExportToString(array);
        }

        public static T Import<T>(string text)
        {
            return JsonConvert.Import<T>(text);
        }

        public static string ToJsonFromXml(XmlDocument xml)
        {
            return string.Empty;
        }

        public static JsonWriter CreateJsonWriter()
        {
            JsonTextWriter jsonWriter = new JsonTextWriter();
            jsonWriter.PrettyPrint = true;
            return jsonWriter;
        }

        public static string GetJsonString(object obj)
        {
            Jayrock.Json.JsonTextWriter writer = new Jayrock.Json.JsonTextWriter();
            Jayrock.Json.Conversion.JsonConvert.Export(obj, writer);
            return writer.ToString();
        }
    }
}
