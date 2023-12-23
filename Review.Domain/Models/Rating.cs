namespace Review.Domain.Models
{
    /// <summary>
    /// Рейтинг
    /// </summary>
    public class Rating
    {
        /// <summary>
        /// Id рейтинга
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id продукта
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Отзывы
        /// </summary>
        public List<Review> Reviews { get; set; } = new();

        /// <summary>
        /// Оценка
        /// </summary>
        public double Grade { get; set; }
    }
}
