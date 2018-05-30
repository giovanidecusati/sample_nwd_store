using System.Threading;
using System.Threading.Tasks;
using BackOffice.Sales.Contracts.v1;
using BackOffice.Sales.Data.Contexts;
using BackOffice.Sales.Services;
using BuildingBlock.Core.IntegrationEvents;

namespace Backoffice.Sales.IntegrationTest.Mocks
{
    public class ProductIntegrationEventServiceMock : IProductIntegrationEventService
    {
        private readonly SalesDbContext _salesContext;

        public ProductIntegrationEventServiceMock(SalesDbContext salesContext)
        {
            _salesContext = salesContext;
        }

        public async Task PublishThroughEventBusAsync(ProductCreatedIntegrationEvent @event, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _salesContext.SaveChangesAsync(cancellationToken);
        }

        public async Task SaveEventAndProductContextChangesAsync(IntegrationEvent @event, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.FromResult(0);
        }
    }
}
