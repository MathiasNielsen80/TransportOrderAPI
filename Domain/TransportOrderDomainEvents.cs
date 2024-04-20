
namespace Domain
{
    public record TransportOrderPlaced(Guid Id, DateTimeOffset OccurredAt): IDomainEvent;    

    public record TransportOrderStarted(Guid Id, DateTimeOffset OccurredAt): IDomainEvent;

    public record TransportOrderCompleted(Guid Id, DateTimeOffset OccurredAt): IDomainEvent;
}