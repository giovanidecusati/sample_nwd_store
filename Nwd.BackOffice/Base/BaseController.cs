using BuildingBlock.Core.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nwd.BackOffice.Base
{
    public class BaseController : ControllerBase
    {
        protected readonly DbContext _context;

        public BaseController(DbContext context)
        {
            _context = context;
        }

        protected async Task<IActionResult> CreateResponse(object result = null, IEnumerable<Notification> notifications = null)
        {
            var tsc = new TaskCompletionSource<IActionResult>();

            if (notifications != null && notifications.Any())
            {
                tsc.SetResult(BadRequest(new
                {
                    success = false,
                    data = result,
                    errors = notifications
                }));
            }
            else
            {
                await _context.SaveChangesAsync();
                tsc.SetResult(Ok(new
                {
                    success = true,
                    data = result,
                    errors = notifications
                }));
            }

            return await tsc.Task;
        }
    }
}
