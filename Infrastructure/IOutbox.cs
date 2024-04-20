using Domain;

namespace Infrastructure
{
    public interface IOutbox
    {
        void OutboxEvents(List<IDomainEvent> uncommittedEvents);
    }
}