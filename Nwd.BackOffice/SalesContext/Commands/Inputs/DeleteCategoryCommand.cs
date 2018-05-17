using BuildingBlock.Core.Commands;
using BuildingBlock.Core.Notifications;
using BuildingBlock.Core.Validators;
using System;

namespace Nwd.BackOffice.SalesContext.Commands.Inputs
{
    public class DeleteCategoryCommand : Notifiable, ICommand
    {
        public Guid CategoryId { get; set; }

        public bool IsValid()
        {
            var contract = new ValidationContract<DeleteCategoryCommand>(this)
                .IsNotNull(CategoryId, "CategoryId could not be null.");

            return !HasNotifications();
        }
    }
}