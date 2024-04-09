using BikeRent.Domain.Entities;
using Flunt.Notifications;
using Flunt.Validations;
using System.Text.Json.Serialization;

namespace BikeRent.Domain.ValueObject
{
    public class Rent: Notifiable<Notification>
    {
        public Bike Bike { get; private set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RentPlan Plan { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        public Rent(Bike bike, RentPlan plan)
        {
            AddNotifications(new Contract<Rent>()
                .IsNotNull(bike, nameof(Bike), "Bike must be informed")
                .IsNotNull(plan, nameof(RentPlan), "Rent plan must be Days7, Days15 or Days30")
                .Notifications);

            Bike = bike;
            Plan = plan;
            StartDate = DateTime.Now.AddDays(1);
        }

        public decimal FinishRentAndGetCost(DateTime returnDate)
        {
            decimal dailyRate = (int)Plan;
            EndDate = GetEndDate();
            if (returnDate < EndDate)
            {
                int extraDays = (EndDate.Value - returnDate).Days;

                decimal extraDaysCost = extraDays * dailyRate * GetPenaltyRate();

                decimal totalCost = ((EndDate.Value - StartDate).Days - extraDays) * dailyRate + extraDaysCost;
                return totalCost;
            }
            else if (returnDate > EndDate)
            {
                int extraDays = (returnDate - EndDate.Value).Days;

                decimal totalCost = (EndDate.Value - StartDate).Days * dailyRate + extraDays * 50m;
                return totalCost;
            }

            return (returnDate - StartDate).Days * dailyRate;
        }

        private decimal GetPenaltyRate()
        {
            decimal penaltyRate = 0m;
            switch (Plan)
            {
                case RentPlan.Days7:
                    penaltyRate = 0.2m;
                    break;
                case RentPlan.Days15:
                    penaltyRate = 0.4m;
                    break;
                case RentPlan.Days30:
                    penaltyRate = 0.6m;
                    break;
            }
            return penaltyRate;
        }

        private DateTime GetEndDate()
        {
            DateTime returnDate;
            switch (Plan)
            {
                case RentPlan.Days7:
                    returnDate = StartDate.AddDays(7);
                    break;
                case RentPlan.Days15:
                    returnDate = StartDate.AddDays(15);
                    break;
                default:
                    returnDate = StartDate.AddDays(30);
                    break;
            }

            return returnDate;
        }
    }
}
