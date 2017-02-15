using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Seq.App.Asana
{
    class IdOnlyConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }
        public override bool CanRead { get { return false; } }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value);

            if (t.Type == JTokenType.Array)
            {
                var col = (IEnumerable<object>)value;
                var strCol = col.Select(c => c.ToString());

                writer.WriteValue(string.Join(",", strCol));
            }
            else
            {
                writer.WriteValue(value.ToString());
            }

        }
    }
}
