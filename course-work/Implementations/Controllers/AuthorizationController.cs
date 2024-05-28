using GL.ApplicationServices.Interfaces;
using GL.Infrastructure.Messaging.Responses.Authentications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GL.WebAPI.Controllers
{
    /// <summary>
    /// Authentication controller.
    /// </summary>
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationController"/> class.
        /// </summary>
        /// <param name="jwtAuthenticationManager">Jwt authentication manager</param>
        public AuthorizationController(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        /// <summary>
        /// Generate Jwt token.
        /// </summary>
        /// <param name="clientId">Client identifier.</param>
        /// <param name="secret">Client secret</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Token([FromQuery] string clientId, [FromQuery] string secret)
        {
            string? token = _jwtAuthenticationManager.Authenticate(clientId, secret);

            ArgumentNullException.ThrowIfNull(token);


            return Ok(await Task.FromResult(new AuthenticationResponse(token)));
        }
    }
}
