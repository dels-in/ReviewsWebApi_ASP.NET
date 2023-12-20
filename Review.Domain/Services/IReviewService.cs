using Review.Domain.Models;

namespace Review.Domain.Services;

public interface IReviewService
{
    /// <summary>
    /// Получение всех отзывов по продукту
    /// </summary>
    /// <param name="id">Id продукта</param>
    /// <returns></returns>
    Task<List<Feedback>> GetFeedbacksByProductIdAsync(int id);

    /// <summary>
    /// Получение отзыва по его Id и Id продукта
    /// </summary>
    /// <param name="id">Id отзыва</param>
    /// <param name="productId">Id продукта</param>
    /// <returns></returns>
    Task<IEnumerable<Feedback?>> GetReviewAsync(int id, int productId);
    
    /// <summary>
    /// Добавление отзыва
    /// </summary>
    /// <param name="productId">Id продукта</param>
    /// <param name="userId">Id пользователя</param>
    /// <param name="description">Текст отзыва</param>
    /// <param name="grade">Оценка</param>
    /// <returns></returns>
    Task<bool> TryAddReviewAsync(int productId, int userId, string description, int grade);

    /// <summary>
    /// Удаление отзыва
    /// </summary>
    /// <param name="id">Id отзыва</param>
    /// <returns></returns>
    Task<bool> TryToDeleteReviewAsync(int id);
}