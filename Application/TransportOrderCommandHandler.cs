using Domain;
using Infrastructure;
using static Domain.TransportOrder;

namespace Application
{
    public class TransportOrderCommandHandler : ITransportOrderCommandHandler
    {
        private ITransportOrderRepository _transportOrderRepository;

        public TransportOrderCommandHandler(ITransportOrderRepository transportOrderRepository)
        {
            _transportOrderRepository = transportOrderRepository;
        }

        public async Task Handle(PlaceTransportOrder cmd)
        {
            var order = PlaceOrder(cmd);
            await _transportOrderRepository.AddTransportOrderAsync(order);
        }

        public async Task Handle(StartTransportOrder cmd)
        {
            var transportOrder = await _transportOrderRepository.GetTransportOrderByIdAsync(cmd.Id);

            if (transportOrder == null)
                throw new ArgumentException("Transport order not found");

            transportOrder.StartOrder();

            await _transportOrderRepository.UpdateTransportOrderAsync(transportOrder);
        }

        public async Task Handle(CompleteTransportOrder cmd)
        {
            var transportOrder = await _transportOrderRepository.GetTransportOrderByIdAsync(cmd.Id);

            if (transportOrder == null)
                throw new ArgumentException("Transport order not found");

            transportOrder.CompleteOrder();

            await _transportOrderRepository.UpdateTransportOrderAsync(transportOrder);
        }
    }
}
