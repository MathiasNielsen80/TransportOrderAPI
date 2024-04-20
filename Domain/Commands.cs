namespace Domain
{
    public record PlaceTransportOrder(Guid Id);
    
    public record StartTransportOrder(Guid Id);

    public record CompleteTransportOrder(Guid Id);
}
