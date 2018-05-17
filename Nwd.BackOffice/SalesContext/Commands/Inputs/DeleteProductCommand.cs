using BuildingBlock.Core.Commands;
using BuildingBlock.Core.Notifications;
using BuildingBlock.Core.Validators;
using System;

namespace Nwd.BackOffice.SalesContext.Commands.Inputs
{
    public class DeleteProductCommand : Notifiable, ICommand
    {
        public Guid ProductId { get; set; }

        public bool IsValid()
        {
            var contract = new ValidationContract<DeleteProductCommand>(this)
                .IsNotNull(ProductId, "ProductId could not be null.");

            return !HasNotifications();
        }
    }
}