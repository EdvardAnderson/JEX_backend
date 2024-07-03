using System.Text.Json;
using System.Text.Json.Serialization;
using JEX_backend.API.Models;

namespace JEX_backend.API.Data
{
    public class CompanyDtoConverter : JsonConverter<CompanyDto>
    {
        public override CompanyDto Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            // Implement custom deserialization logic if needed.
            throw new NotImplementedException();
        }

        public override void Write(
            Utf8JsonWriter writer,
            CompanyDto value,
            JsonSerializerOptions options
        )
        {
            // Customize the serialization here.
            writer.WriteStartObject();
            writer.WriteString("Id", value.Id.ToString());
            writer.WriteString("Name", value.Name);
            writer.WriteString("Address", value.Address);
            writer.WriteStartArray("JobOpenings");
            foreach (var job in value.JobOpenings)
            {
                writer.WriteStartObject();
                writer.WriteString("Id", job.Id.ToString());
                writer.WriteString("Title", job.Title);
                writer.WriteString("Description", job.Description);
                writer.WriteBoolean("IsActive", job.IsActive);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }
}
