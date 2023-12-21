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
    [HttpGet ("TryGetAll")]
    public async Task<ActionResult<List<Feedback>>> TryGetAllAsync()
    {
        try
        {
            var result = await _reviewService.GetAllAsync();
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
    [HttpGet("TryGetByProductId")]
    public async Task<ActionResult<List<Feedback>>> TryGetByProductIdAsync(int productId)
    {
        try
        {
            var result = await _reviewService.GetByProductIdAsync(productId);
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
    [HttpDelete("TryDelete")]
    public async Task<ActionResult<List<Feedback>>> TryDeleteAsync(int id)
    {
        try
        {
            var result = await _reviewService.TryDeleteAsync(id);
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