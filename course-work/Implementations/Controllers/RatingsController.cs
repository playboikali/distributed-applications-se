using GL.ApplicationServices.Interfaces;
using GL.Infrastructure.Messaging.Requests.Ratings;
using GL.Infrastructure.Messaging.Responses.Ratings;
using GL.Infrastructure.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Azure.Core;
using GL.Data.Entities;

namespace GL.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingsManagementService _ratingsService;

        public RatingsController(IRatingsManagementService ratingsService)
        {
            _ratingsService = ratingsService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetRatingsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _ratingsService.GetRatings());
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateRatingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] RatingModel rating)
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var createRatingRequest = new CreateRatingRequest
            {
                Rating = rating,
                UserId = userId
            };

            var response = await _ratingsService.CreateRating(createRatingRequest);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.Message);
            }
            //var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            //if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            //{
            //    return BadRequest("User ID is missing or invalid.");
            //}

            //var createRatingRequest = new CreateRatingRequest
            //{
            //    Rating = rating,
            //    UserId = userId // Set UserId from token
            //};

            //var response = await _ratingsService.CreateRating(createRatingRequest);
            //if (!response.Success)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
            //}

            //return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetRatingByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _ratingsService.GetRatingById(new GetRatingByIdRequest { Id = id }));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdateRatingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] RatingModel rating)
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var updateRatingRequest = new UpdateRatingRequest
            {
                Id = id,
                Rating = rating,
                UserId = userId
            };

            var response = await _ratingsService.UpdateRating(updateRatingRequest);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.Message);
            }
            //return Ok(await _ratingsService.UpdateRating(new UpdateRatingRequest { Id = id, Rating = rating }));
        }

        [HttpPost("search")]
        [ProducesResponseType(typeof(SearchRatingsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Search([FromBody] SearchRatingsRequest request)
        {
            return Ok(await _ratingsService.SearchRatings(request));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DeleteRatingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponseError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteRating([FromRoute] int id)
        {
            var response = await _ratingsService.DeleteRating(new DeleteRatingRequest { Id = id });

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.Message);
            }
            //return Ok(await _ratingsService.DeleteRating(new DeleteRatingRequest { Id = id }));
        }
    }
}
