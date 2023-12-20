using Review.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Review.Domain.Services;

public class ReviewService : IReviewService
{
    private readonly DataBaseContext _databaseContext;

    public ReviewService(DataBaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<List<Feedback>> GetFeedbacksByProductIdAsync(int id)
    {
        return await _databaseContext.Feedbacks.ToListAsync();
    }

    public async Task<IEnumerable<Feedback?>> GetReviewAsync(int id, int productId)
    {
        return await _databaseContext.Feedbacks.Where(x => x.Id == id).ToListAsync();
    }

    public async Task<bool> TryAddReviewAsync(int productId, int userId, string description, int grade)
    {
        try
        {
            var dateTime = DateTime.Now;

            var rating = await _databaseContext.Ratings.FirstOrDefaultAsync(r => r.ProductId == productId);
            if (rating == null)
            {
                rating = new Rating
                {
                    ProductId = productId,
                    CreateDate = dateTime,
                    Grade = grade,
                };
                _databaseContext.Ratings.Add(rating);
            }

            var feedback = new Feedback
            {
                ProductId = productId,
                UserId = userId,
                Text = description,
                Grade = grade,
                CreateDate = dateTime,
                RatingId = rating.Id,
                Rating = rating,
                Status = Status.None
            };
            _databaseContext.Feedbacks.Add(feedback);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> TryToDeleteReviewAsync(int id)
    {
        try
        {
            var review = await _databaseContext.Feedbacks.Where(x => x.Id == id).FirstOrDefaultAsync();
            _databaseContext.Feedbacks.Remove(review!);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}