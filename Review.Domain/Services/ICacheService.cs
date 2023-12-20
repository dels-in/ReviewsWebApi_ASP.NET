namespace Review.Domain.Services;

public interface ICacheService
{
    /// <summary>
    /// Получение данных с помощью ключа
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    T GetData<T>(string key);

    /// <summary>
    /// Установить данные, использвуя значение и срок действия ключа
    /// Set Data with Value and Expiration Time of Key
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="expirationTime"></param>
    /// <returns></returns>
    bool SetData<T>(string key, T value, DateTimeOffset expirationTime);

    /// <summary>
    /// Удалить данные
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    object RemoveData(string key);
}