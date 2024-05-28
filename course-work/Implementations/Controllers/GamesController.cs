using GL.ApplicationServices.Interfaces;
using GL.Data.Entities;
using GL.Infrastructure.Messaging;
using GL.Infrastructure.Messaging.Requests.Games;
using GL.Infrastructure.Messaging.Responses.Games;
using GL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GL.WebAPI.Controllers
{
    /// <summary>
    /// Movies controller.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class GamesController : ControllerBase
    {
        private readonly IGamesManagementService _gameService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GamesController"/> class.
        /// </summary>
        /// <param name="gameService">Game management service.</param>
        public GamesController(IGamesManagementService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        /// Get games.
        /// </summary>
        /// <param name="isActive">Is active boolean type.</param>
        /// <returns>Return list of active games.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(GetGamesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] bool isActive = true, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var request = new GetGamesRequest(isActive, pageNumber, pageSize);
            var response = await _gameService.GetGames(request);
            return Ok(response);
        }

        /// <summary>
        /// Create new game.
        /// </summary>
        /// <param name="game">Game model object.</param>
        /// <returns>Return create empty response.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreateGameResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateGame([FromBody] GameModel game) => Ok(await _gameService.CreateGame(new(game)));

        /// <summary>
        /// Update existing game.
        /// </summary>
        /// <param name="id">Game identifier.</param>
        /// <param name="game">Game model object.</param>
        /// <returns>Return updated game response.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdateGameResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateGame([FromRoute] int id, [FromBody] GameModel game)
        {
            return Ok(await _gameService.UpdateGame(new(id, game)));
        }

        /// <summary>
        /// Delete Game.
        /// </summary>
        /// <param name="id">Game identifier.</param>
        /// <returns>Return empty object.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DeleteGameResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMovie([FromRoute] int id) => Ok(await _gameService.DeleteGame(new(id)));

        /// <summary>
        /// Search games by criteria.
        /// </summary>
        /// <param name="name">Game name.</param>
        /// <returns>Return list of games.</returns>
        [HttpGet("search")]
        [ProducesResponseType(typeof(GetGamesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SearchGames([FromQuery] string name)
        {
            return Ok(await _gameService.SearchGames(new(name)));
        }
    }
}
