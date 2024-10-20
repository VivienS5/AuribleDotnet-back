using System.Net.Http.Headers;
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
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly GraphServiceClient _graphServiceClient;

        public UserController(ITokenAcquisition tokenAcquisition,IAuthorizationHeaderProvider authorizationHeaderProvider, GraphServiceClient graphServiceClient){
            this.authorizationHeaderProvider = authorizationHeaderProvider;
            this._graphServiceClient = graphServiceClient;
            this._tokenAcquisition = tokenAcquisition;
        }
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { "User.Read" });

            // Créer le client Graph avec le token
            _graphServiceClient.AuthenticationProvider = new DelegateAuthenticationProvider(
                requestMessage => {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    return Task.CompletedTask;
                });
            var user = await _graphServiceClient.Me.Request().GetAsync();
            try
            {
                Console.WriteLine(user.DisplayName);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}