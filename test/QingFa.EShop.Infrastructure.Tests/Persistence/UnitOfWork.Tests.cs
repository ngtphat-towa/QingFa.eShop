using FluentAssertions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using QingFa.EShop.Domain.DomainModels;
using QingFa.EShop.Domain.DomainModels.Interfaces;
using QingFa.EShop.Domain.DomainModels.Repositories;
using QingFa.EShop.Infrastructure.Persistence.Repositories;

using Xunit;

namespace QingFa.EShop.Infrastructure.Persistence.Tests
{
    public class UnitOfWorkTests : IDisposable
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly EShopDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkTests()
        {
            // Set up the in-memory database and service provider
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<EShopDbContext>(options =>
                options.UseInMemoryDatabase("TestDatabase"));

            // Add UnitOfWork and its dependencies to the service collection
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

            _serviceProvider = serviceCollection.BuildServiceProvider();

            _context = _serviceProvider.GetRequiredService<EShopDbContext>();
            _unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        public void Dispose()
        {
            _context.Dispose();
            _serviceProvider.Dispose();
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_WhenContextIsNull()
        {
            // Act
            var exception = Record.Exception(() => new UnitOfWork(null!));

            // Assert
            exception.Should().BeOfType<ArgumentNullException>()
                .Which.ParamName.Should().Be("context");
        }

        [Fact]
        public void Repository_ShouldReturnRepositoryInstance()
        {
            // Act
            var repository = _unitOfWork.Repository<TestEntity, int>();

            // Assert
            repository.Should().NotBeNull();
            repository.Should().BeOfType<IGenericRepository<TestEntity, int>>();
        }

        [Fact]
        public async Task CommitAsync_ShouldCallSaveChangesAsync()
        {
            // Act
            var result = await _unitOfWork.CommitAsync(CancellationToken.None);

            // Assert
            result.Should().Be(0); // No changes yet, so the result should be 0
        }

        [Fact]
        public async Task RollbackAsync_ShouldHandleTransaction()
        {
            // Arrange
            await _unitOfWork.BeginTransactionAsync(CancellationToken.None);

            // Act
            await _unitOfWork.RollbackAsync(CancellationToken.None);

            // Assert
            // Rollback should not throw exceptions
        }

        [Fact]
        public async Task RollbackAsync_ShouldRollbackOnException()
        {
            // Arrange
            _context.Add(new TestEntity(1, "Test"));
            await _unitOfWork.CommitAsync(CancellationToken.None);

            // Act
            Func<Task> act = async () =>
            {
                await _unitOfWork.BeginTransactionAsync(CancellationToken.None);
                _context.Add(new TestEntity(2, "Another Test"));
                await _unitOfWork.CommitAsync(CancellationToken.None); // Commit the new data
                throw new InvalidOperationException("Testing rollback");
            };

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>(); // Expect exception due to rollback test

            // After the exception, check the data
            var entityFromDb = await _context.Set<TestEntity>().FindAsync(1);
            entityFromDb.Should().NotBeNull(); // The first entity should be present as it was committed

            var entityFromDb2 = await _context.Set<TestEntity>().FindAsync(2);
            entityFromDb2.Should().BeNull(); // The second entity should be rolled back
        }
    }

    // Concrete class for testing
    public class TestEntity : Entity<int>
    {
        public string Name { get; set; }

        public TestEntity(int id, string name) : base(id)
        {
            Name = name;
        }
    }

    public class EShopDbContext : DbContext
    {
        public EShopDbContext(DbContextOptions<EShopDbContext> options) : base(options) { }

        public DbSet<TestEntity> TestEntities { get; set; }
    }
}
