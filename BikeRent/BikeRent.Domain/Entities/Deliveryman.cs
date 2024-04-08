using BikeRent.Domain.ValueObject;
using Flunt.Validations;

namespace BikeRent.Domain.Entities
{
    public class Deliveryman : Entity
    {
        public string Cnpj { get;  private set; }
        public Cnh Cnh { get; private set; }
        public IEnumerable<Rent> Rents { get; private set; }

        public Deliveryman(string cnpj, Cnh cnh)
        {
            AddNotifications(new Contract<Deliveryman>()
               .IsNotNullOrEmpty(cnpj, nameof(Cnpj), "CNPJ must not be null").Notifications);

            AddNotifications(cnh.Notifications);

            Cnpj = cnpj;
            Cnh = cnh;
            Rents = new List<Rent>();
        }

        public void UpdateCnhPhotoUrl(string cnhUrl)
        {
            Cnh.UpdateCnhPhotoUrl(cnhUrl);

            AddNotifications(Cnh.Notifications);
        }

        public void RentBike(Bike bike, RentPlan plan)
        {
            var Rent = new Rent(bike, plan);
            AddNotifications(Rent.Notifications);

            if (!IsValid)
                return;

            if (Rents == null) Rents = new List<Rent>();
            Rents = Rents.Append(Rent);
        }

        public decimal FinishRentAndGetCost(Bike bike, DateTime returnDate)
        {
            var rent = Rents.FirstOrDefault(rent => rent.Bike?.Id == bike.Id && 
                                                    rent.EndDate == null);

            if (rent == null)
            {
                AddNotification(nameof(Rent), "Deliveryman is not renting this bike");
                return 0;
            }

            return rent.FinishRentAndGetCost(returnDate);
        }
    }
}
