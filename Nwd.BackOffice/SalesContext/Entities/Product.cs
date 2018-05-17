using BuildingBlock.Core.Domain;
using BuildingBlock.Core.DomainEvents.Events;
using BuildingBlock.Core.Validators;
using Nwd.BackOffice.SalesContext.Events;
using System;

namespace Nwd.BackOffice.SalesContext.Entities
{
    public class Product : EntityBase
    {
        private Product() { }

        public Product(Guid categoryId, string name, decimal price)
        {
            Id = Guid.NewGuid();
            CategoryId = categoryId;
            Name = name;
            Price = price;
            IsApproved = false;
        }

        public Guid Id { get; private set; }
        public Guid CategoryId { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public bool IsApproved { get; private set; }

        public void Approved()
        {
            var contract = new ValidationContract<Product>(this)
                .IsFalse(IsApproved, "IsApproved", "Product already approved.");

            if (!HasNotifications())
            {
                IsApproved = true;
                DomainEvents.Raise(new ProductApprovedEvent(Id));
            }
        }
    }
}
