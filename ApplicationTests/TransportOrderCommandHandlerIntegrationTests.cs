using Application;
using Domain;
using Infrastructure;

namespace ApplicationTests
{
    public class TransportOrderCommandHandlerIntegrationTests
    {
        [Fact]
        public async Task PlacingAnOrder_CreatesTheOrderWithStatusNew()
        {
            // Arrange
            var fakeOutbox = new FakeOutbox();
            var sut = new TransportOrderCommandHandler(new TransportOrderRepository(fakeOutbox));
            var readService = new TransportOrderReadService(new TransportOrderRepository(fakeOutbox));
            var transportOrderId = Guid.NewGuid();

            // Act
            await sut.Handle(new PlaceTransportOrder(transportOrderId));

            // Assert that the order was created with the correct status and that an event was added to the outbox
            var transportOrder = await readService.GetTransportOrderByIdAsync(transportOrderId);
            Assert.NotNull(transportOrder);
            Assert.Equal(TransportOrder.OrderState.New.ToString(), transportOrder.State);
            Assert.Single(fakeOutbox.OutboxedEvents);
        }
        
        [Fact]
        public async Task StartingAnOrder_ChangesStateToInProgress()
        {
            // Arrange
            var fakeOutbox = new FakeOutbox();
            var sut = new TransportOrderCommandHandler(new TransportOrderRepository(fakeOutbox));
            var readService = new TransportOrderReadService(new TransportOrderRepository(fakeOutbox));
            var transportOrderId = Guid.NewGuid();

            // Act
            await sut.Handle(new PlaceTransportOrder(transportOrderId));
            await sut.Handle(new StartTransportOrder(transportOrderId));

            // Assert that the orders state was updated correctly and that events was added to the outbox
            var transportOrder = await readService.GetTransportOrderByIdAsync(transportOrderId);
            Assert.NotNull(transportOrder);
            Assert.Equal(TransportOrder.OrderState.InProgress.ToString(), transportOrder.State);
            Assert.Equal(2, fakeOutbox.OutboxedEvents.Count);
        }

        [Fact]
        public async Task CompletingAnOrder_ChangesStateToCompleted()
        {
            // Arrange
            var fakeOutbox = new FakeOutbox();
            var sut = new TransportOrderCommandHandler(new TransportOrderRepository(fakeOutbox));
            var readService = new TransportOrderReadService(new TransportOrderRepository(fakeOutbox));
            var transportOrderId = Guid.NewGuid();

            // Act
            await sut.Handle(new PlaceTransportOrder(transportOrderId));
            await sut.Handle(new StartTransportOrder(transportOrderId));
            await sut.Handle(new CompleteTransportOrder(transportOrderId));

            // Assert that the orders state was updated correctly and that events was added to the outbox
            var transportOrder = await readService.GetTransportOrderByIdAsync(transportOrderId);
            Assert.NotNull(transportOrder);
            Assert.Equal(TransportOrder.OrderState.Completed.ToString(), transportOrder.State);
            Assert.Equal(3, fakeOutbox.OutboxedEvents.Count);
        }

        [Fact]
        public async Task StartingAnInProgressOrder_ThrowsException()
        {
            // Arrange
            var sut = new TransportOrderCommandHandler(new TransportOrderRepository(new FakeOutbox()));
            var readService = new TransportOrderReadService(new TransportOrderRepository(new FakeOutbox()));
            var transportOrderId = Guid.NewGuid();

            // Act
            await sut.Handle(new PlaceTransportOrder(transportOrderId));
            await sut.Handle(new StartTransportOrder(transportOrderId));

            // Assert that an exception is thrown when trying to start an order that is already in progress
            await Assert.ThrowsAsync<InvalidOperationException>(() => sut.Handle(new StartTransportOrder(transportOrderId)));
        }

        [Fact]
        public async Task StartingACompletedOrder_ThrowsException()
        {
            // Arrange
            var sut = new TransportOrderCommandHandler(new TransportOrderRepository(new FakeOutbox()));
            var readService = new TransportOrderReadService(new TransportOrderRepository(new FakeOutbox()));
            var transportOrderId = Guid.NewGuid();

            // Act
            await sut.Handle(new PlaceTransportOrder(transportOrderId));
            await sut.Handle(new StartTransportOrder(transportOrderId));
            await sut.Handle(new CompleteTransportOrder(transportOrderId));

            // Assert that an exception is thrown when trying to start an order that is already completed
            await Assert.ThrowsAsync<InvalidOperationException>(() => sut.Handle(new StartTransportOrder(transportOrderId)));
        }

        [Fact]
        public async Task CompletingANewOrder_ThrowsException()
        {
            // Arrange
            var sut = new TransportOrderCommandHandler(new TransportOrderRepository(new FakeOutbox()));
            var readService = new TransportOrderReadService(new TransportOrderRepository(new FakeOutbox()));
            var transportOrderId = Guid.NewGuid();

            // Act
            await sut.Handle(new PlaceTransportOrder(transportOrderId));

            // Assert that an exception is thrown when trying to start an order that is already completed
            await Assert.ThrowsAsync<InvalidOperationException>(() => sut.Handle(new CompleteTransportOrder(transportOrderId)));
        }
    }
}