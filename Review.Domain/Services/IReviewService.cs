﻿namespace Review.Domain.Services;

public interface IReviewService
{
    /// <summary>
    /// Получение всех отзывов
    /// </summary>
    /// <param name="id">Id продукта</param>
    /// <returns></returns>
    Task<List<Models.Review>> GetAllAsync();

    /// <summary>
    /// Получение отзывов по Id продукта
    /// </summary>
    /// <param name="productId">Id продукта</param>
    /// <returns></returns>
    Task<List<Models.Review>> GetByProductIdAsync(int productId);
    
    /// <summary>
    /// Добавление отзыва
    /// </summary>
    /// <param name="productId">Id продукта</param>
    /// <param name="userId">Id пользователя</param>
    /// <param name="description">Текст отзыва</param>
    /// <param name="grade">Оценка</param>
    /// <returns></returns>
    Task<bool> TryAddAsync(int productId, int userId, string description, int grade);

    /// <summary>
    /// Удаление отзыва
    /// </summary>
    /// <param name="id">Id отзыва</param>
    /// <returns></returns>
    Task<bool> TryDeleteAsync(int id);
}