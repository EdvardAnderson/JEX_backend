using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class NoValuesJsonConverter : JsonConverter
{
    public override bool CanConvert (Type objectType)
    {
        return objectType.IsArray || objectType.GetInterfaces ().Any (i => i.IsGenericType && i.GetGenericTypeDefinition () == typeof (IEnumerable<>));
    }

    public override void WriteJson (JsonWriter writer, object value, JsonSerializer serializer)
    {
        var array = JArray.FromObject (value);
        array.WriteTo (writer);
    }

    public override object ReadJson (JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        throw new NotImplementedException ();
    }
}