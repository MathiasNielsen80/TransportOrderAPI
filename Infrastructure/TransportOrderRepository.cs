using Domain;
using static Domain.TransportOrder;

namespace Infrastructure
{
    public class TransportOrderRepository : ITransportOrderRepository
    {
        public TransportOrderRepository(IOutbox outbox)
        {
            _outbox = outbox;
        }

        private readonly IOutbox _outbox;

        // This is a fake repository for demonstration purposes with seeded data
        private static List<TransportOrder> _transportOrders = new List<TransportOrder>
        {
            new() { Id = Guid.NewGuid(), DateCreated = DateTime.Now, State = OrderState.New },
            new() { Id = Guid.NewGuid(), DateCreated = DateTime.Now, State = OrderState.InProgress },
            new() { Id = Guid.NewGuid(), DateCreated = DateTime.Now, State = OrderState.Completed }
        };        

        public async Task<IEnumerable<TransportOrder>> GetTransportOrdersAsync()
        {
            return await Task.FromResult(_transportOrders);
        }

        public async Task<TransportOrder> GetTransportOrderByIdAsync(Guid Id)
        {
            return await Task.FromResult(_transportOrders.FirstOrDefault(x => x.Id == Id));
        }

        public async Task AddTransportOrderAsync(TransportOrder transportOrder)
        {
            // The following should be in a transaction to ensure consistency
            _transportOrders.Add(transportOrder);
            _outbox.OutboxEvents(transportOrder.UncommittedEvents);
            await Task.CompletedTask;
        }

        public async Task UpdateTransportOrderAsync(TransportOrder transportOrder)
        {
            _outbox.OutboxEvents(transportOrder.UncommittedEvents);
            await Task.CompletedTask;
        }
    }
}
