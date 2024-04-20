using Domain;

namespace Infrastructure
{
    public interface ITransportOrderRepository
    {
        Task<TransportOrder> GetTransportOrderByIdAsync(Guid Id);

        Task<IEnumerable<TransportOrder>> GetTransportOrdersAsync();

        Task AddTransportOrderAsync(TransportOrder transportOrder);

        Task UpdateTransportOrderAsync(TransportOrder transportOrder);
    }
}