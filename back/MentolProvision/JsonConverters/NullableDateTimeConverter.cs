using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MentolProvision.JsonConverters
{
	public class NullableDateTimeConverter: JsonConverter<DateTime?>
	{
		public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var str = reader.GetString();

			if (string.IsNullOrEmpty(str))
				return null;

			return DateTime.Parse(str, CultureInfo.InvariantCulture);
		}

		public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value?.ToString(CultureInfo.InvariantCulture));
		}
	}
}
