using Flunt.Validations;

namespace BikeRent.Domain.Entities
{
    public class Bike : Entity
    {
        public string LicensePlate { get; private set; }
        public string ModelName { get; private set; }
        public int Year { get; private set; }

        public Bike(string licensePlate, string modelName, int year) : base()
        {
            AddNotifications(new Contract<Bike>()
               .IsNotNullOrEmpty(licensePlate, nameof(LicensePlate), "License plate must not be null")
               .IsNotNullOrEmpty(modelName, nameof(ModelName), "Model name must not be null"));

            LicensePlate = licensePlate;
            ModelName = modelName;
            Year = year;
        }

        public void UpdateLicensePlate(string newLicensePlate)
        {
            LicensePlate = newLicensePlate;
            AddNotifications(new Contract<Bike>()
                .IsNotNullOrEmpty(LicensePlate, nameof(LicensePlate), "License plate must not be null"));
        }
    }
}
