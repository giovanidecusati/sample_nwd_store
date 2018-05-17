using BuildingBlock.Core.Domain;
using System;

namespace Nwd.BackOffice.SalesContext.Entities
{
    public class Category : EntityBase
    {
        private Category() { }

        public Category(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}