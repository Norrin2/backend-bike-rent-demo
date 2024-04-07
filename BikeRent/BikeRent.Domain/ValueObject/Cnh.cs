using BikeRent.Domain.Entities;
using Flunt.Notifications;
using Flunt.Validations;
using System.Text.Json.Serialization;

namespace BikeRent.Domain.ValueObject
{
    public class Cnh: Notifiable<Notification>
    {
        public string Number { get; private set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CnhType CnhType { get; private set; }
        public string CnhUrl { get; private set; }
        public Cnh(string number, CnhType cnhType, string cnhUrl)
        {
            AddNotifications(new Contract<Bike>()
               .IsNotNullOrEmpty(number, nameof(Number), "Cnh number must not be null")
               .IsNotNull(cnhType, nameof(CnhType), "Cnh type must be A, B or AB"));

            Number = number;
            CnhType = cnhType;
            CnhUrl = cnhUrl;
        }

        public void UpdateCnhPhotoUrl(string cnhUrl)
        {
            AddNotifications(new Contract<Cnh>()
                .IsNotNullOrEmpty(cnhUrl, nameof(Cnh), "Cnh url must not be null"));

            CnhUrl = cnhUrl;
        }
    }
}
