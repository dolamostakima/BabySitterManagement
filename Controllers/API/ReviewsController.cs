using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBabySitter.Services;
using SmartBabySitter.Services.DTOs;

namespace SmartBabySitter.Controllers.API;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Parent")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviews;

    public ReviewsController(IReviewService reviews)
    {
        _reviews = reviews;
    }

    // GET: /api/reviews/can-review?bookingId=1
    [HttpGet("can-review")]
    public async Task<IActionResult> CanReview([FromQuery] int bookingId)
        => Ok(await _reviews.CanReviewAsync(bookingId));

    // POST: /api/reviews
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ReviewCreateDto dto)
    {
        try
        {
            var id = await _reviews.CreateAsync(dto);
            return Ok(new { reviewId = id });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("me")]
    public async Task<IActionResult> My()
    => Ok(await _reviews.GetMyReviewsAsync());
}