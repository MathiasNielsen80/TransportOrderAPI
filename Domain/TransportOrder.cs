namespace Domain
{
    public class TransportOrder
    {
        public List<IDomainEvent> UncommittedEvents = new();

        public Guid Id { get; set; } 

        public DateTime DateCreated { get; set; }

        public DateTime? DateStarted { get; set; }

        public DateTime? DateCompleted { get; set; }

        public OrderState State { get; set; }
             
        public enum OrderState
        {
            New,
            InProgress,
            Completed
        }

        public static TransportOrder PlaceOrder(PlaceTransportOrder cmd)
        {
            var transportOrder = new TransportOrder
            {
                Id = cmd.Id,
                DateCreated = DateTime.UtcNow,
                State = OrderState.New
            };

            transportOrder.UncommittedEvents.Add(new TransportOrderPlaced(transportOrder.Id, DateTimeOffset.UtcNow));

            return transportOrder;
        }

        public void StartOrder()
        {
            if (State == OrderState.New)
            {
                State = OrderState.InProgress;
                DateStarted = DateTime.UtcNow;
            }
            else
            {
                throw new InvalidOperationException("Order is not in New state");
            }

            UncommittedEvents.Add(new TransportOrderStarted(Id, DateTimeOffset.UtcNow));
        }

        public void CompleteOrder()
        {
            if (State == OrderState.InProgress)
            {
                State = OrderState.Completed;
                DateCompleted = DateTime.UtcNow;
            }
            else
            {
                throw new InvalidOperationException("Order is not in InProgress state");
            }

            UncommittedEvents.Add(new TransportOrderCompleted(Id, DateTimeOffset.UtcNow));
        }
    }
}
