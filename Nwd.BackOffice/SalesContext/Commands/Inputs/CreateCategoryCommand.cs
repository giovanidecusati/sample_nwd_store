using BuildingBlock.Core.Commands;
using BuildingBlock.Core.Notifications;
using BuildingBlock.Core.Validators;

namespace Nwd.BackOffice.SalesContext.Commands.Inputs
{
    public class CreateCategoryCommand : Notifiable, ICommand
    {
        public string Name { get; set; }

        public bool IsValid()
        {
            var contract = new ValidationContract<CreateCategoryCommand>(this)
                .HasMinLenght(p => p.Name, 3, "Invalid min lenght of Name.")
                .HasMaxLenght(p => p.Name, 50, "Invalid max lenght of Name.");

            return !HasNotifications();
        }
    }
}