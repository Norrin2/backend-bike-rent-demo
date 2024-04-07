using BikeRent.Domain.ValueObject;
using System.Text.Json.Serialization;

namespace BikeRent.Publisher.ViewModel
{
    public class CnhViewModel
    {
        public string Number { get;  set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CnhType CnhType { get;  set; }
        public string? CnhUrl { get;  set; }
    }
}
