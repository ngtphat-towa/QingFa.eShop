using FluentAssertions;

using QingFa.EShop.Domain.DomainModels.Core;

namespace QingFa.EShop.Domain.Tests.Catalogs
{
    // Concrete implementation of AggregateRoot for testing purposes
    public class TestAggregateRoot : AggregateRoot<int>
    {
        public TestAggregateRoot(int id) : base(id) { }
    }

    public class AggregateRootTests
    {
        [Fact]
        public void Equality_ShouldReturnTrue_ForEqualAggregateRoots()
        {
            // Arrange
            var id = 1;
            var aggregateRoot1 = new TestAggregateRoot(id);
            var aggregateRoot2 = new TestAggregateRoot(id);

            // Act & Assert
            aggregateRoot1.Should().Be(aggregateRoot2);
        }

        [Fact]
        public void Equality_ShouldReturnFalse_ForDifferentAggregateRoots()
        {
            // Arrange
            var id1 = 1;
            var id2 = 2;
            var aggregateRoot1 = new TestAggregateRoot(id1);
            var aggregateRoot2 = new TestAggregateRoot(id2);

            // Act & Assert
            aggregateRoot1.Should().NotBe(aggregateRoot2);
        }

        [Fact]
        public void GetHashCode_ShouldBeSame_ForEqualAggregateRoots()
        {
            // Arrange
            var id = 1;
            var aggregateRoot1 = new TestAggregateRoot(id);
            var aggregateRoot2 = new TestAggregateRoot(id);

            // Act & Assert
            aggregateRoot1.GetHashCode().Should().Be(aggregateRoot2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ShouldBeDifferent_ForDifferentAggregateRoots()
        {
            // Arrange
            var id1 = 1;
            var id2 = 2;
            var aggregateRoot1 = new TestAggregateRoot(id1);
            var aggregateRoot2 = new TestAggregateRoot(id2);

            // Act & Assert
            aggregateRoot1.GetHashCode().Should().NotBe(aggregateRoot2.GetHashCode());
        }
    }
}
