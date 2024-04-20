using Infrastructure;
using TransportOrderAPI.DTOs;

namespace Application
{
    public class TransportOrderReadService : ITransportOrderReadService
    {
        private ITransportOrderRepository _transportOrderRepository;

        public TransportOrderReadService(ITransportOrderRepository transportOrderRepository)
        {
            _transportOrderRepository = transportOrderRepository;
        }

        public async Task<IEnumerable<TransportOrderDTO>> GetTransportOrdersAsync()
        {
            var transportOrders = await _transportOrderRepository.GetTransportOrdersAsync();
            return transportOrders.Select(transportOrder => new TransportOrderDTO
            {
                Id = transportOrder.Id,
                Date = transportOrder.DateCreated,
                State = transportOrder.State.ToString()
            });
        }

        public async Task<TransportOrderDTO?> GetTransportOrderByIdAsync(Guid id)
        {
            var transportOrder = await _transportOrderRepository.GetTransportOrderByIdAsync(id);

            if (transportOrder == null)
                return null;

            return new TransportOrderDTO
            {
                Id = transportOrder.Id,
                Date = transportOrder.DateCreated,
                State = transportOrder.State.ToString()
            };
        }
    }
}
