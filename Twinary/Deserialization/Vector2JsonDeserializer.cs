using System;
using System.Numerics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Twinary.Deserialization
{
    public class Vector2JsonDeserializer : JsonConverter
    {
        #region Json Converter Implementation

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Vector2);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            JObject jsonObject = JObject.Load(reader);
            Vector2 vector2 = existingValue is Vector2 ? (Vector2)existingValue : new Vector2();

            vector2.X = jsonObject["x"].RemoveFromLowestPossibleParent().Value<float>();
            vector2.Y = jsonObject["y"].RemoveFromLowestPossibleParent().Value<float>();
            
            return vector2;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
