namespace Domain
{
    public interface IDomainEvent
    {
        public DateTimeOffset OccurredAt { get; }
    }
}
