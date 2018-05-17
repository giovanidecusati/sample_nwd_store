using BuildingBlock.Core.Commands;
using BuildingBlock.Core.Notifications;
using Nwd.BackOffice.SalesContext.Commands.Inputs;
using Nwd.BackOffice.SalesContext.Entities;
using Nwd.BackOffice.SalesContext.Repositories;
using System;
using System.Threading.Tasks;

namespace Nwd.BackOffice.SalesContext.Commands.Handlers
{
    public class CategoryHandler : Notifiable,
        ICommandHandler<CreateCategoryCommand, Task<Guid>>,
        ICommandHandler<UpdateCategoryCommand, Task>,
        ICommandHandler<DeleteCategoryCommand, Task>
    {
        private readonly ICategoryRepository _repository;

        public CategoryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateCategoryCommand command)
        {
            var category = new Category(command.Name);

            if (!category.HasNotifications())
                await _repository.CreateAsync(category);

            AddNotifications(category.Notifications);
            return category.Id;
        }

        public async Task Handle(UpdateCategoryCommand command)
        {
            var category = await _repository.GetCategoryByIdAsync(command.CategoryId);
            if (category == null)
                AddNotification("CategoryId", "Invalid CategoryId.");

            if (!HasNotifications())
                _repository.Update(category);

            return;
        }

        public async Task Handle(DeleteCategoryCommand command)
        {
            var category = await _repository.GetCategoryByIdAsync(command.CategoryId);
            if (category == null)
                AddNotification("CategoryId", "Invalid CategoryId.");

            _repository.Delete(category);

            return;
        }
    }
}
