using System.Text.Json;
using System.Text.Json.Serialization;
using YourEcommerce.DTOs.GenderDtos;

public class GenderListConverter : JsonConverter<List<GenderUpdateDto>>
{
    public override List<GenderUpdateDto>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var result = new List<GenderUpdateDto>();

        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
                break;

            if (reader.TokenType == JsonTokenType.Number)
            {
                int id = reader.GetInt32();
                result.Add(new GenderUpdateDto { Id = id });
            }
            else if (reader.TokenType == JsonTokenType.StartObject)
            {
                var gender = JsonSerializer.Deserialize<GenderUpdateDto>(ref reader, options);
                if (gender != null)
                    result.Add(gender);
            }
        }

        return result;
    }

    public override void Write(Utf8JsonWriter writer, List<GenderUpdateDto> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        foreach (var gender in value)
        {
            if (!string.IsNullOrEmpty(gender.Name) || !string.IsNullOrEmpty(gender.Description) || gender.GenderImage != null)
            {
                JsonSerializer.Serialize(writer, gender, options);
            }
            else
            {
                writer.WriteNumberValue(gender.Id);
            }
        }
        writer.WriteEndArray();
    }
}