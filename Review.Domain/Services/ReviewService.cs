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

    public async Task<List<Feedback>> GetByProductIdAsync(int productId)
    {
        return await _databaseContext.Feedbacks.Where(x => x.ProductId == productId).ToListAsync();
    }

    public async Task<bool> TryDeleteAsync(int id)
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