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