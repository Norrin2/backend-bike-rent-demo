using Flunt.Validations;

namespace BikeRent.Domain
{
    public class Bike: Entity
    {
        public string LicensePlate { private get; set; }
        public string ModelName { private get; set; }
        public int Year { private get; set; }

        public Bike(string licensePlate, string modelName, int year) : base()
        {
            AddNotifications(new Contract<Bike>()
                .IsNotNullOrEmpty(LicensePlate, nameof(LicensePlate), "License plate must not be null")
                .IsNotNullOrEmpty(ModelName, nameof(ModelName), "Model name must not be null")
            );

            LicensePlate = licensePlate;
            ModelName = modelName;
            Year = year;
        }
    }
}
