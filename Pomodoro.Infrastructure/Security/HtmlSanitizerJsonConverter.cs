using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace Pomodoro.Infrastructure.Security;

public sealed class HtmlSanitizerJsonConverter : JsonConverter<string>
{
    private readonly HtmlEncoder _customEncoder;

    public HtmlSanitizerJsonConverter()
    {
        _customEncoder = HtmlEncoder.Create(UnicodeRanges.All);
    }
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var rawValue = reader.GetString();
        if (string.IsNullOrWhiteSpace(rawValue))
            return rawValue;
        return _customEncoder.Encode(rawValue.Trim());
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}