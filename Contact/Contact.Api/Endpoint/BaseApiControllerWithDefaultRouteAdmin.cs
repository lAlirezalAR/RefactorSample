using Microsoft.AspNetCore.Mvc;

namespace Contact.Api.Endpoint
{
    [Route("api/v{version:apiVersion}/Admin/[controller]")]
    public abstract class BaseApiControllerWithDefaultRouteAdmin : BaseApiController
    {
    }
}
