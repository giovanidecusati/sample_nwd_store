using BuildingBlock.Core.Commands;
using BuildingBlock.Core.Notifications;
using BuildingBlock.Core.Validators;
using System;

namespace Nwd.BackOffice.SalesContext.Commands.Inputs
{
    public class UpdateProductCommand : Notifiable, ICommand
    {
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public bool IsValid()
        {
            var contract = new ValidationContract<UpdateProductCommand>(this)
                .HasMinLenght(p => p.Name, 3, "Invalid min lenght of Name.")
                .HasMaxLenght(p => p.Name, 50, "Invalid max lenght of Name.")
                .IsGreaterThan(p => p.Price, 0, "Price must be grater than 0.")
                .IsNotNull(CategoryId, "CategoryId could not be null.")
                .IsNotNull(ProductId, "ProductID could not be null.");

            return !HasNotifications();
        }
    }
}