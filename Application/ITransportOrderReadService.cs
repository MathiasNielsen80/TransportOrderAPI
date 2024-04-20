using TransportOrderAPI.DTOs;

namespace Application
{
    public interface ITransportOrderReadService
    {
        Task<TransportOrderDTO?> GetTransportOrderByIdAsync(Guid id);
        
        Task<IEnumerable<TransportOrderDTO>> GetTransportOrdersAsync();
    }
}