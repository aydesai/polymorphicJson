using System.Text.Json;
using System.Text.Json.Serialization;

namespace PolymorphicJson
{
    public class TriggerDetailsConverter : JsonConverter<TriggerDetails>
    {
        enum TypeDiscriminator
        {
            UploadBegin = 1,
            UploadComplete = 2
        }

        public override bool CanConvert(Type typeToConvert) =>
            typeof(TriggerDetails).IsAssignableFrom(typeToConvert);

        public override TriggerDetails? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var readerClone = reader;
            if (readerClone.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }
            readerClone.Read();
            if (readerClone.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }
            var propName = readerClone.GetString();
            if (propName != "TypeDiscriminator")
            {
                throw new JsonException();
            }
            readerClone.Read();
            if (readerClone.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }
            var typeDiscriminator = (TypeDiscriminator) readerClone.GetInt32();
            TriggerDetails triggerDetails = typeDiscriminator switch
            {
                TypeDiscriminator.UploadBegin => JsonSerializer.Deserialize<UploadBegin>(ref reader)!,
                TypeDiscriminator.UploadComplete => JsonSerializer.Deserialize<UploadComplete>(ref reader)!,
                _ => throw new JsonException()
            };
            return triggerDetails;
        }

        public override void Write(Utf8JsonWriter writer, TriggerDetails value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            if (value is UploadBegin uploadBegin)
            {
                writer.WriteNumber("TypeDiscriminator", (int) TypeDiscriminator.UploadBegin);
                writer.WriteNumber("Id", uploadBegin.Id);
                writer.WriteString("Name", uploadBegin.Name);
                writer.WriteString("FileName", uploadBegin.FileName);
                writer.WriteString("TimeOfBeginArrival", uploadBegin.TimeOfBeginArrival);
            }
            else if (value is UploadComplete uploadComplete)
            {
                writer.WriteNumber("typeDiscriminator", (int) TypeDiscriminator.UploadComplete);
                writer.WriteNumber("Id", uploadComplete.Id);
                writer.WriteString("Name", uploadComplete.Name);
                writer.WriteNumber("UploadBeginId", uploadComplete.UploadBeginId);
                writer.WriteString("TimeOfFinishArrival", uploadComplete.TimeOfFinishArrival);
            }
            writer.WriteEndObject();
        }
    }
}