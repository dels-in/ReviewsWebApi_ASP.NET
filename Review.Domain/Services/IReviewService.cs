using Review.Domain.Models;

namespace Review.Domain.Services;

public interface IReviewService
{
    /// <summary>
    /// Получение всех отзывов
    /// </summary>
    /// <param name="id">Id продукта</param>
    /// <returns></returns>
    Task<List<Feedback>> GetAllAsync();

    /// <summary>
    /// Получение отзывов по Id продукта
    /// </summary>
    /// <param name="productId">Id продукта</param>
    /// <returns></returns>
    Task<List<Feedback>> GetByProductIdAsync(int productId);
    
    /// <summary>
    /// Удаление отзыва
    /// </summary>
    /// <param name="id">Id отзыва</param>
    /// <returns></returns>
    Task<bool> TryDeleteAsync(int id);
}