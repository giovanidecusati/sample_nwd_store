
using BuildingBlock.Core.Commands;
using BuildingBlock.Core.Notifications;
using BuildingBlock.Core.Validators;
using System;

namespace Nwd.BackOffice.SalesContext.Commands.Inputs
{
    public class UpdateCategoryCommand : Notifiable, ICommand
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }

        public bool IsValid()
        {
            var contract = new ValidationContract<UpdateCategoryCommand>(this)
                .HasMinLenght(p => p.Name, 3, "Invalid min lenght of Name.")
                .HasMaxLenght(p => p.Name, 50, "Invalid max lenght of Name.")
                .IsNotNull(CategoryId, "CategoryId could not be null.");

            return !HasNotifications();
        }

    }
}