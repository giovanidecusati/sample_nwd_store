using Backoffice.Sales.IntegrationTest.Mocks;
using Backoffice.Sales.IntegrationTest.TestHelpers;
using BackOffice.Sales.Features.ProductFeature;
using BackOffice.Sales.Services;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using System.Threading.Tasks;
using TestStack.BDDfy;

namespace Backoffice.Sales.IntegrationTest.Features
{
    [Story(
      AsA = "As an User",
      IWant = "I want to create a new product",
      SoThat = "So that I can sell into the site.")]
    public class CreateProductScenario : ContainerDrivenScenario
    {
        private readonly ProductController _controller;
        private readonly IProductIntegrationEventService _productEventServiceMoq;
        private readonly Mock<IBusControl> _bus;

        private ProductViewModel _productViewModel;
        private BackOffice.Sales.Data.Entities.Category _category;

        public CreateProductScenario()
        {
            _productEventServiceMoq = new ProductIntegrationEventServiceMock(_databaseFixture.WorkDbContext);

            _bus = new Mock<IBusControl>();

            _controller = new ProductController(
                _productEventServiceMoq,
                _databaseFixture.WorkDbContext,
                _bus.Object);

            _productViewModel = new ProductViewModel()
            {
                CategoryId = 1,
                Featured = true,
                Name = "Teste 1",
                Price = 1.99m
            };
        }

        [Given(StepTitle = "Given a category saved in database")]
        public void GivenACategoryInDatabase()
        {
            _category = new BackOffice.Sales.Data.Entities.Category()
            {
                Name = "Cookies"
            };

            _databaseFixture.WorkDbContext.Categories.Add(_category);
            _databaseFixture.WorkDbContext.SaveChanges();
        }


        public void AndGivenANewProduct()
        {
            _productViewModel = new ProductViewModel()
            {
                CategoryId = _category.Id,
                Name = "Tintan",
                Price = 1.99m,
                Featured = true
            };
        }

        public async Task WhenPostProductIsCalled()
        {
            var result = (CreatedAtRouteResult)await _controller.Post(_productViewModel);

            result.StatusCode.ShouldBe(201);
            result.RouteValues.ShouldContain(f => f.Key == "Id");
            _productViewModel.Id = (int)result.RouteValues["Id"];
        }

        public async Task ThenProductHasShouldBeSave()
        {
            var result = (OkObjectResult)await _controller.Get(_productViewModel.Id);
            var model = (ProductViewModel)result.Value;
            result.StatusCode.ShouldBe(200);
            model.CategoryId.ShouldBe(_productViewModel.CategoryId);
            model.Featured.ShouldBe(_productViewModel.Featured);
            model.Id.ShouldBe(_productViewModel.Id);
            model.Name.ShouldBe(_productViewModel.Name);
            model.Price.ShouldBe(_productViewModel.Price);
        }
    }
}
