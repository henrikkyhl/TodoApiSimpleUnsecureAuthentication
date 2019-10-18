using TodoApi.Models;

namespace TodoApi.Helpers
{
    public interface IAuthenticationHelper
    {
        string GenerateToken(User user);
    }
}
