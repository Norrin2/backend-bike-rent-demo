namespace BikeRent.Infra.Interfaces
{
    public interface IMessageService
    {
        void PublishMessages(IEnumerable<string> messages);
    }
}