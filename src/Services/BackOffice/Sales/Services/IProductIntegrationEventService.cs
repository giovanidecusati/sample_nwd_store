using BackOffice.Sales.Contracts.v1;
using BuildingBlock.Core.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;

namespace BackOffice.Sales.Services
{
    public interface IProductIntegrationEventService
    {
        Task PublishThroughEventBusAsync(ProductCreatedIntegrationEvent @event, CancellationToken cancellationToken = default(CancellationToken));
        Task SaveEventAndProductContextChangesAsync(IntegrationEvent @event, CancellationToken cancellationToken = default(CancellationToken));
    }
}