using BikeRent.Domain.ValueObject;
using System.Text.Json.Serialization;

namespace BikeRent.Publisher.ViewModel
{
    public class BikeRentViewModel
    {
        public Guid BikeId { get; set; }
        public Guid DeliveryManId { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RentPlan Plan { get; set; }
    }
}
