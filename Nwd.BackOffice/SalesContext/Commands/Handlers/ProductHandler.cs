using BuildingBlock.Core.Commands;
using BuildingBlock.Core.Notifications;
using Nwd.BackOffice.SalesContext.Commands.Inputs;
using Nwd.BackOffice.SalesContext.Commands.Outputs;
using Nwd.BackOffice.SalesContext.Entities;
using Nwd.BackOffice.SalesContext.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nwd.BackOffice.SalesContext.Commands.Handlers
{
    public class ProductHandler : Notifiable,
        ICommandHandler<CreateProductCommand, Task<Guid>>,
        ICommandHandler<UpdateProductCommand, Task>,
        ICommandHandler<DeleteProductCommand, Task>,
        ICommandHandler<ApproveProductCommand, Task>,
        ICommandHandler<GetProductsCommand, Task<IEnumerable<GetProductListResult>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;


        public ProductHandler(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Guid> Handle(CreateProductCommand command)
        {
            var product = new Product(command.CategoryId, command.Name, command.Price);

            if (!product.HasNotifications())
                await _productRepository.CreateAsync(product);

            AddNotifications(product.Notifications);
            return product.Id;
        }

        public async Task Handle(UpdateProductCommand command)
        {
            var product = await _productRepository.GetProductByIdAsync(command.ProductId);
            if (product == null)
                AddNotification("ProductId", "Invalid ProductId.");

            var category = await _categoryRepository.GetCategoryByIdAsync(command.CategoryId);
            if (product == null)
                AddNotification("CategoryId", "Invalid CategoryId.");

            if (!HasNotifications())
                await _productRepository.CreateAsync(product);

            return;
        }

        public async Task Handle(DeleteProductCommand command)
        {
            var product = await _productRepository.GetProductByIdAsync(command.ProductId);
            if (product == null)
                AddNotification("ProductId", "Invalid ProductId.");

            if (!HasNotifications())
                _productRepository.Delete(product);

            return;
        }

        public async Task Handle(ApproveProductCommand command)
        {
            var product = await _productRepository.GetProductByIdAsync(command.ProductId);
            if (product == null)
                AddNotification("ProductId", "Invalid ProductId.");

            if (!HasNotifications())
                product.Approved();

            if (!product.HasNotifications())
                _productRepository.Update(product);

            AddNotifications(product.Notifications);
            return;
        }

        public async Task<IEnumerable<GetProductListResult>> Handle(GetProductsCommand command)
        {
            return await _productRepository.GetProducListAsync(command.Page, command.PageSize, command.OrderBy, command.Ascending);
        }
    }
}
