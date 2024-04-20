using Domain;

namespace Application
{
    public interface ITransportOrderCommandHandler
    {
        Task Handle(CompleteTransportOrder cmd);
        Task Handle(PlaceTransportOrder cmd);
        Task Handle(StartTransportOrder cmd);
    }
}