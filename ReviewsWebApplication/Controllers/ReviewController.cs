using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Review.Domain.Models;
using Review.Domain.Services;

namespace ReviewsWebApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewController : ControllerBase
{
    private readonly ILogger<ReviewController> _logger;
    private readonly IReviewService _reviewService;

    public ReviewController(ILogger<ReviewController> logger, IReviewService reviewService)
    {
        _logger = logger;
        _reviewService = reviewService;
    }

    /// <summary>
    /// ��������� ���� ������� �� ��������
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetFeedbacksByProductId")]
    public async Task<ActionResult<List<Feedback>>> GetAllReviewsAsync(int id)
    {
        try
        {
            var result = await _reviewService.GetFeedbacksByProductIdAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return BadRequest(new { Error = e.Message });
        }
    }

    /// <summary>
    /// ��������� ������
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetReview")]
    public async Task<ActionResult<List<Feedback>>> GetReviewAsync(int feedbackId, int productId)
    {
        try
        {
            var result = await _reviewService.GetReviewAsync(feedbackId, productId);
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return BadRequest(new { Error = e.Message });
        }
    }

    /// <summary>
    /// �������� ������ �� id
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpDelete("DeleteReview")]
    public async Task<ActionResult<List<Feedback>>> DeleteReviewAsync(int id)
    {
        try
        {
            var result = await _reviewService.TryToDeleteReviewAsync(id);
            if (result)
                return Ok();
            return BadRequest(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return BadRequest(new { Error = e.Message });
        }
    }
}