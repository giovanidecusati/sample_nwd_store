using BuildingBlock.Core.Commands;
using BuildingBlock.Core.Notifications;
using BuildingBlock.Core.Validators;

namespace Nwd.BackOffice.SalesContext.Commands.Inputs
{
    public class GetProductsCommand : Notifiable, ICommand
    {
        public int Page { get; set; }
        public int PageSize { get; set; } = 10;
        public string OrderBy { get; set; } = "Name";
        public bool Ascending { get; set; } = true;

        public bool IsValid()
        {
            var contract = new ValidationContract<GetProductsCommand>(this)
                .IsGreaterOrEqualsThan(p => p.PageSize, 1, "Invalid PageSize.");

            return !HasNotifications();
        }
    }
}
