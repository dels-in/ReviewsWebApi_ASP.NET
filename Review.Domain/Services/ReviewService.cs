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

    public async Task<List<Models.Review>> GetAllAsync()
    {
        return await _databaseContext.Reviews.ToListAsync();
    }

    public async Task<List<Models.Review>> GetByProductIdAsync(int productId)
    {
        return await _databaseContext.Reviews.Where(x => x.ProductId == productId).ToListAsync();
    }

    public async Task<bool> TryAddAsync(int productId, int userId, string description, int grade)
    {
        try
        {
            var dateTime = DateTime.Now;
            var grades = await _databaseContext.Reviews.Select(x => x.Grade).ToListAsync();
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

            var feedback = new Models.Review
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
            _databaseContext.Reviews.Add(feedback);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> TryDeleteAsync(int id)
    {
        try
        {
            var review = await _databaseContext.Reviews.Where(x => x.Id == id).FirstOrDefaultAsync();
            _databaseContext.Reviews.Remove(review!);
            await _databaseContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}