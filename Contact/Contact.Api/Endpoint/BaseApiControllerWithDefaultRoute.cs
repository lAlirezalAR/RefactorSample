using Microsoft.AspNetCore.Mvc;

namespace Contact.Api.Endpoint
{
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiControllerWithDefaultRoute : BaseApiController
    {
    }
}
