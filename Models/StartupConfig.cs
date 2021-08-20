namespace Rokono_Control.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class StartupConfig
    {
        [JsonProperty("Logging")]
        public Logging Logging { get; set; }

        [JsonProperty("ConnectionStrings")]
        public ConnectionStrings ConnectionStrings { get; set; }

        [JsonProperty("EmailConfiguration")]
        public EmailConfiguration EmailConfiguration { get; set; }
    }

    public partial class ConnectionStrings
    {
        [JsonProperty("RokonocontrolContext")]
        public string RokonocontrolContext { get; set; }
    }

    public partial class EmailConfiguration
    {
        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("SMTP")]
        public string Smtp { get; set; }

        [JsonProperty("SmtpPort")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long SmtpPort { get; set; }

        [JsonProperty("IMAP")]
        public string Imap { get; set; }

        [JsonProperty("ImapPort")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ImapPort { get; set; }

        [JsonProperty("CompanyName")]
        public string CompanyName { get; set; }
    }

    public partial class Logging
    {
        [JsonProperty("LogLevel")]
        public LogLevel LogLevel { get; set; }
    }

    public partial class LogLevel
    {
        [JsonProperty("Default")]
        public string Default { get; set; }

        [JsonProperty("System")]
        public string System { get; set; }

        [JsonProperty("Microsoft")]
        public string Microsoft { get; set; }
    }

    public partial class StartupConfig
    {
        public static StartupConfig FromJson(string json) => JsonConvert.DeserializeObject<StartupConfig>(json,  Rokono_Control.Models.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this StartupConfig self) => JsonConvert.SerializeObject(self, Rokono_Control.Models.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
