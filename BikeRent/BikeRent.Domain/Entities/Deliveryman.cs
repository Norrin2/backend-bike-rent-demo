using BikeRent.Domain.ValueObject;
using Flunt.Validations;

namespace BikeRent.Domain.Entities
{
    public class Deliveryman : Entity
    {
        public string Cnpj { get;  private set; }
        public Cnh Cnh { get; private set; }

        public Deliveryman(string cnpj, Cnh cnh)
        {
            AddNotifications(new Contract<Deliveryman>()
               .IsNotNullOrEmpty(cnpj, nameof(Cnpj), "CNPJ must not be null"));

            AddNotifications(cnh);

            Cnpj = cnpj;
            Cnh = cnh;
        }

        public void UpdateCnhPhotoUrl(string cnhUrl)
        {
            Cnh.UpdateCnhPhotoUrl(cnhUrl);

            AddNotifications(Cnh);
        }
    }
}
