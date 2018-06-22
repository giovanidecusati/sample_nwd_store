using Backoffice.Sales.IntegrationTest.TestHelpers.Fixtures;
using BackOffice.Sales.Mappings;
using Humanizer;
using System;
using TestStack.BDDfy;
using Xunit;

namespace Backoffice.Sales.IntegrationTest.TestHelpers
{
    [Collection("ContainerDrivenScenario")]
    public abstract class ContainerDrivenScenario : IDisposable
    {
        internal readonly DatabaseFixture _databaseFixture;

        static ContainerDrivenScenario()
        {
            MapperExtension.RegisterProfiles();
        }

        public ContainerDrivenScenario()
        {
            _databaseFixture = new DatabaseFixture();
        }


        [Fact]
        public virtual void ExecuteScenario()
        {
            this.BDDfy(GetType().Name);
        }

        protected virtual string BuildTitle()
        {
            return Title ?? GetType().Name.Humanize(LetterCasing.Title);
        }


        public virtual Type Story { get { return GetType(); } }
        public virtual string Title { get; set; }
        public string Category { get; set; }

        public void Dispose()
        {
            _databaseFixture.Dispose();
        }
    }
}
