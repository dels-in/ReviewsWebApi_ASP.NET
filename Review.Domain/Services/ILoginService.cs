using Review.Domain.Models;

namespace Review.Domain.Services;

public interface ILoginService
{
    /// <summary>
    /// Проверить логин
    /// </summary>
    /// <param name="login">Логин</param>
    /// <returns></returns>
    bool CheckLogin(Login login);
}