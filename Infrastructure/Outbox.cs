using Domain;

namespace Infrastructure
{
    public class Outbox : IOutbox
    {
        private List<object> _outbox = new List<object>();

        public void OutboxEvents(List<IDomainEvent> uncommittedEvents)
        {
            _outbox.AddRange(uncommittedEvents);
            uncommittedEvents.Clear();
        }
    }
}
