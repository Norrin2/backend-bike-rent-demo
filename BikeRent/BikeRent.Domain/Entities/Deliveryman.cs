using BikeRent.Domain.ValueObject;
using Flunt.Validations;

namespace BikeRent.Domain.Entities
{
    public class Deliveryman : Entity
    {
        public string Cnpj { get;  set; }
        public Cnh Cnh { get; set; }

        public Deliveryman(string cnpj)
        {
            AddNotifications(new Contract<Deliveryman>()
               .IsNotNullOrEmpty(cnpj, nameof(Cnpj), "CNPJ must not be null"));

            Cnpj = cnpj;
        }
        public void AssignCnh(Cnh cnh)
        {
            AddNotifications(Cnh);
            Cnh = cnh;
        }

        public void UpdateCnhPhotoUrl(string cnhUrl)
        {
            Cnh.UpdateCnhPhotoUrl(cnhUrl);

            AddNotifications(Cnh);
        }
    }
}
