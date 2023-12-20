using Review.Domain.Models;

namespace Review.Domain.Services;

public class LoginService : ILoginService
{
    private readonly DataBaseContext _databaseContext;

    public LoginService(DataBaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public bool CheckLogin(Login login)
    {
        var containsLogin = _databaseContext.Logins;
        foreach (var item in containsLogin)
        {
            if (item.UserName.Equals(login.UserName) && item.Password.Equals(login.Password))
            {
                return true;
            }
        }

        return false;
    }
}