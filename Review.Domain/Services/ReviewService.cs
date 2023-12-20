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

    public async Task<List<Feedback>> GetAllAsync()
    {
        return await _databaseContext.Feedbacks.ToListAsync();
    }

    public async Task<IEnumerable<Feedback?>> GetByProductIdAsync(int productId)
    {
        return await _databaseContext.Feedbacks.Where(x => x.ProductId == productId).ToListAsync();
    }

    public async Task<bool> TryAddReviewAsync(int productId, int userId, string description, int grade)
    {
        try
        {
            var dateTime = DateTime.Now;
            var grades = await _databaseContext.Feedbacks.Select(x => x.Grade).ToListAsync();
            grades.Add(grade);
            var gradesAverage = grades.Average();

            var rating = await _databaseContext.Ratings.FirstOrDefaultAsync(r => r.ProductId == productId);
            if (rating == null)
            {
                rating = new Rating
                {
                    ProductId = productId,
                    CreateDate = dateTime,
                    Grade = gradesAverage,
                };
                _databaseContext.Ratings.Add(rating);
                await _databaseContext.SaveChangesAsync();
            }
            else
            {
                rating.Grade = gradesAverage;
                await _databaseContext.SaveChangesAsync();
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