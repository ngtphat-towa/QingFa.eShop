using MediatR;

using Moq;

using QingFa.EShop.Domain.DomainModels.Extensions;
using QingFa.EShop.Domain.DomainModels.Interfaces;

namespace QingFa.EShop.Domain.Tests.DomainModels.Extensions
{
    public class AggregateRootExtensionsTests
    {
        [Fact]
        public async Task RelayAndPublishEvents_ShouldPublishAllDomainEvents()
        {
            // Arrange
            var domainEvent1 = new Mock<IDomainEvent>();
            var domainEvent2 = new Mock<IDomainEvent>();

            var domainEvents = new List<IDomainEvent> { domainEvent1.Object, domainEvent2.Object };
            var aggregateRootMock = new Mock<IHasDomainEvent>();
            aggregateRootMock.Setup(x => x.DomainEvents).Returns(domainEvents);
            aggregateRootMock.Setup(x => x.ClearDomainEvents());

            var publisherMock = new Mock<IPublisher>();

            // Act
            await aggregateRootMock.Object.RelayAndPublishEvents(
                publisherMock.Object,
                CancellationToken.None
            );

            // Assert
            aggregateRootMock.Verify(x => x.ClearDomainEvents(), Times.Once);
            publisherMock.Verify(
                p =>
                    p.Publish(
                        It.Is<EventWrapper>(ew => ew.Event == domainEvent1.Object),
                        CancellationToken.None
                    ),
                Times.Once
            );
            publisherMock.Verify(
                p =>
                    p.Publish(
                        It.Is<EventWrapper>(ew => ew.Event == domainEvent2.Object),
                        CancellationToken.None
                    ),
                Times.Once
            );
        }
    }
}
