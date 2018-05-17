using Microsoft.AspNetCore.Mvc;
using Nwd.BackOffice.Base;
using Nwd.BackOffice.SalesContext.Commands.Handlers;
using Nwd.BackOffice.SalesContext.Commands.Inputs;
using Nwd.BackOffice.SalesContext.Data.Contexts;
using System.Threading.Tasks;

namespace Nwd.BackOffice.SalesContext.Controllers
{
    [Route("v1/[controller]/[action]")]
    public class CategoryController : BaseController
    {
        private readonly CategoryHandler _handler;
        public CategoryController(CategoryHandler handler, SalesDbContext context) : base(context)
        {
            _handler = handler;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCategoryCommand command)
        {
            if (!command.IsValid())
                return await CreateResponse(null, command.Notifications);

            var result = await _handler.Handle(command);

            return await CreateResponse(result, _handler.Notifications);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateCategoryCommand command)
        {
            if (!command.IsValid())
                return await CreateResponse(notifications: command.Notifications);

            await _handler.Handle(command);

            return await CreateResponse(_handler.Notifications);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteCategoryCommand command)
        {
            if (!command.IsValid())
                return await CreateResponse(notifications: command.Notifications);

            await _handler.Handle(command);

            return await CreateResponse(_handler.Notifications);
        }
    }
}