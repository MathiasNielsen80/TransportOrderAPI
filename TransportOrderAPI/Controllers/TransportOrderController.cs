using Application;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace TransportOrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportOrderController : ControllerBase
    {
        private ITransportOrderReadService _transportOrderReadService;
        private ITransportOrderCommandHandler _transportOrderCommandHandler;

        public TransportOrderController(ITransportOrderReadService transportOrderReadService, ITransportOrderCommandHandler transportOrderCommandHandler)
        {
            _transportOrderReadService = transportOrderReadService;
            _transportOrderCommandHandler = transportOrderCommandHandler;
        }

        // GET: api/<TransportOrderController>
        [HttpGet]
        public async Task<IResult> GetAsync()
        {
            var transportOrders = await _transportOrderReadService.GetTransportOrdersAsync();
            return Results.Ok(transportOrders);
        }

        // GET api/<TransportOrderController>/3c81f576-e4ac-4506-8aab-20607a046b5b
        [HttpGet("{id}")]
        public async Task<IResult> GetAsync(Guid id)
        {
            var transportOrder = await _transportOrderReadService.GetTransportOrderByIdAsync(id);

            if (transportOrder == null)
                return Results.NotFound();

            return Results.Ok(transportOrder);
        }

        [HttpPost("PlaceTransportOrder")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IResult> PlaceTransportOrder()
        {
            var cmd = new PlaceTransportOrder(Guid.NewGuid());
            await _transportOrderCommandHandler.Handle(cmd);
            return Results.Accepted(value: cmd.Id);
        }

        [HttpPost("StartTransportOrder")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IResult> StartTransportOrder(Guid id)
        {
            var cmd = new StartTransportOrder(id);
            await _transportOrderCommandHandler.Handle(cmd);
            return Results.Accepted(value: cmd.Id);
        }

        [HttpPost("CompleteTransportOrder")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IResult> CompleteTransportOrder(Guid id)
        {
            var cmd = new CompleteTransportOrder(id);
            await _transportOrderCommandHandler.Handle(cmd);
            return Results.Accepted(value: cmd.Id);
        }
    }
}
