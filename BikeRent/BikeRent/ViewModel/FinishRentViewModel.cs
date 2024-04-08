namespace BikeRent.Publisher.ViewModel
{
    public class FinishRentViewModel
    {
        public Guid BikeId { get; set; }
        public Guid DeliveryManId { get; set; }
        public DateTime EndDate { get; set; }
    }
}
