using System.Threading.Tasks;
using TaskAssistant.Api.Models.ClientModels;

namespace TaskAssistant.Api.Services.Interfaces
{
    public interface ICustomAuthenticationService
    {
        Task<dynamic> BuildJwtToken(string userName, int tokenExpiryTime);
        Task<bool> ValidateUser(Login loginModel);
    }
}