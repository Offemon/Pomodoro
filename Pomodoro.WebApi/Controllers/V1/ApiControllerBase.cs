using Asp.Versioning;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pomodoro.WebApi.Controllers.V1
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        private ISender? _mediator;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
