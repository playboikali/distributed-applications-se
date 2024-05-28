using GL.ApplicationServices.Interfaces;
using GL.Data.Entities;
using GL.Infrastructure.Messaging.Responses.Genres;
using GL.Infrastructure.Messaging;
using GL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GL.Infrastructure.Messaging.Requests.Genres;

namespace GL.WebAPI.Controllers
{
    /// <summary>
    /// Genre controller.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class GenresController : ControllerBase
    {
        private readonly IGenresManagementServicе _genreService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenresController"/> class.
        /// </summary>
        /// <param name="genreService">Movie management service.</param>
        public GenresController(IGenresManagementServicе genreService)
        {
            _genreService = genreService;
        }

        /// <summary>
        /// Get genres.
        /// </summary>
        /// <returns>Return list of active genre.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(GetGenresResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _genreService.GetGenres());
        }

        /// <summary>
        /// Create new genre.
        /// </summary>
        /// <param name="genre">Genre model object.</param>
        /// <returns>Return create empty response.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CreateGenreResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] GenreModel genre)
        {
            return Ok(await _genreService.CreateGenre(new(genre)));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetGenreByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _genreService.GetGenreById(new GetGenreByIdRequest { Id = id }));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdateGenreResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] GenreModel genre)
        {
            return Ok(await _genreService.UpdateGenre(new UpdateGenreRequest { Id = id, Genre = genre }));
        }

        [HttpPost("search")]
        [ProducesResponseType(typeof(SearchGenresResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Search([FromBody] SearchGenresRequest request)
        {
            return Ok(await _genreService.SearchGenres(request));
        }

        [HttpPost("searchNF")]
        [ProducesResponseType(typeof(SearchGenresResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SearchNF([FromBody] SearchGenresNFRequest request)
        {
            return Ok(await _genreService.SearchGenresByNF(request));
        }

        /// <summary>
        /// Delete genre.
        /// </summary>
        /// <param name="id">Genre identifier.</param>
        /// <returns>Return empty object.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DeleteGenreResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteGenre([FromRoute] int id) => Ok(await _genreService.DeleteGenre(new(id)));
    }
}
