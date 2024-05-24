using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ClassLibrary1
{
    public class Student
    {
        public int Id { get; set; }

        // numele proprietatii care o deserializam difera de cea din model, de aceea specificam explicit din ce proprietate din json vrem sa preluam informatia
        [JsonProperty("wrong_name")]
        public string? WrongName { get; set; }

        [JsonProperty("first_name")]
        public string? FirstName { get; set; }
        [JsonProperty("last_name")]

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Gender { get; set; }

        [JsonProperty("ip_address")]
        public string? IpAddress { get; set; }

        [JsonProperty("data_nasterii")]
        [JsonConverter(typeof(CustomDateTimeConverter), "M/d/yyyy")]

        public DateTime DateOfBirth { get; set; }

        public string? Role { get; set; }

        [JsonProperty("adress")]
        public Address? Address { get; set; }

        [JsonProperty("dog")]
        public string? Dog { get; set; }

        [JsonProperty("a_names")]
        public string? AName { get; set; }
    }

    public class Address
    {
        public string? Street { get; set; }

        public string? Number { get; set; }

        public string? City { get; set; }
    }

    // clasa care ne ajuta la deserializarea corecta a datelor
    class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter(string format)
        {
            base.DateTimeFormat = format;
        }
    }
}
