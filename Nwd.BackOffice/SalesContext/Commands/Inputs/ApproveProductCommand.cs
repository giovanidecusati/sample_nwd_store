using BuildingBlock.Core.Commands;
using BuildingBlock.Core.Notifications;
using BuildingBlock.Core.Validators;
using System;

namespace Nwd.BackOffice.SalesContext.Commands.Inputs
{
    public class ApproveProductCommand : Notifiable, ICommand
    {
        public Guid ProductId { get; set; }

        public bool IsValid()
        {
            var contract = new ValidationContract<ApproveProductCommand>(this)
                .IsNotNull(ProductId, "ProductId could not be null.");

            return !HasNotifications();
        }
    }
}