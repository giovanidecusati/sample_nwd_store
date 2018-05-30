using BackOffice.Sales.Contracts.v1;
using BackOffice.Sales.Data.Contexts;
using BuildingBlock.Core.IntegrationEvents;
using BuildingBlock.IntegrationEventLog.Services;
using BuildingBlock.IntegrationEventLog.Utilities;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace BackOffice.Sales.Services
{
    public class ProductIntegrationEventService : IProductIntegrationEventService
    {
        private readonly Func<DbConnection, IntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IBusControl _eventBus;
        private readonly SalesDbContext _salesContext;
        private readonly IntegrationEventLogService _eventLogService;

        public ProductIntegrationEventService(
            IBusControl eventBus,
            SalesDbContext salesContext,
            Func<DbConnection, IntegrationEventLogService> integrationEventLogServiceFactory)
        {
            _salesContext = salesContext ?? throw new ArgumentNullException(nameof(salesContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = _integrationEventLogServiceFactory(salesContext.Database.GetDbConnection());
        }

        public async Task PublishThroughEventBusAsync(ProductCreatedIntegrationEvent @event, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _eventBus.Publish(@event, cancellationToken);

            await _eventLogService.MarkEventAsPublishedAsync(@event, cancellationToken);
        }

        public async Task SaveEventAndProductContextChangesAsync(IntegrationEvent @event, CancellationToken cancellationToken = default(CancellationToken))
        {
            //Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
            //See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency            
            await ResilientTransaction.New(_salesContext)
                .ExecuteAsync(async () =>
                {
                    // Achieving atomicity between original catalog database operation and the IntegrationEventLog thanks to a local transaction
                    await _salesContext.SaveChangesAsync(cancellationToken);
                    await _eventLogService.SaveEventAsync(@event, _salesContext.Database.CurrentTransaction.GetDbTransaction());
                });
        }
    }
}