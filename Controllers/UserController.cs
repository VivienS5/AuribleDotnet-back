using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web;

namespace AuribleDotnet_back.Controllers
{
    [ApiController]
    [Route("user")]
    [Authorize]
    public class UserController: ControllerBase
    {
         private readonly IAuthorizationHeaderProvider authorizationHeaderProvider;
        private readonly GraphServiceClient _graphServiceClient;

        public UserController(IAuthorizationHeaderProvider authorizationHeaderProvider, GraphServiceClient graphServiceClient){
            this.authorizationHeaderProvider = authorizationHeaderProvider;
            this._graphServiceClient = graphServiceClient;
        }
        [HttpGet("profile")]
        [AuthorizeForScopes(Scopes = ["user.read"])]
        public async Task<IActionResult> Profile()
        {
            try
            {
                var user = await _graphServiceClient.Me.Request().GetAsync();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}